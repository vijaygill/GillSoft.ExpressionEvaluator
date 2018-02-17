using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GillSoft.ExpressionEvaluator.Internals;

namespace GillSoft.ExpressionEvaluator
{
    public class Expression : IAntlrErrorListener<IToken>
    {
        public event EventHandler<FunctionArgs> HandleFunction;
        public event EventHandler<VariableArgs> HandleVariable;

        public object Evaluate(string expression)
        {
            var inputStream = new AntlrInputStream(expression);
            var lexer = new ExpressionLexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new ExpressionParser(tokenStream);

            parser.RemoveErrorListeners();

            parser.AddErrorListener(this);

            var tree = parser.expression();

            var visitor = new ExpressionEvalutationVisitor((e) => this.InvokeHandler(HandleFunction, e), (e) => this.InvokeHandler(HandleVariable, e));

            var res = visitor.Visit(tree);

            return res;
        }


        public static Exception CreateException(IToken token, string message)
        {
            var exception = string.Format("{0} :{1} Line {2} Column {3}", message, token.Text, token.Line, token.Column);
            var res = new Exception(exception);
            return res;
        }

        public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw CreateException(offendingSymbol, msg);
        }
    }
}
