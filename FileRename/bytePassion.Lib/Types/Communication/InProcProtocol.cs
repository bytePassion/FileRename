namespace bytePassion.Lib.Types.Communication
{
    public class InProcProtocol : Protocol
    {
        public InProcProtocol() 
            : base(ProtocolType.InProc)
        {            
        }

        public override string ZmqName => "inproc";
    }
}