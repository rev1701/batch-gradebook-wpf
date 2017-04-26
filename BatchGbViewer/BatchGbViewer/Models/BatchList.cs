using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchGbViewer.Models
{
    public class BatchList
    {
        public BatchList()
        {
            batch = new Batch();
            Batches = new List<Batch>();
            user = new User();  
        }

        public User user { get; set; }
        public Batch batch { get; set; }
        public List<Batch> Batches { get; set; }        
    }
}
