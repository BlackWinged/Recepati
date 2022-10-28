using Dapper;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Recepati.Code.Models;
using Recepati.Controllers;
using Recepati.Db.Code;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Z.Dapper.Plus;

namespace Recepati.Database
{
    public class SecurityManager
    {
        private static string privateKey = @"-----BEGIN RSA PRIVATE KEY-----
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
-----END RSA PRIVATE KEY-----
";

        private static string publicKey = "-----BEGIN PUBLIC KEY-----\r\nMIIBojANBgkqhkiG9w0BAQEFAAOCAY8AMIIBigKCAYEA2168u8mwDzMpgUs1KnBB\r\ndpRZ+FvlcPjYoIsxwXKLxBeAa7ZgDkmxoukjwljPdKbSbyrTE6W+mYo0Ff1gubWi\r\nZQO0bLNJxmZkBMzxhdGyqu877tgrIHCgzJO224T6Mjbxf8rkDRAzXuVU37GIHh1y\r\nqXSAfHnQayTkhmKr+02/fVhsD2Jdc9BxGXmdCLyGSy0Q76pYrGTSysirIjVu+hdC\r\nvR3cCrMCgg+nE5yqiDoQdYmkAcRLGBlcqVdOFj9ok0s4NdmKCD/C9gKdPdsRUO98\r\nhfBbX1MgrDPL98O9FREOJAYdoPP8P6JSTSNahGRtV/z/LTevwWcTj8cIbhvfZpwd\r\nQNiAarJPTh1U9zj+4/NcigE2XgJwkVhzoOLwyjQ5sylVlWAYVqgQOA5/F2ml2V+g\r\nYLgb05083Z++an3j6fev/B+S/mQ8CCprTcRXgQIsaWIHTnjhN7taZOrnyQbDEcYk\r\nAkC62C3R5epJnJmqsbWFkh4AZNLClWRtxh+mszQtfyPPAgMBAAE=\r\n-----END PUBLIC KEY-----\r\n";

        private IHttpContextAccessor context { get; set; }
        public SecurityManager(IHttpContextAccessor context)
        {
            this.context = context;
        }


        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(5);
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        /// <summary>
        /// Validacija passworda uz pomoc hash funkcija za kriptiranje
        /// </summary>
        /// <param name="password">Uneseni password</param>
        /// <param name="correctHash">Vrijednost passworda u bazi</param>
        /// <param name="customPassValidation">Delegat func - ukoliko je pass izracunat s nekom drugom funkcijom koja nije BCrypt, ovdje se postavlja custom racunanje hasha</param>
        /// <returns></returns>
        public static bool ValidatePassword(string password, string correctHash)
        {
            bool result = false;
            bool isPassNullrEmpty = string.IsNullOrEmpty(password);
            bool isHashNullrEmpty = string.IsNullOrEmpty(correctHash);

            if ((isPassNullrEmpty && isHashNullrEmpty) || isPassNullrEmpty)
            {
                return result;
            }

            //regex provjera radi li se o hashu izracunatom s BCrypt-om
            //primjer BCrypt formata: $2a$05$euZTW.RValRcMLbCDiWNmOnsfUA9sOc7WybmVhaVkrKtX5ZdJgJZC 
            var regex = @"^\$2[ayb]\$.{56}$";
            var match = Regex.Match(correctHash, regex, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                result = BCrypt.Net.BCrypt.Verify(password, correctHash);
            }

            return result;
        }


        public string GenerateToken()
        {
            var rsaPublic = RSA.Create();
            rsaPublic.ImportFromPem(publicKey.ToCharArray());

            var rsaPrivate = RSA.Create();
            rsaPrivate.ImportFromPem(privateKey.ToCharArray());

            var token = JwtBuilder.Create()
                      .WithAlgorithm(new RS256Algorithm(rsaPublic, rsaPrivate))
                      .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                      .AddClaim("claim1", 0)
                      .AddClaim("claim2", "claim2-value")
                      .Encode();

            return token;
        }

        public string ParseToken(string token)
        {
            var rsaPublic = RSA.Create();
            rsaPublic.ImportFromPem(publicKey.ToCharArray());

            var rsaPrivate = RSA.Create();
            rsaPrivate.ImportFromPem(privateKey.ToCharArray());

            var json = JwtBuilder.Create()
                     .WithAlgorithm(new RS256Algorithm(rsaPublic, rsaPrivate))
            .MustVerifySignature()
                     .Decode(token);

            return json;
        }
    }
}