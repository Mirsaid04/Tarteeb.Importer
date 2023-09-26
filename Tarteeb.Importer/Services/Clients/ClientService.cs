//=========================
// Copyrite (c) Tarteeb LLC 
// Powering True Leadership
//=========================


using System;
using System.Data;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Logging;
using Tarteeb.Importer.Brokers.Loggings;
using Tarteeb.Importer.Brokers.Storages;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Clients.Exceptions;

namespace Tarteeb.Importer.Services.Clients
{
    internal class ClientService
    {
        private readonly StorageBroker storageBroker;

        public ClientService()
        {
            this.storageBroker = new StorageBroker();
        }

        /// <exception cref="NullClientException"></exception>
        /// <exception cref="InvalidClientException"></exception>
        internal Task<Client> AddClientAsync(Client client)
        {
            if (client is null)
            {
                throw new NullClientException(); 
            }

            Validate(
                (Rule: IsInvalid(client.Id), Parameter: nameof(Client.Id)),
                (Rule: IsInvalid(client.FirstName), Parameter: nameof(Client.FirstName)),
                (Rule: IsInvalid(client.LastName), Parameter: nameof(Client.LastName)),
                (Rule: IsInvalid(client.Email), Parameter: nameof(Client.Email)),
                (Rule: IsInvalid(client.PhoneNumber), Parameter: nameof(Client.PhoneNumber)),
                (Rule: IsInvalid(client.GroupID), Parameter: nameof(Client.GroupID)));

            Validate(
                (Rule: IsInvalidEmail(client.Email), Parameter: nameof(Client.Email))
                );

            return this.storageBroker.InsertClientAsync(client);
        }
        
         private dynamic IsInvalid(Guid id) => new
        {
            Condition = id == default,
            Message = "Id is required"
        };

        private dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private dynamic IsInvalidEmail(string email) => new
        {
            Condition = !Regex.IsMatch(email, "[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"),
            Message = "Invalid email"
        };
       
        private void Validate(params (dynamic rule , string parameteres)[] validations)
        {
           var invalidClientException = new InvalidClientException();

            foreach ((dynamic rule , string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidClientException.UpsertDataList(
                       key: parameter,
                       value: rule.Message);
                }
                invalidClientException.ThrowIfContainsErrors();
            }
        }
    }
}
