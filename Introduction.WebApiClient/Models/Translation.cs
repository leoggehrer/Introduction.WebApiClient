

namespace Introduction.WebApiClient.Models
{
    public class Translation
    {
        public int Id { get; set; }
        public string? AppName { get; set; }
        public int KeyLanguage { get; set; }
        public string Key { get; set; } = string.Empty;
        public int ValueLanguage { get; set; }
        public string Value { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{AppName}-{KeyLanguage}-{Key}-{ValueLanguage}-{Value}";
        }
    }
}
