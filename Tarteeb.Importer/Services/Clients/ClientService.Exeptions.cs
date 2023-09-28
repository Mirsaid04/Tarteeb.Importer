//=========================
// Copyrite (c) Tarteeb LLC 
// Powering True Leadership
//=========================


using System.Threading.Tasks;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Clients.Exceptions;

namespace Tarteeb.Importer.Services.Clients
{
    internal partial class ClientService
    {
        private delegate Task<Client> ReturningClientFunction();

        private Task<Client> TryCatch(ReturningClientFunction returningClientFunction)
        {
            try
            {
                return returningClientFunction();
            }
            catch (NullClientException nullClientException)
            {
                var clientValidationException =
                     new ClientValidationException(nullClientException);

                throw clientValidationException;
            }
            catch (InvalidClientException invalidClientException)
            {
                var clientValidationExceptiont =
                    new ClientValidationException(invalidClientException);

                throw clientValidationExceptiont;
            }
        }
    }
}
