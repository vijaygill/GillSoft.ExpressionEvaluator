namespace GillSoft.ExpressionEvaluator
{
    public class FunctionParameter
    {
        public object Value { get; private set; }

        public string Name { get; private set; }

        public FunctionParameter(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}