using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Astronaut : MonoBehaviour
{
    public List<SpawnArea> SpawnAreas = new List<SpawnArea>();
    public float Health = 1f;

    public House HousePrefab;

    public Vector3 CurrentRotationSpeed;

    private void OnValidate()
    {
        SpawnAreas = GetComponentsInChildren<SpawnArea>().ToList();
    }

    private void OnGUI()
    {
        GUILayout.Label("Health: " + Health);
    }

    public void Rotate(Vector3 rotationDelta)
    {
        CurrentRotationSpeed = Vector3.ClampMagnitude(rotationDelta, 6f);
    }

    private void Update()
    {
        CurrentRotationSpeed = Vector3.MoveTowards(CurrentRotationSpeed, Vector3.zero, 0.03f);
        transform.Rotate(CurrentRotationSpeed, Space.World);
    }
}
