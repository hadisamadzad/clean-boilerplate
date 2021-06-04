using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Communal.Api.Infrastructure;
using Identity.Application.Configurations;
using Identity.Domain.Users;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Application.Helpers
{
    public static class JwtHelper
    {
        public static SecurityTokenConfig Config;

        public static string CreateJwtAccessToken(this User user) =>
            user.CreateJwt(Config.AccessTokenSecretKey, Config.AccessTokenLifetime);

        public static string CreateJwtRefreshToken(this User user) =>
            user.CreateJwt(Config.RefreshTokenSecretKey, Config.RefreshTokenLifetime);

        private static string CreateJwt(this User user, string key, TimeSpan lifetime)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.Encode()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username.ToLower())
            };

            var accessToken = new JwtSecurityToken(
                signingCredentials: credentials,
                issuer: Config.Issuer,
                audience: Config.Audience,
                claims: claims.ToArray(),
                expires: DateTime.UtcNow.Add(lifetime)
            );

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

        public static bool Validate(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;

            var isValid = true;

            var payload = new JwtSecurityTokenHandler().ReadJwtToken(token).Payload;

            // Validation rules
            if (payload == null)
                return false;

            if (payload.Iss != Config.Issuer)
                isValid = false;

            if (!payload.Aud.Contains(Config.Audience))
                isValid = false;

            if (payload.ValidTo.Add(Config.RefreshTokenLifetime) < DateTime.UtcNow)
                isValid = false;

            return isValid;
        }

        private static JwtPayload GetPayload(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;
            return !Validate(token) ? null : new JwtSecurityTokenHandler().ReadJwtToken(token).Payload;
        }

        public static string GetUsername(string token)
        {
            var payload = GetPayload(token);
            return payload.Claims.SingleOrDefault(x => x.Type == "unique_name")?.Value;
        }
    }
}