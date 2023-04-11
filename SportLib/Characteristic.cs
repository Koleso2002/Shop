using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLib
{
    public class Characteristic
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(300)]
        public string Name { get; set; }

        [Range(0,900)]
        public decimal Price { get; set; }

        [MaxLength(300)]
        public string Color { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public IEnumerable<ShopToCharacteristic> shopToCharacteristics { get; set; }

    }
}
