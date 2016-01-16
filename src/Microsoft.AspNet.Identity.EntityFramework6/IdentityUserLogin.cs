namespace Microsoft.AspNet.Identity.EntityFramework6
{
    using System;

    public class IdentityUserLogin : IdentityUserLogin<string> { }

    public class IdentityUserLogin<TKey> where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }

        /// <summary>
        /// Gets or sets the login provider for the login (e.g. facebook, google)
        /// </summary>
        public virtual string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the unique provider identifier for this login.
        /// </summary>
        public virtual string ProviderKey { get; set; }

        /// <summary>
        /// Gets or sets the friendly name used in a UI for this login.
        /// </summary>
        public virtual string ProviderDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the of the primary key of the user associated with this login.
        /// </summary>
        public virtual TKey UserId { get; set; }
    }
}
