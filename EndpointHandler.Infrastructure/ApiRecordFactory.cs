using System;
using EndpointHandler.Domain;
using Newtonsoft.Json;

namespace EndpointHandler.Infrastructure
{
    public static class ApiRecordFactory
    {
        public static ApiRecord DeserializeJsonIntoRecord(string jsonToDeserialize) =>
            JsonConvert.DeserializeObject<ApiRecord>(jsonToDeserialize)
            ?? throw new InvalidOperationException("Received response can not be deserialized.");
    }
}