using System.Text.Json;
using System.Text;

public class PersonSerializer
{
    private readonly JsonSerializerOptions _options;
    
    public PersonSerializer()
    {
        _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true 
        };
    }
    
    public string SerializeToJson(Person person)
    {
        return JsonSerializer.Serialize(person, _options);
    }
    
    public Person DeserializeFromJson(string json)
    {
        return JsonSerializer.Deserialize<Person>(json, _options)!;
    }
    
    public void SaveToFile(Person person, string filePath)
    {
        string json = SerializeToJson(person);
        File.WriteAllText(filePath, json, Encoding.UTF8);
    }
    
    public Person LoadFromFile(string filePath)
    {
        string json = File.ReadAllText(filePath, Encoding.UTF8);
        return DeserializeFromJson(json);
    }
    
    public async Task SaveToFileAsync(Person person, string filePath)
    {
        string json = SerializeToJson(person);
        await File.WriteAllTextAsync(filePath, json, Encoding.UTF8);
    }
    
    public async Task<Person> LoadFromFileAsync(string filePath)
    {
        string json = await File.ReadAllTextAsync(filePath, Encoding.UTF8);
        return DeserializeFromJson(json);
    }

    public void SaveListToFile(List<Person> people, string filePath)
    {
        string json = JsonSerializer.Serialize(people, _options);
        File.WriteAllText(filePath, json, Encoding.UTF8);
    }
    
    public List<Person> LoadListFromFile(string filePath)
    {
        string json = File.ReadAllText(filePath, Encoding.UTF8);
        return JsonSerializer.Deserialize<List<Person>>(json, _options)!;
    }
}