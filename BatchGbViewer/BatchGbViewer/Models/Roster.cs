using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchGbViewer.Models
{
   public class Roster
   {
      public int UserID { get; set; }
      public int BatchID { get; set; }
      public int Status { get; set; }

      public virtual User User { get; set; }
      public virtual StatusType StatusType { get; set; }
   }
}
