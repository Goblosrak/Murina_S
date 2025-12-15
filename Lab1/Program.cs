using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("Класс Person:");
            var person = new Person
            {
                FirstName = "Иван",
                LastName = "Иванов",
                Age = 18,
                Id = "11111",
                BirthDate = new DateTime(2007, 5, 5),
                Email = "ivan@example.com",
                PhoneNumber = "+7-900-000-00-00",
                Password = "password123"
            };
            
            Console.WriteLine($"Полное имя: {person.FullName}");
            Console.WriteLine($"Взрослый?: {person.IsAdult}");
            
            Console.WriteLine("Сериализация:");
            var serializer = new PersonSerializer();
            string json = serializer.SerializeToJson(person);
            Console.WriteLine($"JSON:\n{json}");

            string filePath = "person.json";
            serializer.SaveToFile(person, filePath);
            Console.WriteLine($"Файл сохранен: {File.Exists(filePath)}");
            
            var loadedPerson = serializer.LoadFromFile(filePath);
            Console.WriteLine($"Загружено: {loadedPerson?.FullName}");
            
            await serializer.SaveToFileAsync(person, "person_async.json");
            var asyncLoaded = await serializer.LoadFromFileAsync("person_async.json");
            Console.WriteLine($"Асинхронно загружено: {asyncLoaded?.FullName}");
            
            var people = new List<Person>
            {
                person,
                new Person
                {
                    FirstName = "Соня",
                    LastName = "Мурина",
                    Age = 18,
                    Id = "22222",
                    BirthDate = new DateTime(2007, 9, 6),
                    Email = "sonya@example.com",
                    PhoneNumber = "+7-900-777-70-07",
                    Password = "password123"
                }
            };
            
            serializer.SaveListToFile(people, "people.json");
            var loadedPeople = serializer.LoadListFromFile("people.json");
            Console.WriteLine($"Загружено {loadedPeople?.Count} человек");
            
            using (var manager = new FileResourceManager("file1.txt"))
            {
                manager.OpenForWriting(FileMode.Create);
                manager.WriteLine("ААААА");
                manager.WriteLine("ПОМОГИТЕ!!!");
            }
            
            using (var manager2 = new FileResourceManager("file1.txt"))
            {
                manager2.OpenForReading();
                string content = manager2.ReadAllText();
                Console.WriteLine($"Содержимое файла:\n{content}");
                
                var info = manager2.GetFileInfo();
                Console.WriteLine($"Размер файла: {info.Length} байт");
                Console.WriteLine($"Дата создания: {info.CreationTime}");
            }
        
            try
            {
                person.Email = "invalid-email";
                Console.WriteLine("Ошибка: Валидация не сработала!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
        }
    }
}