using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLib
{
    public class Shop
    {
        public int Id { get; set; }

        public string Address { get; set; } 

        public IEnumerable<ShopToCharacteristic> shopToCharacteristics { get; set; }

    }
}
