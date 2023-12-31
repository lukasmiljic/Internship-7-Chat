using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Data.Entities.Models
{
    public class UserChannel : MessageChannel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<GroupChannel> GroupChannels { get; set; }
    }
}
