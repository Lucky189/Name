using System;
using System.Threading.Tasks;

interface IGenericInterface<T>
{
    void GenericMethod(T value);
}

interface ISimpleInterface
{
    void SimpleMethod();
}

abstract class MyAbstractClass
{
    public abstract void AbstractMethod();

    public void NormalMethod()
    {
        Console.WriteLine("Звичайний метод з абстрактного класу");
    }
}

class MyClass : MyAbstractClass, IGenericInterface<int>, ISimpleInterface
{
    public override void AbstractMethod()
    {
        Console.WriteLine("Реалізація абстрактного методу");
    }

    public void GenericMethod(int value)
    {
        Console.WriteLine($"Generic метод отримав: {value}");
    }

    public void SimpleMethod()
    {
        Console.WriteLine("Простий метод виконано");
    }
}