using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public float Radius = 0.1f;
    [SerializeField] private List<House> _houses;

    public Vector3 Normal;

    public Astronaut Astronaut;
    [SerializeField] private Collider _astronautCollider;

    public float NextSpawnTime;

    private void OnValidate()
    {
        Init();
    }

    public void Init()
    {
        Astronaut = GetComponentInParent<Astronaut>();
        _astronautCollider = Astronaut.GetComponent<Collider>();
    }

    private void Update()
    {
        if (Time.time >= NextSpawnTime) {
            NextSpawnTime = Time.time + Random.Range(2f, 4f);
            SpawnHouse();
        }
    }

    [ContextMenu("Spawn house")]
    public void SpawnHouse()
    {
        var offset = (Vector3)(Random.insideUnitCircle * Radius) + Vector3.forward * 1f;
        var localPosition = transform.rotation * offset;
        var worldPosition = transform.position + localPosition;

        RaycastHit raycastInfo;

        if (_astronautCollider.Raycast(new Ray(worldPosition, -Normal), out raycastInfo, 20f)) {
            var house = Instantiate(Astronaut.HousePrefab);
            SpawnHouse(_astronautCollider, raycastInfo.point, raycastInfo.normal, house.gameObject);
            _houses.Add(house);
        }
    }

    public static void SpawnHouse(Collider astronaut, Ray ray, GameObject house)
    {
        RaycastHit raycastInfo;

        if (astronaut.Raycast(ray, out raycastInfo, 20f)) {
            SpawnHouse(astronaut, raycastInfo.point, raycastInfo.normal, house);
        }
    }

    public static void SpawnHouse(Collider astronaut, Vector3 point, Vector3 normal, GameObject house)
    {
        house.transform.SetParent(astronaut.transform);
        house.transform.SetPositionAndRotation(point, Quaternion.LookRotation(normal) * Quaternion.AngleAxis(90, Vector3.right));
    }
}
