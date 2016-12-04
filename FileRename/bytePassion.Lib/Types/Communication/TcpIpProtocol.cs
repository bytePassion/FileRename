namespace bytePassion.Lib.Types.Communication
{
    public class TcpIpProtocol : Protocol
    {
        public TcpIpProtocol()
            : base(ProtocolType.TcpIp)
        {            
        }
        
        public override string ZmqName => "tcp";        
    }
}