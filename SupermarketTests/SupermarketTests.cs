using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupermarketNS;

namespace SupermarketTests
{
    [TestClass]
    public class SupermarketTests
    {
        [TestMethod]
        public void Receipt_BasketEmpty_Test()
        {
            // Arrange
            var supermarket = new Supermarket();

            // Act
            try
            {
                var receipt = supermarket.GenerateReceipt();
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, Supermarket.BasketEmptyMessage);
                return;
            }

            Assert.Fail("The expected exception was not thrown.");

        }

        [TestMethod]
        public void Receipt_QuantityZero_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var apple = supermarket.PriceList.Find(i => i.Name == "Apple");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = apple,
                Quantity = 0
            });

            // Act
            try
            {
                var receipt = supermarket.GenerateReceipt();
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, Supermarket.BasketHasInvalidNumberOfItemsdMessage);
                return;
            }

            Assert.Fail("The expected exception was not thrown.");

        }

        [TestMethod]
        public void Receipt_WeightZero_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var banana = supermarket.PriceList.Find(i => i.Name == "Banana");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = banana,
                Weight = 0
            });

            // Act
            try
            {
                var receipt = supermarket.GenerateReceipt();
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, Supermarket.BasketHasInvalidNumberOfItemsdMessage);
                return;
            }

            Assert.Fail("The expected exception was not thrown.");

        }

        [TestMethod]
        public void Receipt_WeightInsteadOfQuantity_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var apple = supermarket.PriceList.Find(i => i.Name == "Apple");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = apple,
                Weight = 1
            });

            // Act
            try
            {
                var receipt = supermarket.GenerateReceipt();
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, Supermarket.BasketHasInvalidNumberOfItemsdMessage);
                return;
            }

            Assert.Fail("The expected exception was not thrown.");

        }

        [TestMethod]
        public void Receipt_QuantityInsteadOfWeight_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var banana = supermarket.PriceList.Find(i => i.Name == "Banana");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = banana,
                Quantity = 1
            });

            // Act
            try
            {
                var receipt = supermarket.GenerateReceipt();
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, Supermarket.BasketHasInvalidNumberOfItemsdMessage);
                return;
            }

            Assert.Fail("The expected exception was not thrown.");

        }

        [TestMethod]
        public void Receipt_QuantityNegative_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var apple = supermarket.PriceList.Find(i => i.Name == "Apple");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = apple,
                Quantity = -1
            });

            // Act
            try
            {
                var receipt = supermarket.GenerateReceipt();
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, Supermarket.BasketHasInvalidNumberOfItemsdMessage);
                return;
            }

            Assert.Fail("The expected exception was not thrown.");

        }

        [TestMethod]
        public void Receipt_UnhandledSellByType_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var chicken = supermarket.PriceList.Find(i => i.Name == "Chicken");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = chicken,
                Quantity = 1
            });

            // Act
            try
            {
                var receipt = supermarket.GenerateReceipt();
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, Supermarket.SellByTypeNotHandledMessage);
                return;
            }

            Assert.Fail("The expected exception was not thrown.");

        }

        [TestMethod]
        public void Receipt_SingleApple_CheckQuantity_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var apple = supermarket.PriceList.Find(i => i.Name == "Apple");

            supermarket.Basket.Add(new Supermarket.Purchases { 
                Item = apple,
                Quantity = 1
            });

            // Act
            var receipt = supermarket.GenerateReceipt();

            // Assert
            int expectedQuantity = 1;
            int actualQuantity = receipt.ItemsPurchased.Find(i => i.Item.Name == "Apple").Quantity;
            Assert.AreEqual(expectedQuantity, actualQuantity, "Total not calculated correctly");

        }

        [TestMethod]
        public void Receipt_SingleApple_CheckTotals_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var apple = supermarket.PriceList.Find(i => i.Name == "Apple");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = apple,
                Quantity = 1
            });

            // Act
            var receipt = supermarket.GenerateReceipt();

            // Assert
            double expectedTotal = 1 * apple.Cost;
            double actualTotal = receipt.Total;
            Assert.AreEqual(expectedTotal, actualTotal, 0.001, "Total not calculated correctly");

        }

        [TestMethod]
        public void Receipt_MultipleApples_CheckQuantity_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var apple = supermarket.PriceList.Find(i => i.Name == "Apple");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = apple,
                Quantity = 5
            });

            // Act
            var receipt = supermarket.GenerateReceipt();

            // Assert
            int expectedQuantity = 5;
            int actualQuantity = receipt.ItemsPurchased.Find(i => i.Item.Name == "Apple").Quantity;
            Assert.AreEqual(expectedQuantity, actualQuantity, "Total not calculated correctly");

        }

        [TestMethod]
        public void Receipt_MultipleApples_CheckTotals_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var apple = supermarket.PriceList.Find(i => i.Name == "Apple");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = apple,
                Quantity = 5
            });

            // Act
            var receipt = supermarket.GenerateReceipt();

            // Assert
            double expectedTotal = 5 * apple.Cost;
            double actualTotal = receipt.Total;
            Assert.AreEqual(expectedTotal, actualTotal, 0.001, "Total not calculated correctly");

        }

        [TestMethod]
        public void Receipt_MultipleApplesMultipleTimes_CheckQuantity_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var apple = supermarket.PriceList.Find(i => i.Name == "Apple");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = apple,
                Quantity = 5
            });
            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = apple,
                Quantity = 2
            });
            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = apple,
                Quantity = 3
            });

            // Act
            var receipt = supermarket.GenerateReceipt();

            // Assert
            int expectedQuantity = 10;
            int actualQuantity = receipt.ItemsPurchased.Find(i => i.Item.Name == "Apple").Quantity;
            Assert.AreEqual(expectedQuantity, actualQuantity, "Total not calculated correctly");

        }

        [TestMethod]
        public void Receipt_MultipleApplesMultipleTimes_CheckTotals_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var apple = supermarket.PriceList.Find(i => i.Name == "Apple");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = apple,
                Quantity = 5
            });
            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = apple,
                Quantity = 2
            });
            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = apple,
                Quantity = 3
            });

            // Act
            var receipt = supermarket.GenerateReceipt();

            // Assert
            double expectedTotal = 10 * apple.Cost;
            double actualTotal = receipt.Total;
            Assert.AreEqual(expectedTotal, actualTotal, 0.001, "Total not calculated correctly");

        }

        [TestMethod]
        public void Receipt_SingleOrange_CheckQuantity_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var orange = supermarket.PriceList.Find(i => i.Name == "Orange");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = orange,
                Quantity = 1
            });

            // Act
            var receipt = supermarket.GenerateReceipt();

            // Assert
            int expectedQuantity = 1;
            int actualQuantity = receipt.ItemsPurchased.Find(i => i.Item.Name == "Orange").Quantity;
            Assert.AreEqual(expectedQuantity, actualQuantity, "Total not calculated correctly");

        }

        [TestMethod]
        public void Receipt_SingleOrange_CheckTotals_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var orange = supermarket.PriceList.Find(i => i.Name == "Orange");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = orange,
                Quantity = 1
            });

            // Act
            var receipt = supermarket.GenerateReceipt();

            // Assert
            double expectedTotal = 1 * orange.Cost;
            double actualTotal = receipt.Total;
            Assert.AreEqual(expectedTotal, actualTotal, 0.001, "Total not calculated correctly");

        }

        [TestMethod]
        public void Receipt_MultipleOranges_CheckQuantity_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var orange = supermarket.PriceList.Find(i => i.Name == "Orange");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = orange,
                Quantity = 5
            });

            // Act
            var receipt = supermarket.GenerateReceipt();

            // Assert
            int expectedQuantity = 5;
            int actualQuantity = receipt.ItemsPurchased.Find(i => i.Item.Name == "Orange").Quantity;
            Assert.AreEqual(expectedQuantity, actualQuantity, "Total not calculated correctly");

        }

        [TestMethod]
        public void Receipt_MultipleOranges_CheckTotals_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var orange = supermarket.PriceList.Find(i => i.Name == "Orange");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = orange,
                Quantity = 5
            });

            // Act
            var receipt = supermarket.GenerateReceipt();

            // Assert
            double expectedTotal = (1 * orange.DiscountCost) + (2 * orange.Cost);
            double actualTotal = receipt.Total;
            Assert.AreEqual(expectedTotal, actualTotal, 0.001, "Total not calculated correctly");

        }

        [TestMethod]
        public void Receipt_MultipleOrangesMultipleTimes_CheckQuantity_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var Orange = supermarket.PriceList.Find(i => i.Name == "Orange");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = Orange,
                Quantity = 5
            });
            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = Orange,
                Quantity = 2
            });
            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = Orange,
                Quantity = 3
            });

            // Act
            var receipt = supermarket.GenerateReceipt();

            // Assert
            int expectedQuantity = 10;
            int actualQuantity = receipt.ItemsPurchased.Find(i => i.Item.Name == "Orange").Quantity;
            Assert.AreEqual(expectedQuantity, actualQuantity, "Total not calculated correctly");

        }

        [TestMethod]
        public void Receipt_MultipleOrangesMultipleTimes_CheckTotals_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var Orange = supermarket.PriceList.Find(i => i.Name == "Orange");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = Orange,
                Quantity = 5
            });
            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = Orange,
                Quantity = 2
            });
            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = Orange,
                Quantity = 3
            });

            // Act
            var receipt = supermarket.GenerateReceipt();

            // Assert
            double expectedTotal = (3 * Orange.DiscountCost) + (1 * Orange.Cost);
            double actualTotal = receipt.Total;
            Assert.AreEqual(expectedTotal, actualTotal, 0.001, "Total not calculated correctly");

        }

        [TestMethod]
        public void Receipt_SingleBanana_CheckWeight_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var banana = supermarket.PriceList.Find(i => i.Name == "Banana");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = banana,
                Weight = 1
            });

            // Act
            var receipt = supermarket.GenerateReceipt();

            // Assert
            double expectedWeight = 1;
            double actualWeight = receipt.ItemsPurchased.Find(i => i.Item.Name == "Banana").Weight;
            Assert.AreEqual(expectedWeight, actualWeight, 0.001, "Total not calculated correctly");

        }

        [TestMethod]
        public void Receipt_SingleBanana_CheckTotals_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var banana = supermarket.PriceList.Find(i => i.Name == "Banana");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = banana,
                Weight = 1
            });

            // Act
            var receipt = supermarket.GenerateReceipt();

            // Assert
            double expectedTotal = 1 * banana.Cost;
            double actualTotal = receipt.Total;
            Assert.AreEqual(expectedTotal, actualTotal, 0.001, "Total not calculated correctly");

        }

        [TestMethod]
        public void Receipt_MultipleBananas_CheckWeight_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var banana = supermarket.PriceList.Find(i => i.Name == "Banana");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = banana,
                Weight = 5
            });

            // Act
            var receipt = supermarket.GenerateReceipt();

            // Assert
            double expectedWeight = 5;
            double actualWeight = receipt.ItemsPurchased.Find(i => i.Item.Name == "Banana").Weight;
            Assert.AreEqual(expectedWeight, actualWeight, 0.001, "Total not calculated correctly");

        }

        [TestMethod]
        public void Receipt_MultipleBananas_CheckTotals_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var banana = supermarket.PriceList.Find(i => i.Name == "Banana");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = banana,
                Weight = 5
            });

            // Act
            var receipt = supermarket.GenerateReceipt();

            // Assert
            double expectedTotal = 5 * banana.Cost;
            double actualTotal = receipt.Total;
            Assert.AreEqual(expectedTotal, actualTotal, 0.001, "Total not calculated correctly");

        }

        [TestMethod]
        public void Receipt_MultipleBananasMultipleTimes_CheckWeight_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var banana = supermarket.PriceList.Find(i => i.Name == "Banana");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = banana,
                Weight = 5
            });
            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = banana,
                Weight = 2
            });
            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = banana,
                Weight = 3
            });

            // Act
            var receipt = supermarket.GenerateReceipt();

            // Assert
            double expectedWeight = 10;
            double actualWeight = receipt.ItemsPurchased.Find(i => i.Item.Name == "Banana").Weight;
            Assert.AreEqual(expectedWeight, actualWeight, 0.001, "Total not calculated correctly");

        }

        [TestMethod]
        public void Receipt_MultipleBananasMultipleTimes_CheckTotals_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var banana = supermarket.PriceList.Find(i => i.Name == "Banana");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = banana,
                Weight = 5
            });
            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = banana,
                Weight = 2
            });
            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = banana,
                Weight = 3
            });

            // Act
            var receipt = supermarket.GenerateReceipt();

            // Assert
            double expectedTotal = 10 * banana.Cost;
            double actualTotal = receipt.Total;
            Assert.AreEqual(expectedTotal, actualTotal, 0.001, "Total not calculated correctly");

        }

        [TestMethod]
        public void Receipt_MultipleItems_CheckTotals_Test()
        {
            // Arrange
            var supermarket = new Supermarket();
            var apple = supermarket.PriceList.Find(i => i.Name == "Apple");
            var orange = supermarket.PriceList.Find(i => i.Name == "Orange");
            var banana = supermarket.PriceList.Find(i => i.Name == "Banana");

            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = apple,
                Quantity = 1
            });
            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = orange,
                Quantity = 1
            });
            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = banana,
                Weight = 1
            });
            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = apple,
                Quantity = 3
            });
            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = orange,
                Quantity = 12
            });
            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = banana,
                Weight = 0.5
            });
            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = apple,
                Quantity = 6
            });
            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = orange,
                Quantity = 7
            });
            supermarket.Basket.Add(new Supermarket.Purchases
            {
                Item = banana,
                Weight = 3
            });

            // Act
            var receipt = supermarket.GenerateReceipt();

            // Assert
            int discountedOranges = 6;
            int fullpriceOranges = 2;
            double expectedTotal = (10 * apple.Cost) + (discountedOranges * orange.DiscountCost) + (fullpriceOranges * orange.Cost) + (4.5 * banana.Cost);
            double actualTotal = receipt.Total;
            Assert.AreEqual(expectedTotal, actualTotal, 0.001, "Total not calculated correctly");

        }


    }
}
