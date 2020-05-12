using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbClientConsole
{
    public class User
    {
        [BsonElement("firstName")]
        /// <summary>
        /// The user's first name.
        /// </summary>
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        /// <summary>
        /// The user's last name.
        /// </summary>
        public string LastName { get; set; }

        [BsonElement("email")]
        /// <summary>
        /// The user's email.
        /// </summary>
        public string Email { get; set; }

        [BsonElement("books")]
        public List<Book> Books { get; set; }

        [BsonElement("givenname")]
        public string GivenName { get; set; }
    }
}
