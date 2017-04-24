using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchGbViewer.Models
{
   public class UserGradeBook
   {
      public UserGradeBook()
      {
         Batches = new Dictionary<int, string>();
         gradebook = new List<ExamAssessment>();
      }

      public User user { get; set; }
      public Dictionary<int, string> Batches { get; set; } // gradebook ID as key, batch name as value
      public virtual List<ExamAssessment> gradebook { get; set; }
   }
}
