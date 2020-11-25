using System;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private DataClass data;
    private void Awake()
    {
        data = new DataClass();

        data.name = "aaaa";

        data.ships = new List<Ship>();

        var ship = new Ship();
        ship.age = 10;
        ship.type = Type.A;


        var ship2 = new Ship();
        ship2.age = 20;
        ship2.type = Type.B;

        data.ships.Add(ship);
        data.ships.Add(ship2);
    }

    private void Start()
    {
        //MyUtilities.IOUtility<DataClass>.SaveData(data, "PlayerAAA", new Action<bool>((b) =>
        //{
        //    print(b);
        //}));

        MyUtilities.IOUtility<DataClass>.LoadData("PlayerAAA", new Action<DataClass>((obj) =>
        {
            print(obj);

            DataClass data = obj;

            print(data.name);
            print(data.ships.Count);
        }));
    }
}

[System.Serializable]
public class DataClass
{
    public string name;
    public List<Ship> ships;
}

[System.Serializable]
public class Ship
{
    public int age;
    public Type type;
} 

public enum Type
{
    A,
    B
}