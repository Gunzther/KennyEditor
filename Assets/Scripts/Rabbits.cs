using System;
using UnityEngine;
using System.Collections.Generic;

public class Rabbits
{
    RabbitsInfo rabbitsInfo = new RabbitsInfo();
    //public List<Rabbit> rabbits1 = new List<Rabbit>();

    private int age = 1;
    private string name = "rabby";

    public void addRabbit()
    {
        Debug.Log("add rabbit");
        Rabbit rab = new Rabbit(name + age, age);
        age++;
        rabbitsInfo.rabbits.Add(rab);
    }

    public RabbitsInfo GetRabbitsInfo()
    {
        foreach (Rabbit rabbit in rabbitsInfo.rabbits)
        {
            Debug.Log(rabbit.name + ", " + rabbit.age);
        }
        return rabbitsInfo;
    }

    [Serializable]
    public class RabbitsInfo
    {
        public List<Rabbit> rabbits = new List<Rabbit>();
        public int banana = 10;

        private string ban = "haha";
    }

}

