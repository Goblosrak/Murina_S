using System;
using System.Reflection;

public class Student
{
    private string firstName = "Иван";
    private string lastName = "Иванов";
    private int age = 20;
    private double grade = 4.35;
}

class Program
{
    static void Main()
    {
        Student student = new Student();
        Type type = student.GetType();
        
        FieldInfo[] fields = type.GetFields(
            BindingFlags.NonPublic | 
            BindingFlags.Instance);
        
        Console.WriteLine("Значения закрытых полей:");
        foreach (FieldInfo field in fields)
        {
            object value = field.GetValue(student);
            Console.WriteLine($"{field.Name}: {value}");
        }
        
        Console.WriteLine("Изменяем значения");
        
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(string))
            {
                field.SetValue(student, "Igor Igorevich");
            }
            else if (field.FieldType == typeof(int))
            {
                field.SetValue(student, 21);
            }
            else if (field.FieldType == typeof(double))
            {
                field.SetValue(student, 4.5);
            }
        }
        
        Console.WriteLine("Обновленные значения:");
        foreach (FieldInfo field in fields)
        {
            object value = field.GetValue(student);
            Console.WriteLine($"{field.Name}: {value}");
        }
    }
}