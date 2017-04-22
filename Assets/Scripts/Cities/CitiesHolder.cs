using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitiesHolder : MonoBehaviour
{
    [SerializeField] private Cities _spreadsheet;
    public static List<City> Cities;

    public static int GetRandomCityIndex()
    {
        int count = Cities.Count;
        var index = Random.Range(0, count);
       
        return index;
    }
    
    private void Awake ()
    {
        Cities = new List<City>();
        InitCities(_spreadsheet.dataArray);
    }
    
    private void InitCities(CitiesData[] citiesData)
    {
        for (int i = 0; i < citiesData.Length; i++)
        {
            var data = citiesData[i];
            var city = new City(data.INDEX, data.NAME, data.STARTPOPULATION, data.GROWTHSPEED);
            Cities.Add(city);
        }
    }
}