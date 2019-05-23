using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WAD_Application.Models
{
    public class User : IdentityUser
    {
        public string Nickname { get; set; }
        public bool IsOnline { get; set; }

        public virtual IList<UserConversation> UserConversations { get; set; }
    }
}
