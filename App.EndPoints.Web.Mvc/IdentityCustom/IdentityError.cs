using Microsoft.AspNetCore.Identity;

namespace App.EndPoints.Web.Mvc.IdentityCustom
{
    public class IdentityError:IdentityErrorDescriber
    {
        public override Microsoft.AspNetCore.Identity.IdentityError InvalidEmail(string email)
        {
            return base.InvalidEmail(email);
            /*return new IdentityError()
            {
                
            };*/
        }
    }
}
