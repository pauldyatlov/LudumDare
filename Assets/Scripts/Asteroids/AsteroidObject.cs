using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidObject : MonoBehaviour
{
    private AsteroidParameter _parameter;

    private const float MinXRotationSpeed = 0;
    private const float MaxXRotationSpeed = 50;

    private const float MinYRotationSpeed = 0;
    private const float MaxYRotationSpeed = 50;

    private const float MinZRotationSpeed = 0;
    private const float MaxZRotationSpeed = 50;

    private Vector3 _rotationVector;

    public void Show(AsteroidParameter parameter)
    {
        _parameter = parameter;

        _rotationVector = new Vector3(UnityEngine.Random.Range(MinXRotationSpeed, MaxXRotationSpeed),
                                      UnityEngine.Random.Range(MinYRotationSpeed, MaxYRotationSpeed),
                                      UnityEngine.Random.Range(MinZRotationSpeed, MaxZRotationSpeed));
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime * _parameter.Speed);
        transform.Rotate(_rotationVector * Time.deltaTime);
    }
}