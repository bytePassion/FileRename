namespace bytePassion.Lib.Types.Communication
{
    public class Address
    {
        public Address(Protocol protocol, AddressIdentifier identifier)
        {
            Protocol = protocol;
            Identifier = identifier;
        }

        public Protocol          Protocol   { get; }
        public AddressIdentifier Identifier { get; }

        public string ZmqAddress => $"{Protocol.ZmqName}://{Identifier}";
    }
}
