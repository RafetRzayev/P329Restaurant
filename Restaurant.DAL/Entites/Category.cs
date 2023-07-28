using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Entites
{
    public class Category : TimeStample
    {
        public string Name { get; set; }

        public ICollection<Food>? Foods { get; set; }
    }
}
