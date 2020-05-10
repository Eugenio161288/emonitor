using Microsoft.AspNetCore.Identity;

namespace Euromonitor.Server.IdentityServer.Infrastructure.Data.Identity
{
    public class AppUser : IdentityUser
    {
        // Add additional profile data for application users by adding properties to this class
        public string Name { get; set; }        
    }
}
