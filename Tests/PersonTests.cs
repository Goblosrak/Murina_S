using Xunit;
using Lab1.Models;

namespace Lab1.Tests
{
    public class PersonTests
    {
        [Fact]
        public void Person_Create_Works()
        {
            var person = new Person("Ого", "Сириусович", 77, "sirius@gmail.com", "123");
            Assert.Equal("Ого", person.FirstName);
            Assert.Equal("Сириусович", person.LastName);
        }
        
        [Fact]
        public void Person_FullName_Works()
        {
            var person = new Person("Огузок", "Шефович", 10, "spartak@gmail.com", "456");
            Assert.Equal("Огузок Шефович", person.FullName);
        }
        
        [Fact]
        public void Person_IsAdult_Works()
        {
            var adult = new Person("Ого", "Сириусович", 77, "sirius@gmail.com", "111");
            var child = new Person("Огузок", "Шефович", 10, "spartak@gmail.com", "222");
            Assert.True(adult.IsAdult);
            Assert.False(child.IsAdult);
        }
    }
}