//=========================
// Copyrite (c) Tarteeb LLC 
// Powering True Leadership
//=========================


using System.Text.RegularExpressions;
using System;
using Tarteeb.Importer.Models.Clients.Exceptions;
using Tarteeb.Importer.Models.Clients;

namespace Tarteeb.Importer.Services.Clients
{
    internal partial class ClientService
    {
        private void ValidateClientOnAdd(Client client)
        {
            ValidateClientNotNull(client);

            Validate(
                (Rule: IsInvalid(client.Id), Parameter: nameof(Client.Id)),
                (Rule: IsInvalid(client.FirstName), Parameter: nameof(Client.FirstName)),
                (Rule: IsInvalid(client.LastName), Parameter: nameof(Client.LastName)),
                (Rule: IsInvalid(client.Email), Parameter: nameof(Client.Email)),
                (Rule: IsInvalid(client.BirthDate), Parameter: nameof(Client.BirthDate)),
                (Rule: IsLessThan12(client.BirthDate), Parameter: nameof(Client.BirthDate)),
                (Rule: IsInvalid(client.PhoneNumber), Parameter: nameof(Client.PhoneNumber)),
                (Rule: IsInvalid(client.GroupID), Parameter: nameof(Client.GroupID)));

            Validate(
                (Rule: IsInvalidEmail(client.Email), Parameter: nameof(Client.Email)));
        }

        private static void ValidateClientNotNull(Client client)
        {
            if (client is null)
            {
                throw new NullClientException();
            }
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
            Message = "Email is invalid"
        };

        private dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private dynamic IsLessThan12(DateTimeOffset date) => new
        {
            Condition = IsAgeLessThan12(date),
            Message = "Age is less than 12"
        };

        private bool IsAgeLessThan12(DateTimeOffset date)
        {
            DateTimeOffset now = this.dateTimeBroker.GetCurrentDateTimeOffset();
            int age = (now - date).Days / 365;

            return age < 12;
        }

        private void Validate(params (dynamic rule, string parameteres)[] validations)
        {
            var invalidClientException = new InvalidClientException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidClientException.UpsertDataList(
                       key: parameter,
                       value: rule.Message);
                }
            }
            invalidClientException.ThrowIfContainsErrors();
        }
    }
}
