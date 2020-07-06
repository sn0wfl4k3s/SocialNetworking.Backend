using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Core.Service
{
    public class Response<T>
    {
        private readonly IDictionary<string, List<string>> _messages = new Dictionary<string, List<string>>();

        public IDictionary<string, List<string>> Errors { get; }
        public T Result { get; }

        public Response() => Errors = new ReadOnlyDictionary<string, List<string>>(_messages);

        public Response(T result) : this() => Result = result;

        public bool HasError() => _messages.Keys.Count > 0;

        public Response<T> AddError(string key, string message)
        {
            if (!_messages.ContainsKey(key))
            {
                _messages.Add(key, new List<string>() { message });
            }
            else
            {
                _messages[key].Add(message);
            }

            return this;
        }
    }
}
