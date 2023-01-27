﻿namespace UniversityApiBackend.Models.DataModels
{
    public class JwtSettings
    {
        public bool ValidateIssuerSignigKey { get; set; }  
        
        public string IssuerSigningKey { get; set; } = string.Empty;


        public bool ValidateIssuer { get; set; } = true;

        public string? ValidIssuer { get; set; }

        
        public bool ValidateAudience { get; set; } = true;

        public string? ValidAudience { get; set; }

        
        public bool RequiereExpirationTime { get; set; }

        public bool ValidateLifetime { get; set; } = true;
        // 49 min Clase 6 Configuraciones JWT
    }
}