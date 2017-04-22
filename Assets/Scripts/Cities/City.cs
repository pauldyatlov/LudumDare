using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City
{
    public static event Action<int> DestroyedCity;

    public int Index { get; private set; }
    public string Name { get; private set; }
    public int Population { get; private set; }
    public float GrowthSpeed { get; private set; }

    public City(int index, string name, int population, float growthSpeed)
    {
        Index = index;
        Name = name;
        Population = population;
        GrowthSpeed = growthSpeed;
    }

    public void IncreasePopulation(int quantity)
    {
        Population += quantity;
        Debug.Log("Population in City " + Index + " is increased by " + quantity);
    }

    public void DecreasePopulation(int quantity)
    {
        Population -= quantity;
        Debug.Log("Population in City " + Index + " is deccreased by " + quantity);

        if (Population < 0)
            DestroyCity(Index);
    }

    public void DestroyCity(int index)
    {
        if (DestroyedCity != null) DestroyedCity(index);
    }
}
