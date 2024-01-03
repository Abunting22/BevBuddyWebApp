using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BevBuddyWebApp.Shared.Models
{
    public class Bet
    {
        public int BetID { get; set; }
        public int UserID { get; set; }
        public string Bettor { get; set; } = string.Empty;
        public int Wager { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime WagerDate { get; set; }
    }
}
