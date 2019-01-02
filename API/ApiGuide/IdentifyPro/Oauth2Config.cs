using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentifyPro
{
    public static class Oauth2Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResourceResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(), //必须要添加，否则报无效的scope错误
                new IdentityResources.Profile()
            };
        }
        // scopes define the resources in your system
        public static IEnumerable<Scope> GetScopes()
        {
            return new List<Scope>
            {
                new Scope
                {
                    Name = "api1",
                    Description = "My API",
                },
                //如果想带有RefreshToken，那么必须设置：StandardScopes.OfflineAccess
             //   IdentityServerConstants.StandardScopes.OfflineAccess,
            };
        }

        // client want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
               
                new Client
                {
                    ClientId = "credt_client", //client_id
                    AllowedGrantTypes = new string[] { GrantType.ClientCredentials },
                    ClientSecrets =
                    {
                        new Secret("credt_secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        "api1",
                        "api",
                    },
                    AllowedCorsOrigins =new List<string>
                    {
                        @"https://localhost:44309"
                    }
                },
             
                new Client
                {
                 
                    ClientId = "pwd_client",
                    AllowedGrantTypes = new string[] { GrantType.ResourceOwnerPassword },
                    ClientSecrets =
                    {
                        new Secret("pwd_secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        "api1",
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                    },
                    AllowedCorsOrigins =new List<string>
                    {
                        @"https://localhost:44309"
                    }
                    //AccessTokenLifetime = 3600, //AccessToken的过期时间， in seconds (defaults to 3600 seconds / 1 hour)
                    //AbsoluteRefreshTokenLifetime = 60, //RefreshToken的最大过期时间，就算你使用了TokenUsage.OneTimeOnly模式，更新的RefreshToken最大期限也是为这个属性设置的(就是6月30日就得要过期[根据服务器时间]，你用旧的RefreshToken重新获取了新RefreshToken，新RefreshToken过期时间也是6月30日)， in seconds. Defaults to 2592000 seconds / 30 day
                    //RefreshTokenUsage = TokenUsage.OneTimeOnly,   //默认状态，RefreshToken只能使用一次，使用一次之后旧的就不能使用了，只能使用新的RefreshToken
                    //RefreshTokenUsage = TokenUsage.ReUse,   //重复使用RefreshToken，RefreshToken过期了就不能使用了
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "qwerty",
                    Password = "a123"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "aspros",
                    Password = "b123"
                },
                new TestUser
                {
                    SubjectId = "3",
                    Username = "test",
                    Password = "c123"
                }
            };
        }
        public static IEnumerable<ApiResource> GeyApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1","用户API")
            };
        }
    }
}

