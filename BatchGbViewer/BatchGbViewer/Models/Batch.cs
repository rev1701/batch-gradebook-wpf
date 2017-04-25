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
      public string Technology { get; set; }
      
      public string Trainer { get; set; }
  
      public Nullable<System.DateTime> StartDate { get; set; }
      public Nullable<int> LengthInWeeks { get; set; }

      public virtual ICollection<Roster> Rosters { get; set; }
      public virtual ICollection<ExamSetting> ExamSettings { get; set; }
   }

    public class BatchVM
    {
        public string Name { get; set; }
        public string Technology { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public string Trainer { get; set; }
    }

    public class BatchMapper
    {
        public BatchVM MapToBatch(Batch batch, User user)
        {
            var batchvm = new BatchVM();
            batchvm.Name = batch.Name;
            batchvm.Technology = batch.BatchID;
            var c = new Batch();

            if (batch.BatchID == "WeTheBest")
            {
                batchvm.Technology = ".NET";
            }
            else if (batch.BatchID == "LetItBurn")
            {
                batchvm.Technology = "Java";
            }
            else
            {
                batchvm.Technology = "SDET";
            }
            batchvm.StartDate = batch.StartDate;
            batchvm.FromDate = batchvm.StartDate;
            batchvm.ToDate = batch.StartDate.Value.AddDays(69);
            batchvm.Trainer = user.fname + " " + user.lname;

            return batchvm;
        }
    }
}
