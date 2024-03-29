﻿//=========================
// Copyrite (c) Tarteeb LLC 
// Powering True Leadership
//=========================


using System;
using Xeptions;

namespace Tarteeb.Importer.Models.Clients.Exceptions
{
    internal class FailedServiceClientException : Xeption
    {
        public FailedServiceClientException(Exception innerException)
            : base(message: "Failed client storage error occured , contact support.", innerException)
        { }
    }
}
