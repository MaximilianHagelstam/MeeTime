using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeeTime.Models
{
    public class Meet
    {
        public int Id { get; set; }

        public string MeetName { get; set; }

        public string MeetCode { get; set; }

        public string CurrentUserId { get; set; }

        public Meet()
        {

        }
    }
}
