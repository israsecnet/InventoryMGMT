using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMGMT
{
    public class Inventory
    {
        public static BindingList<Part> AllParts = new BindingList<Part>();
        public static BindingList<Product> Products = new BindingList<Product>();
    
        public static void PopulateDummyLists()
        {
            Product dummyProd1 = new(1, "Prod 1", 10, 12.00m, 5, 1);
            Product dummyProd2 = new(2, "Prod 2", 10, 8.00m, 5, 1);
            Product dummyProd3 = new(3, "Prod 3", 10, 5m, 5, 1);
            Product dummyProd4 = new(4, "Prod 4", 10, 3m, 5, 1);

            Products.Add(dummyProd1);
            Products.Add(dummyProd2);
            Products.Add(dummyProd3);
            Products.Add(dummyProd4);

            // add mach ids and comp names
            Part dummyPart1A = new InHousePart(1, "Part 1.A", 15, 15.00m, 30, 10, 9001);
            Part dummyPart1B = new InHousePart(2, "Part 1.B", 10, 12.00m, 25, 10, 9001);
            Part dummyPart2A = new InHousePart(3, "Part 2.A", 12, 10.00m, 25, 10, 9002);
            Part dummyPart2B = new InHousePart(4, "Part 2.B", 15, 5.00m, 25, 10, 9002);
            Part dummyPart3A = new OutsourcedPart(5, "Part 3.A", 15, 15.00m, 30, 10, "EvilCorp");
            Part dummyPart3B = new OutsourcedPart(6, "Part 3.B", 10, 12.00m, 25, 10, "EvilCorp");
            Part dummyPart4A = new OutsourcedPart(7, "Part 4.A", 12, 10.00m, 25, 10, "Evil LLC");
            Part dummyPart4B = new OutsourcedPart(8, "Part 4.B", 15, 5.00m, 25, 10, "Evil LLC");

            AllParts.Add(dummyPart1A);
            AllParts.Add(dummyPart1B);
            AllParts.Add(dummyPart2A);
            AllParts.Add(dummyPart2B);
            AllParts.Add(dummyPart3A);
            AllParts.Add(dummyPart3B);
            AllParts.Add(dummyPart4A);
            AllParts.Add(dummyPart4B);

            // Add parts to respective Products
            dummyProd1.AssociatedParts.Add(dummyPart1A);
            dummyProd1.AssociatedParts.Add(dummyPart1B);
            dummyProd2.AssociatedParts.Add(dummyPart2A);
            dummyProd2.AssociatedParts.Add(dummyPart2B);
            dummyProd3.AssociatedParts.Add(dummyPart3A);
            dummyProd3.AssociatedParts.Add(dummyPart3B);
            dummyProd4.AssociatedParts.Add(dummyPart4A);
            dummyProd4.AssociatedParts.Add(dummyPart4B);
        }

        public static void addProduct(Product product)
        {
            Products.Add(product);
        }

        public bool RemoveProduct(int prodID) 
        {
            bool result = false;
            foreach (Product product in Products)
            {
                if (product.ProductID == prodID)
                {
                    Products.Remove(product);
                    return result = true;
                }
                else
                {
                    MessageBox.Show("Error: Removal failed!");
                    return result = false;
                }
            }
            return result;
        }

        public static Product lookupProduct(int prodID)
        {
            foreach (Product product in Products)
            {
                if (product.ProductID == prodID)
                {
                    return product;
                }
            }
            Product tmpprod = new Product();
            return tmpprod; 
        }

        public static void updateProduct (int prodID, Product updatedProd)
        {
            foreach (Product product in Products)
            {
                if (product.ProductID == prodID)
                {
                    product.Name = updatedProd.Name;
                    product.Inventory = updatedProd.Inventory;
                    product.Price = updatedProd.Price;
                    product.Max = updatedProd.Max;
                    product.Min = updatedProd.Min;
                    product.AssociatedParts = updatedProd.AssociatedParts;
                    return;
                }
            }
        }

        public static void addPart(Part part)
        {
            AllParts.Add(part);
        }
        public bool deletePart(Part part)
        {
            bool result = false;
            foreach(Part part2 in AllParts)
            {
                if (part2.PartID == part.PartID)
                {
                    AllParts.Remove(part2);
                    return result = true;
                }
                else
                {
                    MessageBox.Show("Error: Part Removal Failed");
                    return result = false;
                }
            }
            return result;
        }
        
        public static Part lookupPart(int partID)
        {
            foreach(Part part in AllParts)
            {
                if(part.PartID == partID)
                {
                    return part;
                }
            }
            Part tmpPart = new InHousePart();
            return tmpPart;
        }

        public static void updateinPart(int partID, InHousePart part)
        {
            for (int i = 0; i < AllParts.Count; i++)
            {
                if (AllParts[i].GetType() == typeof(InventoryMGMT.InHousePart))
                {
                    InHousePart newPart = (InHousePart)AllParts[i];

                    if (newPart.PartID == partID)
                    {
                        newPart.Name = part.Name;
                        newPart.Inventory = part.Inventory;
                        newPart.Price = part.Price;
                        newPart.Max = part.Max;
                        newPart.Min = part.Min;
                        newPart.MachineID = part.MachineID;
                    }
                }
            }
        }

        public static void updateoutPart(int partID, OutsourcedPart part)
        {
            for (int i = 0; i < AllParts.Count; i++)
            {
                if (AllParts[i].GetType() == typeof(InventoryMGMT.OutsourcedPart))
                {
                    OutsourcedPart newPart = (OutsourcedPart)AllParts[i];

                    if (newPart.PartID == partID)
                    {
                        newPart.Name = part.Name;
                        newPart.Inventory = part.Inventory;
                        newPart.Price = part.Price;
                        newPart.Max = part.Max;
                        newPart.Min = part.Min;
                        newPart.CompanyName = part.CompanyName;
                    }
                }
            }
        }

    }
}
