using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Antlr4.Runtime.Tree;

namespace GillSoft.ExpressionEvaluator
{
    internal class ExpressionEvalutationVisitor : ExpressionBaseVisitor<object>
    {
        private readonly Action<FunctionArgs> handleFunction;
        private readonly Action<VariableArgs> handleVariable;

        public ExpressionEvalutationVisitor(Action<FunctionArgs> handleFunction,
            Action<VariableArgs> handleVariable)
        {
            this.handleFunction = handleFunction;
            this.handleVariable = handleVariable;
        }

        private Dictionary<string, object> predefinedConstants = new Dictionary<string, object>
        {
            { "pi", 22.0 / 7.0},
        };

        public override object VisitSubExpression([NotNull] ExpressionParser.SubExpressionContext context)
        {
            if (context.mathExpr != null)
            {
                return VisitMathematicalExpression(context.mathExpr);
            }
            if (context.boolExpr != null)
            {
                return VisitBooleanExprerssion(context.boolExpr);
            }
            return base.VisitSubExpression(context);
        }

        public override object VisitSimpleValue([NotNull] ExpressionParser.SimpleValueContext context)
        {
            var res = string.Empty;
            switch (context.value.Type)
            {
                case ExpressionParser.TRUE:
                case ExpressionParser.FALSE:
                    {
                        return context.value.Text;
                    }
                case ExpressionParser.STRING:
                    {
                        res = context.value.Text;
                        if (!string.IsNullOrWhiteSpace(res))
                        {
                            res = res.Replace("'", string.Empty)
                                .Replace("\"", string.Empty);
                        }
                        return res;
                    }
                case ExpressionParser.CONST:
                    {
                        // this is a constant type, so just return the text/value of proper type
                        return context.value.Text;
                    }
                case ExpressionParser.IDENT:
                    {
                        var name = context.value.Text;
                        var key = name.ToLower();
                        if (predefinedConstants.ContainsKey(key))
                        {
                            return predefinedConstants[key];
                        }

                        // let user code handle the resolution of the variable

                        var handler = handleVariable;
                        if (handler != null)
                        {
                            var e = new VariableArgs(name, context.value.Text);
                            handler(e);
                            if (e.HasValue)
                            {
                                return e.Value;
                            }
                            throw Expression.CreateException(context.value, "Variable not resolved");
                        }
                        else
                        {
                            throw Expression.CreateException(context.value, "No handler provided to resolve variable");
                        }
                    }
            }
            return base.VisitSimpleValue(context);
        }

        public override object VisitMathematicalExpression([NotNull] ExpressionParser.MathematicalExpressionContext context)
        {
            if (context.sign != null)
            {
                var res = "" + VisitMathematicalExpression(context.mathematicalExpression()[0]);
                var mult = context.sign.Text.Equals("-") ? -1 : 1;

                var resNum = default(double);
                if (!double.TryParse(res, out resNum))
                {
                    throw new Exception("Cannot convert to numeric: " + res);
                }
                return resNum * mult;
            }
            if (context.functionValue != null)
            {
                return VisitFunction(context.functionValue);
            }
            if (context.op != null)
            {
                var left = "" + VisitMathematicalExpression(context.left);
                var right = "" + VisitMathematicalExpression(context.right);

                var leftNumeric = default(double);
                var rightNumeric = default(double);

                var isNumeric = (double.TryParse(left, out leftNumeric)
                    && double.TryParse(right, out rightNumeric));

                switch (context.op.Type)
                {
                    case ExpressionParser.ADD:
                        {
                            if (isNumeric)
                            {
                                return leftNumeric + rightNumeric;
                            }
                            else
                            {
                                return left + right;
                            }
                        }
                    case ExpressionParser.SUB:
                        {
                            if (isNumeric)
                            {
                                return leftNumeric - rightNumeric;
                            }
                            else
                            {
                                throw new Exception("Cannot apply operand " + ExpressionParser.DefaultVocabulary.GetLiteralName(context.op.Type) + " on strings");
                            }
                        }
                    case ExpressionParser.DIV:
                        {
                            if (isNumeric)
                            {
                                return leftNumeric / rightNumeric;
                            }
                            else
                            {
                                throw new Exception("Cannot apply operand " + ExpressionParser.DefaultVocabulary.GetLiteralName(context.op.Type) + " on strings");
                            }
                        }
                    case ExpressionParser.MULT:
                        {
                            if (isNumeric)
                            {
                                return leftNumeric * rightNumeric;
                            }
                            else
                            {
                                throw new Exception("Cannot apply operand " + ExpressionParser.DefaultVocabulary.GetLiteralName(context.op.Type) + " on strings");
                            }
                        }
                    case ExpressionParser.POW:
                        {
                            if (isNumeric)
                            {
                                return Math.Pow(leftNumeric, rightNumeric);
                            }
                            else
                            {
                                throw new Exception("Cannot apply operand " + ExpressionParser.DefaultVocabulary.GetLiteralName(context.op.Type) + " on strings");
                            }
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            return base.VisitMathematicalExpression(context);
        }

        public override object VisitTerminal(ITerminalNode node)
        {
            return base.VisitTerminal(node);
        }

        public override object VisitFunction([NotNull] ExpressionParser.FunctionContext context)
        {
            var e = new FunctionArgs(context.name.Text);
            if (context.paramFirst != null)
            {
                var paramEvaluated = VisitExpression(context.paramFirst);
                var p = new FunctionParameter(context.paramFirst.GetText(), paramEvaluated);
                e.Params.Add(p);
            }
            if (context._paramRest != null)
            {
                foreach (var item in context._paramRest)
                {
                    var paramEvaluated = VisitExpression(item);
                    var p = new FunctionParameter(item.GetText(), paramEvaluated);
                    e.Params.Add(p);
                }
            }

            var handler = handleFunction;
            if (handler != null)
            {
                handler(e);
                if (!e.HasResult)
                {
                    throw new Exception("Unhandled function: " + e.Name);
                }
            }
            return e.Result;
        }

        public override object VisitBooleanExprerssion([NotNull] ExpressionParser.BooleanExprerssionContext context)
        {
            if (context.value != null)
            {
                return VisitSimpleValue(context.value);
            }
            if (context.sign != null)
            {
                var res = "" + VisitBooleanExprerssion(context.expr);
                var resBool = default(bool);
                if (!bool.TryParse(res, out resBool))
                {
                    throw new Exception("Cannot convert to boolean: " + res);
                }
                return !resBool;
            }
            if (context.functionValue != null)
            {
                return VisitFunction(context.functionValue);
            }
            if (context.left != null && context.right != null)
            {
                var left = "" + VisitBooleanExprerssion(context.left);
                var right = "" + VisitBooleanExprerssion(context.right);

                var leftBool = default(bool);
                var rightBool = default(bool);

                if (!bool.TryParse(left, out leftBool))
                {
                    throw new Exception("Cannot convert to boolean: " + left);
                }
                if (!bool.TryParse(right, out rightBool))
                {
                    throw new Exception("Cannot convert to boolean: " + right);
                };

                switch (context.op.Type)
                {
                    case ExpressionParser.AND:
                        {
                            return leftBool && rightBool;
                        }
                    case ExpressionParser.OR:
                        {
                            return leftBool || rightBool;
                        }
                }
            }
            return base.VisitBooleanExprerssion(context);
        }
    }
}

