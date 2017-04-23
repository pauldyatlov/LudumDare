using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class House : MonoBehaviour
{
    [Serializable]
    public class HousePart
    {
        public GameObject GameObject;
        public float Length;
    }

    [SerializeField] private HousePart _main;
    [SerializeField] private HousePart _roof;
    [SerializeField] private HousePart[] _fills;

    [SerializeField] private List<HousePart> _fillParts;

    [SerializeField] private int _currentStage;

    [SerializeField] private float NextGrowthTime;

    //private void Update()
    //{
    //    if (Time.time >= NextGrowthTime) {
    //        NextGrowthTime = Time.time + Random.Range(2f, 4f);
    //        SetStage(_currentStage + 1);
    //    }
    //}

    public void SetStage(int stage)
    {
        if (stage < _currentStage) {
            Debug.LogError("Cannot set stage " + stage + " when at stage " + _currentStage);
            return;
        }

        var currentLength = _main.Length;

        foreach (var fillPart in _fillParts)
            currentLength += fillPart.Length;

        for (int i = _currentStage; i < stage; i++) {
            var fillPart = _fills[Random.Range(0, _fills.Length)];
            var fill = Instantiate(fillPart.GameObject);
            fill.transform.SetParent(transform, worldPositionStays: false);
            fill.transform.localPosition = new Vector3(0, currentLength, 0);
            fill.SetActive(true);
            _fillParts.Add(fillPart);
            currentLength += fillPart.Length;
        }

        _roof.GameObject.transform.localPosition = new Vector3(0, currentLength, 0);
        _currentStage = stage;
    }
}
