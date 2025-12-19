using System.IO;
using Xunit;
using Lab1.Models;
using Lab1.Serializers;

namespace Lab1.Tests
{
    public class PersonSerializerTests
    {
        [Fact]
        public void Serializer_Serialize_Works()
        {
            var person = new Person("Апельсин", "Фруктович", 20, "apelsinchik@gmail.com", "123");
            var serializer = new PersonSerializer();
            string json = serializer.SerializeToJson(person);
            var deserializedPerson = serializer.DeserializeFromJson(json);
            Assert.Equal("Апельсин", deserializedPerson.FirstName);
            Assert.Equal("Фруктович", deserializedPerson.LastName);
        }
        
        [Fact]
        public void Serializer_Deserialize_Works()
        {
            string json = @"{""FirstName"":""Малина"",""LastName"":""Ягодкова"",""Age"":22}";
            var serializer = new PersonSerializer();
            var person = serializer.DeserializeFromJson(json);
            Assert.Equal("Малина", person.FirstName);
            Assert.Equal("Ягодкова", person.LastName);
        }
        
        [Fact]
        public void Serializer_SaveLoad_Works()
        {
            var person = new Person("Помидор", "Томатович", 30, "tomato228@gmail.com", "555");
            var serializer = new PersonSerializer();
            string filename = "test123.json";
            
            serializer.SaveToFile(person, filename);
            var loaded = serializer.LoadFromFile(filename);
            
            Assert.Equal("Помидор", loaded.FirstName);
            Assert.Equal("Томатович", loaded.LastName);
            
            File.Delete(filename);
        }
    }
}