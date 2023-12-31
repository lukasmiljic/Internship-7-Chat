using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Data.Entities.Models
{
    public class GroupChannel : MessageChannel
    {
        public string Title { get; set; }
        public ICollection<UserChannel> Users { get; set; }
    }
}
