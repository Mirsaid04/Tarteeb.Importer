﻿//=========================
// Copyrite (c) Tarteeb LLC 
// Powering True Leadership
//=========================


using Xeptions;

namespace Tarteeb.Importer.Models.Clients.Exceptions
{
    internal class ClientDependencyException : Xeption
    {
        public ClientDependencyException(Xeption innerException)
            : base(message: "Client dependency error occured , contact support.",innerException)
        { }
    }
}
