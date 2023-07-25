﻿using System.Text;

namespace Lazar.Services.Contracts.Auth {
    internal class AuthDto
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string KEY) =>
           new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
