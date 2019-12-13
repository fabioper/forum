using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.IdentityServer
{
    public class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "ForumAPI",
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email
                    },
                    Scopes =
                    {
                        new Scope
                        {
                            Name = "ForumAPI",
                            DisplayName = "Full Acess to Forum API data"
                        }
                    }
                }
            };


        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client_mvc",
                    ClientName = "MVC Client",
                    ClientUri = "https://localhost:44397",
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:44397/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44397" },
                    AllowedCorsOrigins = { "https://localhost:44397" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "forumapi"
                    },
                    RequireClientSecret = false,
                    RequireConsent = false
                }
            };
    }
}
