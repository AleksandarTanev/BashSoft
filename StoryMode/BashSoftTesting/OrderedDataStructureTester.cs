namespace BashSoftTesting
{
    using System;
    using System.Collections.Generic;
    using Executor.Contracts;
    using Executor.DataStructures;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class OrderedDataStructureTester
    {
        private ISimpleOrderedBag<string> names;

        [TestInitialize]
        public void SetUp()
        {
            this.names = new SimpleSortedList<string>();
        }

        [TestMethod]
        public void TestEmptyCtor()
        {
            // Arrange
            this.names = new SimpleSortedList<string>();

            // Assert
            Assert.AreEqual(this.names.Capacity, 16);
            Assert.AreEqual(this.names.Size, 0);
        }

        [TestMethod]
        public void TestCtorWithInitialCapacity()
        {
            // Arrange
            this.names = new SimpleSortedList<string>(20);

            // Assert
            Assert.AreEqual(this.names.Capacity, 20);
            Assert.AreEqual(this.names.Size, 0);
        }

        [TestMethod]
        public void TestCtorWithAllParams()
        {
            // Arrange
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase, 30);

            // Assert
            Assert.AreEqual(this.names.Capacity, 30);
            Assert.AreEqual(this.names.Size, 0);
        }

        [TestMethod]
        public void TestCtorWithInitialComparer()
        {
            // Arrange
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase);

            // Assert
            Assert.AreEqual(this.names.Capacity, 16);
            Assert.AreEqual(this.names.Size, 0);
        }

        [TestMethod]
        public void TestAddIncreaseSize()
        {
            // Act
            this.names.Add("Nasko");

            // Assert
            Assert.AreEqual(this.names.Size, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddNullThrowsException()
        {
            // Act
            this.names.Add(null);
        }

        [TestMethod]
        public void TestAddUnsortedDataIsHeldSorted()
        {
            // Act
            this.names.Add("Rosen");
            this.names.Add("Georgi");
            this.names.Add("Balkan");

            // Assert
            string previoustElement = null;
            foreach (var name in this.names)
            {
                if (previoustElement == null)
                {
                    previoustElement = name;
                }
                else
                {
                    Assert.IsTrue(string.Compare(previoustElement, name, StringComparison.Ordinal) < 0);
                }
            }
        }

        [TestMethod]
        public void TestAddingMoreThanInitialCapacity()
        {
            // Arrange
            string fakeElement = "Gosho";
            int sizeToCHeck = 17;

            // Act
            for (int i = 0; i < sizeToCHeck; i++)
            {
                this.names.Add(fakeElement);
            }

            // Assert
            Assert.AreEqual(this.names.Size, sizeToCHeck);
            Assert.AreNotEqual(this.names.Capacity, 16);
        }

        [TestMethod]
        public void TestAddingAllFromCollectionIncreasesSize()
        {
            // Arrange
            var listOfElements = new List<string>()
            {
                "Gosho",
                "Pesho"
            };

            // Act
            this.names.AddAll(listOfElements);

            // Assert
            Assert.AreEqual(this.names.Size, listOfElements.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddingAllFromNullThrowsException()
        {
            // Act
            this.names.AddAll(null);
        }

        [TestMethod]
        public void TestAddAllKeepsSorted()
        {
            // Arrange
            var listOfElements = new List<string>()
            {
                "Rosen",
                "Georgi",
                "Balkan"
            };

            // Act
            this.names.AddAll(listOfElements);

            // Assert
            string previoustElement = null;
            foreach (var name in this.names)
            {
                if (previoustElement == null)
                {
                    previoustElement = name;
                }
                else
                {
                    Assert.IsTrue(string.Compare(previoustElement, name, StringComparison.Ordinal) < 0);
                }
            }
        }

        [TestMethod]
        public void TestRemoveValidElementRemovesSelectedOne()
        {
            // Arrange
            this.names.Add("Ivan");
            this.names.Add("Nasko");

            // Act
            this.names.Remove("Ivan");

            // Assert
            foreach (var name in this.names)
            {
                Assert.AreNotEqual(name, "Ivan");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRemovingNullThrowsException()
        {
            // Act
            this.names.Remove(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestJoinWithNull()
        {
            // Arrange
            this.names.Add("Ivan");
            this.names.Add("Nasko");

            // Act
            this.names.JoinWith(null);
        }

        [TestMethod]
        public void TestJoinWorksFine()
        {
            // Arrange
            this.names.Add("Ivan");
            this.names.Add("Nasko");

            // Act
            string result = this.names.JoinWith(", ");

            // Assert
            Assert.AreEqual(result, "Ivan, Nasko");
        }
    }
}
