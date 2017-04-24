using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchGbViewer.Models
{
   public class ExamSetting
   {
      public int ExamSettingsID { get; set; }
      public System.DateTime StartTime { get; set; }
      public int LengthOfExamInMinutes { get; set; }
      public string ExamTemplateID { get; set; }
      public bool Editable { get; set; }
      public System.DateTime EndTime { get; set; }
      public int AllowedAttempts { get; set; }
   }
}
