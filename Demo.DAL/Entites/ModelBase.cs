using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entites
{
    public class ModelBase
    {
        public int ID { get; set; }
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }

        public DateTime LastUpdatedOn { get; set;}

        public bool IsDeleted { get; set; }


    }
}
