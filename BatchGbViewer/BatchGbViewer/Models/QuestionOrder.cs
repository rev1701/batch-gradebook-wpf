using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchGbViewer.Models
{
   public class QuestionOrder
   {
      public int QuestionNumber { get; set; }
      public string QuestionID { get; set; }
      public int QuestionOrderID { get; set; }
      public int ExamAssessmentFK { get; set; }

      public virtual QuestionAnswer QuestionAnswer { get; set; }
   }
}
