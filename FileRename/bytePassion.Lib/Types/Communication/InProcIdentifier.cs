namespace bytePassion.Lib.Types.Communication
{
    public class InProcIdentifier : AddressIdentifier
    {        
        public InProcIdentifier(string identifier)
            : base(AddressIdentifierType.String)
        {
            Identifier = identifier;
        }

        public string Identifier { get; }
    }
}