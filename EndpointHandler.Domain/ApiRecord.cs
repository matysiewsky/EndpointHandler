namespace EndpointHandler.Domain
{
    public record ApiRecord(string Fact, int Length)
    {
        public override string ToString()
            => $"fact: '{Fact}', length: {Length}";
    };
}