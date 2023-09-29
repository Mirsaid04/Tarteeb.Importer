//=========================
// Copyrite (c) Tarteeb LLC 
// Powering True Leadership
//=========================


using System;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Clients.Exceptions;
using Xeptions;

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

                throw CreatClientValidationException(nullClientException);
            }
            catch (InvalidClientException invalidClientException)
            { 
                throw CreatClientValidationException(invalidClientException);
            }
            catch(DuplicateKeyException duplicateKeyException)
            {
                var  alreadyExistsClientException =
                    new AlreadyExistsClientException(duplicateKeyException);

                throw CreateDependencyValidationException(alreadyExistsClientException);
            }
            catch(DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
               var lockedClientException = new LockedClientException(dbUpdateConcurrencyException);

                throw CreatDependencyException(lockedClientException);
            }
            catch(DbUpdateException  dbUpdateException)
            {
                var failedStorageClientException =
                 new FailedStorageClientException(dbUpdateException);

                    throw CreatDependencyException(failedStorageClientException);
            }
            catch (Exception exception)
            {
                var failedServiceClientException =
                    new FailedServiceClientException(exception);

                throw CreatServiceException(failedServiceClientException);
            }
        }

        private ClientValidationException CreatClientValidationException(Xeption exception)
        {
            var clientValidationException = new ClientValidationException(exception);

            return clientValidationException;
        }

        private ClientDependencyValidationException  CreateDependencyValidationException(Xeption exception)
        {
            var clientDependencyValidationException = 
                new ClientDependencyValidationException(exception);

            return clientDependencyValidationException;
        }

        private Exception CreatDependencyException(Xeption exception)
        {
          var  clientDependencyException =
                new ClientDependencyException(exception);

            return clientDependencyException;
        }
        private ClientServiceException CreatServiceException(Xeption exception)
        {
            var clientServiceException = 
                new ClientServiceException(exception);

            return clientServiceException;
        }
    }
}
