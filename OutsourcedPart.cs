using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InventoryMGMT
{
    public class OutsourcedPart : Part
    {
        private string companyName;
        public string CompanyName { get; set; }
        public OutsourcedPart() { }
        public OutsourcedPart(int partID, string name, int inventory, decimal price, int max, int min)
        {
            PartID = partID;
            Name = name;
            Inventory = inventory;
            Price = price.ToString();
            Max = max;
            Min = min;
        }
        public OutsourcedPart(int partID, string name, int inventory, decimal price, int max, int min, string companyName)
        {
            PartID = partID;
            Name = name;
            Inventory = inventory;
            Price = price.ToString();
            Max = max;
            Min = min;
            CompanyName = companyName;
        }
    }
}
