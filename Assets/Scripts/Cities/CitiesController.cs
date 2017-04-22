using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitiesController : MonoBehaviour
{
    private ActionGenerator _actionGenerator;

    private void Awake()
    {
        ActionGenerator.IncreaseCityPopulation += IncreasePopulation;
    }

    private void OnDestroy()
    {
        ActionGenerator.IncreaseCityPopulation -= IncreasePopulation;
    }

    private void IncreasePopulation(int index, int quantity)
    {
        var city = CitiesHolder.Cities.Find(x => x.Index == index);
        if (city == null)
            return;

        city.IncreasePopulation(quantity);
    }
}