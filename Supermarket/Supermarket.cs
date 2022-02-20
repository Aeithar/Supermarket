using System;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketNS
{
    public class Supermarket
    {
        public enum sellBy
        {
            Item = 1,
            Weight = 2,
            TestCase = 99
        }

        public class ItemForSale
        {
            public string Name;
            public sellBy SellBy;
            public double Cost;
            public int DiscountFrequency;
            public double DiscountCost;
        }

        public class Purchases
        {
            public ItemForSale Item;
            public int Quantity;
            public double Weight;
        }

        public class Receipt
        {
            public List<Purchases> ItemsPurchased;
            public double Total;

            public Receipt()
            {
                ItemsPurchased = new List<Purchases>();
                Total = 0;
            }
        }

        private List<ItemForSale> _priceList = new List<ItemForSale>();
        private List<Purchases> _basket = new List<Purchases>();

        public List<ItemForSale> PriceList
        {
            get { return _priceList; }
            set { _priceList = value; }
        }

        public List<Purchases> Basket
        {
            get { return _basket; }
            set { _basket = value; }
        }

        public const string SellByTypeNotHandledMessage = "Sell By type not handled";
        public const string BasketEmptyMessage = "Basket is empty";
        public const string BasketHasInvalidNumberOfItemsdMessage = "Basket has invalid number of items";

        public Supermarket()
        {
            SetPrices();
            _basket = new List<Purchases>();
        }

        public static void Main()
        {

        }

        private void SetPrices()
        {
            _priceList = new List<ItemForSale>();
            _priceList.Add(new ItemForSale
            {
                Name = "Apple",
                SellBy = sellBy.Item,
                Cost = 0.65
            });
            _priceList.Add(new ItemForSale
            {
                Name = "Orange",
                SellBy = sellBy.Item,
                Cost = 0.40,
                DiscountFrequency = 3,
                DiscountCost = 1.00
            });
            _priceList.Add(new ItemForSale
            {
                Name = "Banana",
                SellBy = sellBy.Weight,
                Cost = 0.75
            });
            _priceList.Add(new ItemForSale
            {
                Name = "Chicken",
                SellBy = sellBy.TestCase,
                Cost = 4.50
            });
        }

        private bool ValidateBasket()
        {
            if (_basket.Count == 0)
            {
                throw new ArgumentOutOfRangeException("Count", _basket.Count, BasketEmptyMessage);
            }
            foreach (var purchase in _basket)
            {
                if (purchase.Quantity <= 0 && purchase.Item.SellBy == sellBy.Item)
                {
                    throw new ArgumentOutOfRangeException("Quantity", purchase.Quantity, BasketHasInvalidNumberOfItemsdMessage);
                }
                if (purchase.Weight <= 0 && purchase.Item.SellBy == sellBy.Weight)
                {
                    throw new ArgumentOutOfRangeException("Quantity", purchase.Quantity, BasketHasInvalidNumberOfItemsdMessage);
                }
                if (purchase.Quantity > 0 && purchase.Item.SellBy == sellBy.Weight)
                {
                    throw new ArgumentOutOfRangeException("Quantity", purchase.Quantity, BasketHasInvalidNumberOfItemsdMessage);
                }
                if (purchase.Weight > 0 && purchase.Item.SellBy == sellBy.Item)
                {
                    throw new ArgumentOutOfRangeException("Quantity", purchase.Quantity, BasketHasInvalidNumberOfItemsdMessage);
                }
            }
            return true;
        }

        private void CompactBasket()
        {
            // Combine multiple copies of the same item into a single copy with a summed Quantity or Weight

            List<Purchases> compactedBasket = new List<Purchases>();

            foreach (var item in _priceList)
            {
                var quantity = _basket.Where(b => b.Item == item).Sum(i => i.Quantity);
                var weight = _basket.Where(b => b.Item == item).Sum(i => i.Weight);
                if (quantity > 0 || weight > 0)
                {
                    compactedBasket.Add(new Purchases
                    {
                        Item = item,
                        Quantity = quantity,
                        Weight = weight
                    });
                }
            }

            _basket = compactedBasket;
        }

        public Receipt GenerateReceipt()
        {
            var basketValid = false;
            try
            {
                basketValid = ValidateBasket();
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException(ex.Message);
            }
            CompactBasket();
            var receipt = new Receipt();
            receipt.ItemsPurchased.AddRange(_basket);
            receipt.Total = Total();
            return receipt;
        }

        private double Total()
        {
            double total = 0d;

            foreach (var item in _basket)
            {
                switch (item.Item.SellBy)
                {
                    case sellBy.Item:
                        {
                            if (item.Item.DiscountFrequency > 0)
                            {
                                var discounted = Math.Floor((double)item.Quantity / item.Item.DiscountFrequency);
                                var fullPrice = item.Quantity % item.Item.DiscountFrequency;
                                total += (discounted * item.Item.DiscountCost) + (fullPrice * item.Item.Cost);
                            }
                            else
                            {
                                total += (item.Quantity * item.Item.Cost);
                            }
                            break;
                        }
                    case sellBy.Weight:
                        {
                            total += (item.Weight * item.Item.Cost);
                            break;
                        }
                    default:
                        {
                            throw new ArgumentOutOfRangeException("SellBy", item.Item.SellBy, SellByTypeNotHandledMessage);
                        }
                }
            }

            return total;
        }


    }
}