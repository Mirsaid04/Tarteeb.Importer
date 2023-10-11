//=========================
// Copyrite (c) Tarteeb LLC 
// Powering True Leadership
//=========================


using System;
using System.Net.Http;
using Xeptions;

namespace Tarteeb.Importer.Models.Clients.Exceptions
{
    internal class NullClientException : Xeption 
    {
        public NullClientException()
            : base(message: "Client is null")
        { }
    }
}
