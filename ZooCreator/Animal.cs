using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooCreator
{
    class Animal
    {
        public Animal(string name, decimal price, int quantity, decimal dailyCost, int attractionValue)
        {
            this.Name = name;
            this.Price = price;
            this.Quantity = quantity;
            this.DailyCost = dailyCost;
            this.AttractionValue = attractionValue;
            this.Location = "";
        }

        private string name;
        public string Name
        {
            get { return name; }
            private set
            {
                name = value;
            }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            private set
            {
                price = value;
            }
        }

        public int Quantity
        { get; set; }

        private decimal dailyCost;
        public decimal DailyCost
        {
            get { return dailyCost; }
            private set
            {
                dailyCost = value;
            }
        }

        private int attractionValue;
        public int AttractionValue
        {
            get { return attractionValue; }
            private set
            {
                attractionValue = value;
            }
        }

        public string Location
        { get; set; }

        public void Buy(int quantity)
        {
            this.Quantity += quantity;
        }

        public void Sell(int quantity)
        {
            if (this.Quantity - quantity >= 0)
            {
                this.Quantity -= quantity;
            }
        }
    }
}
