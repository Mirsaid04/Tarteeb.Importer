//=========================
// Copyrite (c) Tarteeb LLC 
// Powering True Leadership
//=========================


using System;
using Xeptions;

namespace Tarteeb.Importer.Models.Clients.Exceptions
{
    internal class LockedClientException : Xeption
    {
        public LockedClientException(Exception innerException)
            :base("Client is locked, try again later. ",innerException)
        { }
    }
}
