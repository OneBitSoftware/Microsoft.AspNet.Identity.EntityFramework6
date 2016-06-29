using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity.EntityFramework6.Test
{
    /// <summary>
    /// Used to allow unit tests of the UserManager class.
    /// I'm using this because a few members of UserManager are internal
    /// </summary>
    /// <typeparam name="TUser">The type encapsulating a user.</typeparam>
    public class TestingUserManager<TUser> : UserManager<TUser>
        where TUser : class
    {
        public TestingUserManager
            (IUserStore<TUser> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<TUser> passwordHasher, IEnumerable<IUserValidator<TUser>> userValidators,
            IEnumerable<IPasswordValidator<TUser>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<TestingUserManager<TUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators,
                  passwordValidators, keyNormalizer, errors, services, logger)
        {
            Store = store;
            Options = optionsAccessor?.Value ?? new IdentityOptions();
            PasswordHasher = passwordHasher;
            KeyNormalizer = keyNormalizer;
            ErrorDescriber = errors;
            Logger = logger;

            var propertyPasswordValidators = this.GetType().BaseType.GetProperty("PasswordValidators",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this);
            _passwordValidators = (IList<IPasswordValidator<TUser>>)propertyPasswordValidators;

            var propertyUserValidators = this.GetType().BaseType.GetProperty("UserValidators",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this);
            _userValidators = (IList<IUserValidator<TUser>>)propertyUserValidators;

        }

        public new ILogger Logger;
        public IPasswordHasher<TUser> PasswordHasher;
        public ILookupNormalizer KeyNormalizer;
        public IdentityErrorDescriber ErrorDescriber;
        public IdentityOptions Options;

        IList<IUserValidator<TUser>> _userValidators;
        public IList<IUserValidator<TUser>> UserValidators
        {
            get
            {
                return _userValidators;
            }
        }

        IList<IPasswordValidator<TUser>> _passwordValidators;
        public IList<IPasswordValidator<TUser>> PasswordValidators
        {
            get
            {
                return _passwordValidators;
            }
        }
    }
}
