using bytePassion.Lib.Types.SemanticTypes.Base;


namespace bytePassion.Lib.Types.SemanticTypes
{

    public class Radians : SimpleDoubleSemanticType
    {
        public Radians(double value)
            : base(value, "rad")
        {            
        }       
    }
}