using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerApi.Models
{
    public class Group
    {
        public string TableId { get; set; }
        public List<string> PlayerIds { get; set; }
    }
}
