using User.Domain.Entities;
using User.Persistence.Configurations.BaseConfigurations;

namespace User.Persistence.Configurations;

/// <summary>
/// Entity Framework Core configuration for the <see cref="UserAccount"/> entity.
/// Inherits soft delete base configuration.
/// </summary>
internal sealed class UserAccountConfiguration : SoftDeleteBaseEntityConfiguration<UserAccount>;
