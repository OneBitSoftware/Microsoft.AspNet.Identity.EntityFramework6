namespace Microsoft.AspNet.Identity.EntityFramework6.Test
{
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNet.Http;
    using System.Collections.Generic;

    public class TestingRoleManager<TRole> : RoleManager<TRole>
            where TRole : class
    {
        public TestingRoleManager
            (IRoleStore<TRole> store, IEnumerable<IRoleValidator<TRole>> roleValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
            ILogger<TestingRoleManager<TRole>> logger, IHttpContextAccessor contextAccessor)
            : base(store, roleValidators, keyNormalizer, errors, logger, contextAccessor)
        {

            Store = store;
            KeyNormalizer = keyNormalizer ?? new UpperInvariantLookupNormalizer();
            ErrorDescriber = errors ?? new IdentityErrorDescriber();
            _context = contextAccessor?.HttpContext;
            Logger = logger;

            var propertyRoleValidators = this.GetType().BaseType.GetProperty("RoleValidators",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this);
            _roleValidators = (IList<IRoleValidator<TRole>>)propertyRoleValidators;
        }

        public new ILogger Logger;

        IList<IRoleValidator<TRole>> _roleValidators;
        public IList<IRoleValidator<TRole>> RoleValidators
        {
            get { return _roleValidators; }
        }

        private readonly HttpContext _context;
        public ILookupNormalizer KeyNormalizer { get; set; }
        public IdentityErrorDescriber ErrorDescriber { get; set; }
        public new IRoleStore<TRole> Store { get; private set; }
    }
}
