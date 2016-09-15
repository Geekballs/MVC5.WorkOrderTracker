using System;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace App.Web.Lib.Managers
{
    /// <summary>
    /// Active Directory authentication manager which can be called before the IService* pipeline has been invoked.
    /// </summary>
    public class ActiveDirectoryAuthenticationManager
    {
        /// <summary>
        /// Initialize the ApplicationAuthenticationManager
        /// </summary>
        private readonly ApplicationAuthenticationManager _aam = new ApplicationAuthenticationManager();

        /// <summary>
        /// Return the authentication result responses.
        /// </summary>
        public class AuthenticationResult
        {
            public AuthenticationResult(string errMessage = null)
            {
                ErrorMessage = errMessage;
            }
            public string ErrorMessage { get; private set; }
            public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly IAuthenticationManager _authManager;

        public ActiveDirectoryAuthenticationManager(IAuthenticationManager authManager)
        {
            _authManager = authManager;
        }

        public AuthenticationResult SignIn(string username, string password)
        {
            #if DEBUG
            var authType = ContextType.Machine;

            #else
            // authenticates against your Domain AD
            var authType = ContextType.Domain;

            #endif
            var principalCtx = new PrincipalContext(authType);
            var isAuthed = false;
            UserPrincipal userPrincipal = null;
            try
            {
                // Check the authorization context parameters (username, password etc).
                isAuthed = principalCtx.ValidateCredentials(username, password, ContextOptions.Negotiate);

                // Check if the request authenticated and is enabled in the application.
                if (isAuthed && ApplicationAuthenticationManager.GetUserByName(username).LoginEnabled)
                {
                    userPrincipal = UserPrincipal.FindByIdentity(principalCtx, username);
                }
            }
            catch (Exception)
            {
                isAuthed = false;
                userPrincipal = null;
            }

            // The request has been authenticated, but they are not enabled in this application.
            if (isAuthed && !ApplicationAuthenticationManager.GetUserByName(username).LoginEnabled)
            {
                return new AuthenticationResult("Unauthorized Application Access!");
            }

            // Not authenticated.
            if (!isAuthed || userPrincipal == null)
            {
                return new AuthenticationResult("Incorrect Credentials!");
            }

            // Active Directory account is locked out.
            //TODO: Should we be exposing this?
            if (userPrincipal.IsAccountLockedOut())
            {
                return new AuthenticationResult("AD Account Locked!");
            }

            // Active Directory account disabled.
            //TODO: Should we be exposing this?
            if (userPrincipal.Enabled.HasValue && userPrincipal.Enabled.Value == false)
            {
                return new AuthenticationResult("AD Account Disabled!");
            }

            var identity = CreateIdentity(userPrincipal);
            _authManager.SignOut(MyAuthentication.ApplicationCookie);
            _authManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
            return new AuthenticationResult();
        }

        /// <summary>
        /// Claim properties.
        /// </summary>
        /// <param name="userPrincipal"></param>
        /// <returns></returns>
        private ClaimsIdentity CreateIdentity(UserPrincipal userPrincipal)
        {
            var identity = new ClaimsIdentity(MyAuthentication.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "Active Directory"));
            identity.AddClaim(new Claim(ClaimTypes.Name, userPrincipal.SamAccountName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userPrincipal.SamAccountName));
            if (!string.IsNullOrEmpty(userPrincipal.EmailAddress))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, userPrincipal.EmailAddress));
            }

            var user = ApplicationAuthenticationManager.GetUserByName(userPrincipal.SamAccountName);
            var userRoles = ApplicationAuthenticationManager.GetRolesForUser(user.SystemUserId);

            // Add all of the system roles that have been assigned to the system user.
            foreach (var role in userRoles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.SystemRole.Name));
            }

            return identity;
        }
    }
}