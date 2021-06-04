using System.Collections.Generic;
using Communal.Application.Constants;

namespace Communal.Application.Infrastructure.Errors
{
    public struct ErrorModel
    {
        public ErrorModel(int code, string title, params (Language Language, string Message)[] messages)
        {
            Code = code;
            Title = title;

            Messages = new Dictionary<Language, string>();

            foreach (var message in messages)
                Messages.Add(message.Language, message.Message);
        }

        public readonly int Code;
        public readonly string Title;
        public Dictionary<Language, string> Messages;

        public ErrorModel Ready(params string[] values)
        {
            var messages = new Dictionary<Language, string>();
            foreach (var message in Messages)
                messages.Add(message.Key, string.Format(message.Value, values));

            Messages = messages;
            return this;
        }
    }
}