using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchGbViewer.Models
{
   public class Batch
   {
      public string BatchID { get; set; }
      public string Name { get; set; }
      public Nullable<System.DateTime> StartDate { get; set; }
      public Nullable<int> LengthInWeeks { get; set; }

      public virtual ICollection<Roster> Rosters { get; set; }
      public virtual ICollection<ExamSetting> ExamSettings { get; set; }
   }
}
