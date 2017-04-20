using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchGbViewer
{ 
   public class Batch
   {
      public string BatchID { get; set; }
      public string Name { get; set; }
      public string Technology { get; set; }
      public DateTime StartDate { get; set; }
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

    public class BatchVM
    {
        public string Name { get; set; }
        public string Technology { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string TrainerName { get; set; }
    }

    public class BatchMapper
    {
        public BatchVM MapToBatch(Batch batch, User user)
        {
            var batchvm = new BatchVM();
            batchvm.Name = batch.Name;
            batchvm.Technology = batch.BatchID;
            batchvm.StartDate = batch.StartDate;
            batchvm.FromDate = batchvm.StartDate;
            batchvm.ToDate = batch.StartDate;
            batchvm.TrainerName = user.FName + " " + user.LName;

            return batchvm;
        }
    }
}
