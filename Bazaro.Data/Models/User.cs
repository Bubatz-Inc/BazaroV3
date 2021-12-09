using Bazaro.Data.Models.Base;
using Microsoft.AspNetCore.Identity;

namespace Bazaro.Data.Models
{
    public class User : IdentityUser, IEntity
    {
        public string AvatarUrl { get; set; }
    }
}
