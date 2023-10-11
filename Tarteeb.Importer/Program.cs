//=========================
// Copyrite (c) Tarteeb LLC 
// Powering True Leadership
//=========================


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
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
        var clientCount = new Faker();

        for (int i = 0; i < 2000; i++)
        {
            Client client = new Client
            {
                FirstName = clientCount.Name.FirstName(),
                LastName = clientCount.Name.LastName(),
                Email = clientCount.Internet.Email(),
                BirthDate = clientCount.Date.Between(new DateTime(1990,1,1), new DateTime(2023,1,1)),
                Id = clientCount.Random.Guid(),
                GroupID =clientCount.Random.Guid(),
                PhoneNumber = clientCount.Phone.PhoneNumber(),
            };

            var loggingBroker = new LoggingBroker();
            var clientService = new ClientService(new StorageBroker(), new DateTimeBroker());

            try
            {
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
            catch (ClientDependencyValidationException clientDependencyValidationException)
            {
                loggingBroker.LogError(clientDependencyValidationException.InnerException.Message);
            }
            catch (ClientDependencyException clientDependencyException)
            {
                loggingBroker.LogError(clientDependencyException.InnerException.Message);
            }
            catch (ClientServiceException clientServiceException)
            {
                loggingBroker.LogError(clientServiceException.InnerException.Message);
            }
        }
    }
}


    
    
    

