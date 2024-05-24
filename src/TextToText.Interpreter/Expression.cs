namespace TextToText.Interpreter
{
    public sealed class Expression
    {
        readonly Regex _templateRegex = new Regex(@"\{\.([^}]+)\}");
        readonly Regex _pathRegex = new Regex(@"\w+");

        readonly string _template;
        public Expression(string template)
        {
            _template = template;
        }

        public string Result { get; private set; }

        public Expression Interpret(Context context)
        {
            Result = _templateRegex.Replace(_template, new MatchEvaluator(m =>
            {
                string path = _pathRegex.Match(m.Value).Value;
                JsonElement root = context.Doc.RootElement;
                JsonElement element = root.GetProperty(path);

                switch (element.ValueKind)
                {
                    case JsonValueKind.Null:
                    case JsonValueKind.Undefined:
                        return m.Value;
                    default:
                        return element.ToString();
                }
            }));

            return this;
        }
    }
}
