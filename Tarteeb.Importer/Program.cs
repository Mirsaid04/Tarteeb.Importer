//=========================
// Copyrite (c) Tarteeb LLC 
// Powering True Leadership
//=========================


using System;
using Tarteeb.Importer.Brokers.Storages;
using Tarteeb.Importer.Models.Clients;

namespace Tarteeb.Importer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new Client
            {
                Id = Guid.NewGuid(),
                FirstName = "Mirsaid",
                LastName = "Sirojiddinov",
                PhoneNumber = "00000000000",
                Email = "Bro@gmail.com",
                BirthDate = DateTime.Now,
            };

            using (var storageBroker = new StorageBroker())
            {
                storageBroker.Clients.Add(client);
                storageBroker.SaveChanges();
            }
        }
    }
}