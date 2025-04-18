using backend.Interfaces;

namespace backend.Utils
{
    public class Utils : IUtils
    {
        public IStringCase StringCase { get; }

        public Utils(IStringCase stringCase)
        {
            StringCase = stringCase;
        }
    }
}
