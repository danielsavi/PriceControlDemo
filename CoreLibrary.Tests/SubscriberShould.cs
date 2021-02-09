using System;
using Xunit;

namespace CoreLibrary.Tests
{
    public class SubscriberShould
    {
        [Fact]
        public void Subscriber_Empty_subscriberName_ShouldReturn_ArgumentException()
        {
            //Arrange
            //Subscriber sub1 = new Subscriber("", Subscriber.TriggerType.Sell, 0.3); 

            //Act
            var ex = Assert.Throws<ArgumentException>(() => new Subscriber("", Subscriber.TriggerType.Sell, 0.3));

            //Assert
            Assert.Equal("Should not be empty or null (Parameter 'subscriberName')", ex.Message);
        }


        [Fact]
        public void Subscriber_Negative_Threshold_ShouldReturn_ArgumentException()
        {
            //Arrange
            //Subscriber sub1 = new Subscriber("", Subscriber.TriggerType.Sell, 0.3); 

            //Act
            var ex = Assert.Throws<ArgumentException>(() => new Subscriber("User1", Subscriber.TriggerType.Sell, -1));

            //Assert
            Assert.Equal("Should not be less than zero (Parameter 'threshold')", ex.Message);
        }
    }
}
