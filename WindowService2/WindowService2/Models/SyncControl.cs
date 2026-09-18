using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowService2.Models
{
    public class SyncControl
    {
        public int Id { get; set; }

        public DateTime? LastSyncTime { get; set; }
    }
}
