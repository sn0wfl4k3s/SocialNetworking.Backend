using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class Response<TResponse>
    {
        public IDictionary<string, List<string>> Errors { get; private set; }
        public TResponse Result { get; private set; }

        public Response() => Errors = new Dictionary<string, List<string>>();

        public void AddResult(TResponse result) => Result = result;

        public void AddError(string key, string message)
        {
            if (!Errors.ContainsKey(key))
            {
                Errors.Add(key, new List<string>() { message });
            }
            else
            {
                Errors[key].Add(message);
            }
        }
    }
}
