using Identity.Application.Extensions;
using Identity.Domain;
using Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Identity.Persistence.EntityConfigurations.Users
{
    internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(b => b.Username).IsUnique();

            #region Mappings

            builder.Property(b => b.Username)
                .HasMaxLength(Defaults.UsernameLength)
                .IsRequired();

            builder.Property(b => b.Email)
                .HasMaxLength(Defaults.EmailLength);

            builder.Property(b => b.PasswordHash)
                .HasMaxLength(Defaults.PasswordHashLength);

            builder.Property(b => b.FirstName)
                .HasMaxLength(Defaults.NameLength);

            builder.Property(b => b.LastName)
                .HasMaxLength(Defaults.NameLength);

            builder.Property(b => b.SecurityStamp)
                .IsConcurrencyToken()
                .HasMaxLength(Defaults.SecurityStampLength)
                .IsFixedLength();

            builder.Property(b => b.ConcurrencyStamp)
                .IsConcurrencyToken()
                .HasMaxLength(Defaults.SecurityStampLength);

            #endregion

            #region Conversions

            builder.Property(x => x.State)
                .HasConversion(new EnumToStringConverter<UserState>())
                .HasMaxLength(UserState.Active.GetMaxLength());

            #endregion

            #region Navigations

            builder
                .HasMany(x => x.Roles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            #endregion
        }
    }
}