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

        private Dictionary<string, Func<List<FunctionParameter>, object>> predefinedFunctions = new Dictionary<string, Func<List<FunctionParameter>, object>>
        {
            {  "lower", (a) => { return ("" + a[0].Value).ToLower(CultureInfo.CurrentCulture); } },
            {  "upper", (a) => { return ("" + a[0].Value).ToUpper(CultureInfo.CurrentCulture); } },


            {  "abs", (a) => { return  Math.Abs(Convert.ToDouble(a[0].Value)); } },
            {  "cos", (a) => { return  Math.Cos(Convert.ToDouble(a[0].Value)); } },
            {  "max", (a) => { return  a.Select(o => Convert.ToDouble(o.Value)).Max(); } },
            {  "pow", (a) => { return  Math.Pow(Convert.ToDouble(a[0].Value), Convert.ToDouble(a[1].Value)); } },
            {  "sin", (a) => { return  Math.Sin(Convert.ToDouble(a[0].Value)); } },
            {  "sqrt", (a) => { return  Math.Sqrt(Convert.ToDouble(a[0].Value)); } },
            {  "tan", (a) => { return  Math.Tan(Convert.ToDouble(a[0].Value)); } },
        };

        private static object ProperTypeValue(object value, IToken sign)
        {
            if (value == null)
            {
                return value;
            }

            var signText = sign != null ? sign.Text : string.Empty;

            var mult = "-".Equals(signText) ? -1 : 1;

            var numeric = default(double);

            var isNumeric = double.TryParse("" + value, out numeric);

            if (isNumeric)
            {
                return mult * numeric;
            }

            return signText + value;
        }

        public override object VisitSubExpresion([NotNull] ExpressionParser.SubExpresionContext context)
        {
            return VisitExpression(context.expression());
        }

        public override object VisitExpression([NotNull] ExpressionParser.ExpressionContext context)
        {
            if (context.sign != null)
            {
                var res = VisitExpression(context.expression()[0]);

                return ProperTypeValue(res, context.sign);
            }

            if (context.value != null)
            {
                switch (context.value.Type)
                {
                    case ExpressionParser.STRING:
                        {
                            var res = context.value.Text;
                            if(!string.IsNullOrWhiteSpace(res))
                            {
                                res = res.Replace("'", string.Empty)
                                    .Replace("\"", string.Empty);
                            }
                            return res;
                        }
                    case ExpressionParser.CONST:
                        {
                            // this is a constant type, so just return the text/value of proper type
                            return ProperTypeValue(context.value.Text, context.sign);
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
                                    return ProperTypeValue(e.Value, context.sign);
                                }
                                throw Expression.CreateException(context.value, "Variable not resolved");
                            }
                            else
                            {
                                throw Expression.CreateException(context.value, "No handler provided to resolve variable");
                            }
                        }
                }
            }

            if (context.op != null)
            {
                var left = "" + VisitExpression(context.left);
                var right = "" + VisitExpression(context.right);

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
            return base.VisitExpression(context);
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

            var key = e.Name.ToLower();
            if (predefinedFunctions.ContainsKey(key))
            {
                var func = predefinedFunctions[key];
                e.Result = func(e.Params);
            }
            else
            {
                var handler = handleFunction;
                if (handler != null)
                {
                    handler(e);
                    if (!e.HasResult)
                    {
                        throw new Exception("Unhandled function: " + e.Name);
                    }
                }
            }
            return ProperTypeValue(e.Result, null);
        }
    }
}

