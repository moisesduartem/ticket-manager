﻿namespace TicketManager.Api.Core.Exceptions
{
    public class ApplicationLayerException : Exception
    {
        public ApplicationLayerException(string message) : base(message)
        { }
    }
}
