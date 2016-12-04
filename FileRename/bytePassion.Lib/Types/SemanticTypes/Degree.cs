using bytePassion.Lib.Types.SemanticTypes.Base;


namespace bytePassion.Lib.Types.SemanticTypes
{
    public class Degree : SimpleDoubleSemanticType
    {        
        public Degree(double value)
            : base(value, "deg")
        {            
        }        
    }
}
