using System.Text.Json.Serialization;

public class Person
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
    
    [JsonIgnore]
    public string? Password { get; set; }
    
    [JsonPropertyName("personId")]
    public string? Id { get; set; }
    
    [JsonInclude]
    private DateTime _birthDate;
    
    public DateTime BirthDate
    {
        get => _birthDate;
        set => _birthDate = value;
    }
    
    private string? _email;
    public string? Email
    {
        get => _email;
        set
        {
            if (!string.IsNullOrEmpty(value) && !value.Contains('@'))
                throw new ArgumentException("Email должен содержать символ '@'");
            _email = value;
        }
    }

    [JsonPropertyName("phone")]
    public string? PhoneNumber { get; set; }
    
    public string FullName => $"{FirstName} {LastName}";
    
    public bool IsAdult => Age >= 18;

    public Person() { }
    
    public Person(string firstName, string lastName, int age, string id, DateTime birthDate, string email, string phoneNumber, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Id = id;
        BirthDate = birthDate;
        Email = email; 
        PhoneNumber = phoneNumber;
        Password = password;
    }
}