using System;

public class MyClass
{
    
    private string name;

    
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    
    public MyClass()
    {
        name = "Constructor";
    }

    
    public MyClass(string name)
    {
        this.name = name;
    }

    
    public void PublicMethod()
    {
        Console.WriteLine($"Публічний метод. Name = {name}");
        PrivateMethod();
    }
    
    private void PrivateMethod()
    {
        Console.WriteLine("Це приватний метод");
    }
}
