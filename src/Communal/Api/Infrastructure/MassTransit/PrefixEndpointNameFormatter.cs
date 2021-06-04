using System.Text.RegularExpressions;
using MassTransit.Definition;

namespace Communal.Api.Infrastructure.MassTransit
{
    public class PrefixEndpointNameFormatter : DefaultEndpointNameFormatter
    {
        private readonly string _prefix;
        private static readonly Regex Pattern = new Regex("(?<=[a-z0-9])[A-Z]", RegexOptions.Compiled);
        private readonly string _separator;

        public PrefixEndpointNameFormatter(string prefix, string separator = "-")
        {
            _prefix = prefix;
            _separator = separator;
        }

        public override string SanitizeName(string name) =>
            Pattern.Replace(_prefix + name, m => _separator + m.Value)
            .ToLowerInvariant();
    }
}