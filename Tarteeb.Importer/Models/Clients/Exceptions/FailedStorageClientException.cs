//=========================
// Copyrite (c) Tarteeb LLC 
// Powering True Leadership
//=========================


using System;
using Xeptions;

namespace Tarteeb.Importer.Models.Clients.Exceptions
{
    internal class FailedStorageClientException : Xeption
    {
        public FailedStorageClientException(Exception innerException)
            : base(message : "Failed storage error occured, contact support",innerException)
        { }
    }
}
