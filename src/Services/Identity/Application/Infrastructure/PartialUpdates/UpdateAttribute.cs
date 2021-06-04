using System;

namespace Identity.Application.Infrastructure.PartialUpdates
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UpdateAttribute : Attribute
    {
        private readonly UpdateType _update;

        public UpdateAttribute(UpdateType update)
        {
            _update = update;
        }

        public UpdateType GetUpdateType()
        {
            return _update;
        }
    }
}
