using SimpleBanking.API.Models;

namespace SimpleBanking.API.Tests.Models
{
    public class EventTests
    {
        [Fact]
        public void Event_ShouldHaveType()
        {
            var eventInstance = new Event();

            eventInstance.Type = "transfer";

            Assert.NotNull(eventInstance.Type);
            Assert.Equal("transfer", eventInstance.Type);
        }

        [Fact]
        public void Event_ShouldHaveOrigin()
        {
            var eventInstance = new Event();

            eventInstance.Origin = "12345";

            Assert.NotNull(eventInstance.Origin);
            Assert.Equal("12345", eventInstance.Origin);
        }

        [Fact]
        public void Event_ShouldHaveDestination()
        {
            var eventInstance = new Event();

            eventInstance.Destination = "67890";

            Assert.NotNull(eventInstance.Destination);
            Assert.Equal("67890", eventInstance.Destination);
        }

        [Fact]
        public void Event_ShouldHaveAmount()
        {
            var eventInstance = new Event();

            eventInstance.Amount = 1000;

            Assert.Equal(1000, eventInstance.Amount);
        }

        [Fact]
        public void Event_ShouldHandleNegativeAmount()
        {
            var eventInstance = new Event();

            eventInstance.Amount = -500;

            Assert.Equal(-500, eventInstance.Amount);
        }
    }
}