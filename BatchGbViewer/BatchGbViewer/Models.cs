using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchGbViewer
{ 
   public class Batch
   {
      public string ID { get; set; }
      public string BatchName { get; set; }
      public string Technology { get; set; }
      public DateTime StartDateFrom { get; set; }
      public DateTime StartDateTo { get; set; }
      public string Trainer { get; set; }
   }

   public class User
   {
      public int ID { get; set; }
      public string FName { get; set; }
      public string LName { get; set; }
      public string UserType { get; set; }

   }

   public class Exam
   {
      public int ID { get; set; }
      public int UserID { get; set; }
      public string Technology { get; set; }
      public double Grade { get; set; }
   }
}
