namespace University_Api_Backend.Models.DataModels
{
    public class JwtSettings
    {// se puede emplear en cualquier proyecto

        public bool ValidateIssuerSigningKey { get; set; } // para comprobar la firma de nuestro usuario
        public string IssuerSigningKey { get; set; } = string.Empty;


        public bool ValidateIssuer { get; set; } = true;
        public string? ValidIssuer { get; set; }


        public bool ValidateAudience { get; set; } = true;
        public string? ValidAudience { get; set; }

        public bool RequireExpirationTime { get; set; }

        public bool ValidateLifetime { get; set; } = true;








    }
}
