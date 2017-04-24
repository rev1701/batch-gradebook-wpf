using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchGbViewer.Models
{
   public class Grade
   {
      public string fname { get; set; }
      public string lname { get; set; }
      public string email { get; set; }
      public string batchName { get; set; }
      public string technology { get; set; }
      public string examName { get; set; }
      public Nullable<decimal> Score { get; set; }
   }
}
