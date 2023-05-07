namespace universityApiBackend.Models.DataModels
{
    public class JwtSettings
    {
        public bool ValidateIssuerSigningKey { get; set; }
        public string? IssuerSigningKey { get; set; } = string.Empty;
       
        public bool ValidateIssuers { get; set; } = true;
        public string? ValidIssuer { get; set; }
       
        public bool ValidateAudience { get; set; } = true;
        public string? ValidAudience { get; set;}

        public bool RequiredExpoirationTime { get; set; }
        public bool ValidateLifeTime { get; set; } = true;


    }
}
