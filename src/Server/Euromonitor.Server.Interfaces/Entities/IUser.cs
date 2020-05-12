namespace Euromonitor.Server.Interfaces.Entities
{
    /// <summary>
    /// Represents the user's properties like names and email.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// The user's first name.
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// The user's last name.
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// The user's email.
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// User given name from authority.
        /// </summary>
        string GivenName { get; set; }
    }
}
