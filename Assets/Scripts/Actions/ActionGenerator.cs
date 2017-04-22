using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGenerator : MonoBehaviour
{
    public static event Action<int, int> IncreaseCityPopulation;
    public static event Action<int, int> DecreaseCityPopulation;

    private float _populationDeltaSec;
    private float _minPopulationDelta = 1;
    private float _maxPopulationDelta = 6;
    private int _quantity = 10;
    private DateTime _lastPopulationUpdate;

    private void Update()
    {
        var now = DateTime.UtcNow;
        GenerateIncreasePopulationEvent(now);
    }

    private void GenerateIncreasePopulationEvent(DateTime now)
    {
        var nextDateTime = _lastPopulationUpdate.AddSeconds(_populationDeltaSec);

        if (now < nextDateTime || now == _lastPopulationUpdate)
            return;

        _populationDeltaSec = UnityEngine.Random.Range(_minPopulationDelta, _maxPopulationDelta);
        _lastPopulationUpdate = DateTime.UtcNow;



        IncreasePopulation();
    }

    private void IncreasePopulation()
    {
        if (IncreaseCityPopulation != null) IncreaseCityPopulation(CitiesHolder.GetRandomCityIndex(), _quantity);
    }

    private void DecreasePopulation()
    {
        if (DecreaseCityPopulation != null) DecreaseCityPopulation(CitiesHolder.GetRandomCityIndex(), _quantity);
    }
}
