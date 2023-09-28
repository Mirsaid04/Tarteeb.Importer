//=========================
// Copyrite (c) Tarteeb LLC 
// Powering True Leadership
//=========================


using System;
using System.Data;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tarteeb.Importer.Brokers.DateTimes;
using Tarteeb.Importer.Brokers.Storages;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Clients.Exceptions;

namespace Tarteeb.Importer.Services.Clients
{
    internal partial class ClientService
    {
        private readonly StorageBroker storageBroker;
        private readonly DateTimeBroker dateTimeBroker;

        public ClientService(StorageBroker storageBroker, DateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        /// <exception cref="NullClientException"></exception>
        /// <exception cref="InvalidClientException"></exception>
        internal Task<Client> AddClientAsync(Client client) =>
        TryCatch(() =>
        {
                ValidateClientOnAdd(client);
                return this.storageBroker.InsertClientAsync(client);
        });
    }
}
