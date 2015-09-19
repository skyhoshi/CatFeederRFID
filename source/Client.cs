using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat_Feeder
{
    /// <summary>
    /// Store Information about the individual Animal Feeder such as ID and Login
    /// </summary>
    class Client
    {
        /// <summary>
        /// used to register the client with another feeder and the online services.
        /// </summary>
        public Guid ClientID { get; set; }
        /// <summary>
        /// Token for Login : used in authentication methods along with the client ID to form a username and password
        /// </summary>
        public string Auth { get; set; }
                /// <summary>
        /// Human Readable Identification
        /// </summary>
        public string Name { get; set; }

    }
}
