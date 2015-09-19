using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat_Feeder
{
    class ClientService
    {
        /// <summary>
        /// Registers the client. Provides no means of Authentication. Authentication MUST be done prior to registering at the service level.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool RegisterClient(Client client)
        {
            //take the client here and log them into the Azure System.
            return false;
        }
        /// <summary>
        /// Client Check in is to provide a method of lasting communication and to also ensure connectiviety. (Last Checked In: can provide information about how long it's been since the service has seen a device) 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public bool CheckinClient(Client client, AzureDataPackage Data)
        {
            //take the client and tell the Azure System that we are still here using the Azure DataPackage
            return false;
        }
    }
    class AzureDataPackage
    {
        public string data { get; set; }
    }
}
