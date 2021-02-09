using System;
using Xunit;

namespace CoreLibrary.Tests
{
    public class PublisherShould
    {
        [Fact]
        public void Publisher_Constructor_Empty_assetName_ShouldReturn_ArgumentException()
        {
            //Arrange
            //var arbitraryStock = new Publisher("RY", 14.30);

            //Act
            var ex = Assert.Throws<ArgumentException>(() => new Publisher("", 14.30));

            //Assert
            Assert.Equal("Should not be empty or null (Parameter 'assetName')", ex.Message);
        }

        [Fact]
        public void Publisher_Constructor_Negative_Value_ShouldReturn_ArgumentException()
        {
            //Arrange
            //var arbitraryStock = new Publisher("RY", 14.30);

            //Act
            var ex = Assert.Throws<ArgumentException>(() => new Publisher("RY", -1));

            //Assert
            Assert.Equal("Should not be equal or less than zero (Parameter 'value')", ex.Message);
        }

        [Fact]
        public void Publisher_UpdateValue_Negative_Value_ShouldReturn_ArgumentException()
        {
            //Arrange
            var arbitraryStock = new Publisher("RY", 14.30);

            //Act
            var ex = Assert.Throws<ArgumentException>(() => arbitraryStock.UpdateValue(0));

            //Assert
            Assert.Equal("Should not be equal or less than zero (Parameter 'value')", ex.Message);
        }
    }
}
