//=========================
// Copyrite (c) Tarteeb LLC 
// Powering True Leadership
//=========================


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarteeb.Importer.Brokers.DateTimes;
using Tarteeb.Importer.Brokers.Loggings;
using Tarteeb.Importer.Brokers.Storages;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Clients.Exceptions;
using Tarteeb.Importer.Services.Clients;
using Xeptions;

internal class Program
{
    static async Task Main(string[] args)
    {
        LoggingBroker loggingBroker = new LoggingBroker();
        try
        {
            var clientService = new ClientService(new StorageBroker(), new DateTimeBroker());
            Client client = new Client
            {
                FirstName = "Mirsaid",
                LastName = "Sirojiddinov",
                Email = "Mirsaid04@gmail.com",
                BirthDate = DateTimeOffset.UtcNow,
                Id = Guid.NewGuid(),
                GroupID = Guid.NewGuid(),
                PhoneNumber = "12345678"
            };

            var persistedCLient = await clientService.AddClientAsync(client);

            Console.WriteLine(persistedCLient.FirstName);
        }
        catch (ClientValidationException clientValidationException)
        {
            Xeption innerException = (Xeption)clientValidationException.InnerException;

            loggingBroker.LogError(innerException.Message);

            foreach (DictionaryEntry entry in innerException.Data)
            {
                string errorSummary = ((List<string>)entry.Value)
                    .Select((string value) => value)
                    .Aggregate((string current, string next) => current + ", " + next);

                Console.WriteLine(entry.Key + " - " + errorSummary);
            }
        }
    }
}