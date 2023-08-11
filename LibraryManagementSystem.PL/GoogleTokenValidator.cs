﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagementSystem.PL
{
    public class GoogleTokenValidator : ISecurityTokenValidator
    {
        private readonly string _clientId;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public GoogleTokenValidator(string clientId)
        {
            _clientId = clientId;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public bool CanValidateToken => true;

        public int MaximumTokenSizeInBytes { get; set; } = TokenValidationParameters.DefaultMaximumTokenSizeInBytes;

        public bool CanReadToken(string securityToken)
        {
            return _tokenHandler.CanReadToken(securityToken);
        }

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            validatedToken = null;
            try
            {
                var payload = GoogleJsonWebSignature.ValidateAsync(securityToken, new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new[] { _clientId }
                }).Result; // here is where I delegate to Google to validate

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, payload.Email),
                    new Claim(ClaimTypes.Name, payload.Email),
                    new Claim(JwtRegisteredClaimNames.FamilyName, payload.FamilyName),
                    new Claim(JwtRegisteredClaimNames.GivenName, payload.GivenName),
                    new Claim(JwtRegisteredClaimNames.Email, payload.Email),
                    new Claim(JwtRegisteredClaimNames.Name, payload.Name),
                    new Claim(JwtRegisteredClaimNames.Sub, payload.Subject),
                    new Claim(JwtRegisteredClaimNames.Iss, payload.Issuer),
                };
                
                validatedToken = _tokenHandler.ReadJwtToken(securityToken);
                var principle = new ClaimsPrincipal();
                principle.AddIdentity(new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme));
                
                return principle;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}