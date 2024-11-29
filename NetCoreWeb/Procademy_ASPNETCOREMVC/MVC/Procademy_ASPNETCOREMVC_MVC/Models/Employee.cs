namespace Procademy_ASPNETCOREMVC_MVC.Models;

public class Employee
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int Salary { get; private set; }
    public int Age { get; private set; }

    public Employee(int id, string name, int salary, int age)
    {
        Id = id;
        Name = name;
        Salary = salary;
        Age = age;
    }
}