using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Ships
    {
        [Key]
        public int ShipID {  get; set; }
        public DateTime DateOrder { get; set; }
        public bool IsApproved { get; set; }
        public DateTime ?DateShip { get; set; }
        [ForeignKey("Books")]
        public int BookID { get; set; }
        [ForeignKey("UsersOrder")]
        public int UserOrderID { get; set; }

        [ForeignKey("UsersApprove")]
        public int ?UserApproveID { get; set; }
        public  Users UsersOrder { get; set; }
        public  Users UsersApprove { get; set; }
        public Books Books { get; set; }  
    }
}
