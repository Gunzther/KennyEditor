using System;

[Serializable]
public class Rabbit
{
    public string name;
    public int age;

    private int i;

    public Rabbit(string name, int age)
    {
        this.name = name;
        this.age = age;
        this.i = 1;
    }
}
