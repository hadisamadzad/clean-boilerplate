using Common.Extensions;
using Identity.Application.Types.Entities.Users;
using Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Identity.Database.EntityConfigurations.Users;

internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .UseIdentityAlwaysColumn()
            .UseHiLo("UserId_HiLo");

        builder.HasIndex(x => x.Email).IsUnique();

        #region Mappings

        builder.Property(x => x.Email)
            .HasMaxLength(Constants.Char60Length);

        builder.Property(x => x.Mobile)
            .HasMaxLength(Constants.Char20Length);

        builder.Property(x => x.PasswordHash)
            .HasMaxLength(Constants.PasswordHashLength);

        builder.Property(x => x.FirstName)
            .HasMaxLength(Constants.Char80Length);

        builder.Property(x => x.LastName)
            .HasMaxLength(Constants.Char80Length);

        builder.Property(x => x.SecurityStamp)
            .IsConcurrencyToken()
            .HasMaxLength(Constants.SecurityStampLength)
            .IsFixedLength();

        builder.Property(b => b.ConcurrencyStamp)
            .IsConcurrencyToken()
            .HasMaxLength(Constants.SecurityStampLength);

        #endregion

        #region Conversions

        builder.Property(x => x.Role)
            .HasConversion(new EnumToStringConverter<Role>())
            .HasMaxLength(Role.Admin.GetMaxLength());

        builder.Property(x => x.State)
            .HasConversion(new EnumToStringConverter<UserState>())
            .HasMaxLength(UserState.Active.GetMaxLength());

        #endregion

        #region Navigation

        #endregion
    }
}
