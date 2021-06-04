using System.Reflection;
using Communal.Application.Infrastructure.Operations;
using Identity.Application.Behaviors.Common;
using Identity.Application.Behaviors.Users;
using Identity.Application.Commands.Users;
using Identity.Application.Models.Commands.Users;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Extensions.DependencyInjection
{
    public static class MediatRInjection
    {
        public static IServiceCollection AddConfiguredMediatR(this IServiceCollection services)
        {
            // Handlers
            services.AddMediatR(typeof(CreateUserCommand).GetTypeInfo().Assembly);

            // Generic behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommitBehavior<,>));

            // Validation behaviors
            // Users
            services.AddTransient(typeof(IPipelineBehavior<CreateUserCommand, OperationResult>),
                typeof(CreateUserValidationBehavior<CreateUserCommand, OperationResult>));
            services.AddTransient(typeof(IPipelineBehavior<UpdateUserCommand, OperationResult>),
                typeof(UpdateUserValidationBehavior<UpdateUserCommand, OperationResult>));
            services.AddTransient(typeof(IPipelineBehavior<UpdateUserRolesCommand, OperationResult>),
                typeof(UpdateUserRolesValidationBehavior<UpdateUserRolesCommand, OperationResult>));

            return services;
        }
    }
}