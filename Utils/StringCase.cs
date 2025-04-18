using backend.Interfaces;

namespace backend.Utils
{
    public class StringCase : IStringCase
    {
        public string ToSnakeCase(string name)
        {
            var snakeCaseName = string.Empty;
            for (int i = 0; i < name.Length; i++)
            {
                var c = name[i];
                if (char.IsUpper(c))
                {
                    if (i > 0)
                    {
                        snakeCaseName += "_";
                    }
                    snakeCaseName += char.ToLower(c);
                }
                else
                {
                    snakeCaseName += c;
                }
            }
            return snakeCaseName;
        }
    }
}
