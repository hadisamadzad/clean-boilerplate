using System.Collections.Generic;
using Common.Application.Constants;

namespace Common.Application.Infrastructure.Errors;

public record ErrorModel(string Code, string Title, string Message);