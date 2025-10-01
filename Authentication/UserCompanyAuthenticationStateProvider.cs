using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace webMyStoreApp.Authentication
{
    public class UserCompanyAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionsStorage;
        private ClaimsPrincipal _notLogged = new ClaimsPrincipal(new ClaimsIdentity());

        public UserCompanyAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionsStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return new AuthenticationState(_notLogged); 
        }

        public async Task<AuthenticationState> GetActualAuthenticationStateAsync() 
        { 
                var userSessionStorageResult = await _sessionsStorage.GetAsync<UserSession>("UserSession");
                var loginResult = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
                if (loginResult == null)
                {
                    Console.WriteLine("No user session found.");
                    return new AuthenticationState(_notLogged);
                }

                // Logging user session data
                Console.WriteLine("User session found. Creating claims...");
                Console.WriteLine($"UserId: {loginResult.UserId}");
                Console.WriteLine($"UserName: {loginResult.UserName}");
                Console.WriteLine($"Email: {loginResult.Email}");
                Console.WriteLine($"PhoneNumber: {loginResult.PhoneNumber}");

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, loginResult.UserId.ToString()),
                        new Claim(ClaimTypes.Name, loginResult.UserName),
                        new Claim(ClaimTypes.Email, loginResult.Email),
                        new Claim(ClaimTypes.MobilePhone, loginResult.PhoneNumber)
                    };
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "AuthSuccess"));
                return new AuthenticationState(claimsPrincipal);
        }

        public async Task UpdateAutenticationState(UserSession user)
        {
            ClaimsPrincipal claimsPrincipal;

            if (user != null)
            {

                await _sessionsStorage.SetAsync("UserSession", user);

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
                    };
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "AuthSuccess"));
                Console.WriteLine("User authenticated and session updated.");
            }
            else
            {
                await _sessionsStorage.DeleteAsync("UserSession");
                claimsPrincipal = _notLogged;

                Console.WriteLine("User session deleted.");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}

