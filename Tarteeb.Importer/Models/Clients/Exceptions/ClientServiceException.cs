//=========================
// Copyrite (c) Tarteeb LLC 
// Powering True Leadership
//=========================


using Xeptions;

namespace Tarteeb.Importer.Models.Clients.Exceptions
{
    internal class ClientServiceException : Xeption
    {
        public ClientServiceException(Xeption innerException)
            : base(message: "Client service error occured , contact support.",innerException)
        { }
            
        
    }
}
