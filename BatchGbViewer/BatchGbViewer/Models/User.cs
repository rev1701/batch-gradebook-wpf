using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchGbViewer.Models
{
   public class User
   {
      public string fname { get; set; }
      public string lname { get; set; }
      public int UserType { get; set; }
      public string email { get; set; }
      public virtual ICollection<ExamSetting> ExamSettings { get; set; }
      public virtual UserType UserType1 { get; set; }
   }
}
