namespace Microsoft.AspNet.Identity.EntityFramework6
{
    using System;
    public class IdentityUserRole : IdentityUserRole<string> { }
    public class IdentityUserRole<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary> 
        /// Gets or sets the primary key of the user that is linked to a role. 
        /// </summary> 
        public virtual TKey UserId { get; set; }


        /// <summary> 
        /// Gets or sets the primary key of the role that is linked to the user. 
        /// </summary> 
        public virtual TKey RoleId { get; set; }
    }
}
