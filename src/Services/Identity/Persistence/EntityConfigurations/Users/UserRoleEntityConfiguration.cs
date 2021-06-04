using Identity.Application.Extensions;
using Identity.Domain.Roles;
using Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Identity.Persistence.EntityConfigurations.Users
{
    internal class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.UserId, x.Role }).IsUnique();

            #region Conversions

            builder.Property(x => x.Role)
                .HasConversion(new EnumToStringConverter<Role>())
                .HasMaxLength(Role.Admin.GetMaxLength());

            #endregion
        }
    }
}