using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLib
{
    public  class ShopToCharacteristic
    {
        public int Id { get; set; }

        [ForeignKey(nameof(shop))]
        public int ShopId { get; set; }

        [ForeignKey(nameof(characteristic))]
        public int CharacteristicId { get; set; }

        public int count { get; set; }

        public Characteristic characteristic { get; set; }
        public Shop shop { get; set; }


    }
}
