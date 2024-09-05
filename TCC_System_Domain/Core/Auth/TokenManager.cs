using System;
using System.IO.Compression;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TCC_System_Domain.Core.Auth.JsonObjects;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace TCC_System_Domain.Core
{
    public class TokenManager
    {
        public static string GenerateToken(UserJson user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim("DomainLogin", user.DomainLogin),
                        new Claim("Login", user.Login),
                        new Claim("Nome", user.Nome),
                        new Claim("Email", user.Email),
                        new Claim("Claims", user.Claims),
                }),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(GenerateSecretKey()), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        public static string GenerateDbToken(DBJson db)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim("Server", db.Server),
                        new Claim("User", db.User),
                        new Claim("Password", db.Password)
                }),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(GenerateSecretKey()), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return CompressString(token);
        }

        public static DBJson GetDBJsonByToken(string tokenValue)
        {
            if (!string.IsNullOrEmpty(tokenValue))
            {
                tokenValue = DecompressString(tokenValue);
                var handler = new JwtSecurityTokenHandler();
                var tokenReader = handler.ReadJwtToken(tokenValue);
                var payload = tokenReader.Payload.SerializeToJson();
                var json = JsonConvert.DeserializeObject<DBJson>(payload);

                return json;
            }

            return null;
        }

        public static UserJson GetUserJsonByToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                var valorCookie = DecompressString(token);
                var handler = new JwtSecurityTokenHandler();
                var tokenReader = handler.ReadJwtToken(valorCookie);
                var payload = tokenReader.Payload.SerializeToJson();
                var json = JsonConvert.DeserializeObject<UserJson>(payload);

                return json;
            }

            return null;
        }

        public static string GetTokenKey()
        {
            return "LLSToken";
        }

        public static SecurityKey GetFixedSecretKey()
        {
            var secret = "teNjm3PCKoiJ_izuR5JT6ONBoWMsD-O9PUiiIcrqAK4oNHEukb1oP8cpYCqg2h0HMCYndfFPYkGGrguybZI27g";
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }

        public static byte[] GenerateSecretKey()
        {
            var key = new byte[64];
            RandomNumberGenerator.Create().GetBytes(key);
            return key;
        }

        public static string GenerateSecretKey(bool safeForURL = false)
        {
            var key = new byte[64];
            RandomNumberGenerator.Create().GetBytes(key);
            var base64Secret = Convert.ToBase64String(key);

            // make safe for url
            if (safeForURL)
                return base64Secret.TrimEnd('=').Replace('+', '-').Replace('/', '_');

            return base64Secret;
        }

        public static bool ValidateToken(JwtSecurityTokenHandler handler, string tokenValue)
        {
            var parameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetFixedSecretKey(),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false
            };

            try
            {
                handler.ValidateToken(tokenValue, parameters, out SecurityToken validToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Compress

        public static string CompressString(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return Convert.ToBase64String(gZipBuffer);
        }

        #endregion

        #region Decompress

        public static string DecompressString(string compressedText)
        {
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }

        #endregion

        #region Simple Cryptografy

        private const string CryptPublicKey = "09VRH0uJ";
        private const string CryptSecretKey = "JAN8OZfj";

        public static string Encrypt(string textToEncrypt)
        {
            string ToReturn = "";
            byte[] secretkeyByte = Encoding.UTF8.GetBytes(CryptSecretKey);
            byte[] publickeybyte = Encoding.UTF8.GetBytes(CryptPublicKey);
            MemoryStream ms = null;
            CryptoStream cs = null;
            byte[] inputbyteArray = Encoding.UTF8.GetBytes(textToEncrypt);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                cs.FlushFinalBlock();
                ToReturn = Convert.ToBase64String(ms.ToArray());
            }

            return CompressString(ToReturn);
        }

        public static string Decrypt(string textToDecrypt)
        {
            textToDecrypt = DecompressString(textToDecrypt);

            string ToReturn = "";
            byte[] privatekeyByte = Encoding.UTF8.GetBytes(CryptSecretKey);
            byte[] publickeybyte = Encoding.UTF8.GetBytes(CryptPublicKey);
            MemoryStream ms = null;
            CryptoStream cs = null;
            byte[] inputbyteArray = new byte[textToDecrypt.Replace(" ", "+").Length];
            inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                ToReturn = encoding.GetString(ms.ToArray());
            }

            return ToReturn;
        }

        #endregion
    }

}
