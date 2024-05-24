namespace TextToText.Interpreter
{
    public sealed class Context
    {
        readonly JsonDocument _doc;

        public Context(JsonDocument doc)
        {
            _doc = doc;
        }

        public JsonDocument Doc => _doc;
    }
}