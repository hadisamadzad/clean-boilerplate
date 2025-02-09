using Common.Interfaces;

namespace Identity.Application.Types.Entities;

public interface ISoftDeletableEntity : IEntity
{
    bool IsDeleted { get; set; }
    DateTime DeletedAt { get; set; }
}