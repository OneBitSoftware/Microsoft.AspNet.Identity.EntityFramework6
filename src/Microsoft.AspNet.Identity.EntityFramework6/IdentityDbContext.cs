namespace Microsoft.AspNet.Identity.EntityFramework6
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Infrastructure.Annotations;

     /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    public class IdentityDbContext : IdentityDbContext
        <IdentityUser, IdentityRole, string, IdentityUserLogin,
        IdentityUserRole, IdentityUserClaim, IdentityRoleClaim>
    {
        public IdentityDbContext(string connectionString) : base(connectionString) { }

        /// <summary>
        ///     Default constructor which uses the DefaultConnection
        /// </summary>
        public IdentityDbContext()
            : this("DefaultConnection")
        {
        }
    }

    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of the user objects.</typeparam>
    public class IdentityDbContext<TUser> : IdentityDbContext
        <TUser, IdentityRole, string,
        IdentityUserLogin, IdentityUserRole,
        IdentityUserClaim, IdentityRoleClaim>
        where TUser : IdentityUser
    {
        /// <summary>
        ///     Constructor which takes the connection string to use
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        public IdentityDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
    }

    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of user objects.</typeparam>
    /// <typeparam name="TRole">The type of role objects.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for users and roles.</typeparam>
    public class IdentityDbContext
        <TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim, TRoleClaim> : DbContext
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
    {
        /// <summary>
        ///     Default constructor which uses the DefaultConnection
        /// </summary>
        public IdentityDbContext()
            : this("DefaultConnection")
        {
        }

        /// <summary>
        ///     Constructor which takes the connection string to use
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        public IdentityDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        /// <summary>
        ///     Constructs a new context instance using conventions to create the name of
        ///     the database to which a connection will be made, and initializes it from
        ///     the given model.  The by-convention name is the full name (namespace + class
        ///     name) of the derived context class.  See the class remarks for how this is
        ///     used to create a connection.
        /// </summary>
        /// <param name="model">The model that will back this context.</param>
        public IdentityDbContext(DbCompiledModel model) : base(model)
        {
        }

        /// <summary>
        ///     Constructs a new context instance using the given string as the name or connection
        ///     string for the database to which a connection will be made, and initializes
        ///     it from the given model.  See the class remarks for how this is used to create
        ///     a connection.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        /// <param name="model">The model that will back this context.</param>
        public IdentityDbContext(string nameOrConnectionString, DbCompiledModel model) : base(nameOrConnectionString, model)
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of Users.
        /// </summary>
        public DbSet<TUser> Users { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of User roles.
        /// </summary>
        public DbSet<TUserRole> UserRoles { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of User claims.
        /// </summary>
        public DbSet<TUserClaim> UserClaims { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of User logins.
        /// </summary>
        public DbSet<TUserLogin> UserLogins { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of roles.
        /// </summary>
        public DbSet<TRole> Roles { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of role claims.
        /// </summary>
        public DbSet<TRoleClaim> RoleClaims { get; set; }

        /// <summary>
        /// Configures the schema needed for the identity framework.
        /// </summary>
        /// <param name="builder">
        /// The builder being used to construct the model for this context.
        /// </param>
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var userClaim = builder.Entity<TUserClaim>().ToTable("AspNetUserClaims");
            userClaim.HasKey(uc => uc.Id);

            var roleClaim = builder.Entity<TRoleClaim>().ToTable("AspNetRoleClaims");
            roleClaim.HasKey(rc => rc.Id);

            var userRole = builder.Entity<TUserRole>().ToTable("AspNetUserRoles");
            userRole.HasKey(r => new { r.UserId, r.RoleId });

            var userLogin = builder.Entity<TUserLogin>().ToTable("AspNetUserLogins");
            userLogin.HasKey(l => new { l.LoginProvider, l.ProviderKey });


            var role = builder.Entity<TRole>().ToTable("AspNetRoles");
            role.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
            role.Property(u => u.Name).HasMaxLength(256);
            role.Property(u => u.NormalizedName)
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(
                new IndexAttribute("RoleNameIndex") { IsUnique = true }));

            role.HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);
            role.HasMany(r => r.Claims).WithRequired().HasForeignKey(rc => rc.RoleId);


            var user = builder.Entity<TUser>().ToTable("AspNetUsers");

            user.HasKey(u => u.Id);

            user.Property(u => u.UserName)
                .HasMaxLength(256)
                .IsRequired();
            user.Property(u => u.Email).HasMaxLength(256);
            user.Property(u => u.NormalizedUserName)
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(
                    new IndexAttribute("UserNameIndex") { IsUnique = true }));
            user.Property(u => u.NormalizedEmail)
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(
                    new IndexAttribute("EmailIndex") { IsUnique = true }));
            user.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            user.HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);
            user.HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);
            user.HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);


        }
    }
}
