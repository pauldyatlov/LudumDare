using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Astronaut : MonoBehaviour
{
    public List<SpawnArea> SpawnAreas = new List<SpawnArea>();

    public House HousePrefab;
    public Vector3 CurrentRotationSpeed;

    private Action<float> _onHealthChanged;

    private float _health;
    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;

            _onHealthChanged.Invoke(_health);
        }
    }

    private void OnValidate()
    {
        SpawnAreas = GetComponentsInChildren<SpawnArea>().ToList();
    }

    public void Init(Action<float> onHealthChanged)
    {
        _onHealthChanged = onHealthChanged;
        Health = 100;
    }

    public void Rotate(Vector3 rotationDelta)
    {
        CurrentRotationSpeed = Vector3.ClampMagnitude(rotationDelta, 6f);
    }

    private void Update()
    {
        CurrentRotationSpeed = Vector3.MoveTowards(CurrentRotationSpeed, Vector3.zero, 0.03f);
        transform.Rotate(CurrentRotationSpeed, Space.World);

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Health -= 5;
        }
    }
}