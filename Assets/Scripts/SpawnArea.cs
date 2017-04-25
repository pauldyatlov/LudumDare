using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnArea : MonoBehaviour
{
    public float Radius = 0.1f;
    [SerializeField] private List<House> _houses = new List<House>();

    public Astronaut Astronaut;
    [SerializeField] private Collider _astronautCollider;

    public ParticleSystem.Particle[] Particles;

    public float NextSpawnTime;

    //private void OnValidate()
    //{
    //    Init();
    //}

    public Color Color;

    public void Init()
    {
        Astronaut = GetComponentInParent<Astronaut>();
        _astronautCollider = Astronaut.PandaCollider;
        _houseSpiral = DrawSpiral(5, Radius, 0.01f).ToArray();
    }

    private Vector2[] _houseSpiral;

    public void SpawnHouse(Vector2 point)
    {
        var mainOffset = (Vector3)point + Vector3.forward * 0.1f;

        var offset = mainOffset;
        var localPosition = transform.rotation * offset;
        var worldPosition = transform.position + localPosition;

        RaycastHit raycastInfo;

        if (_astronautCollider.Raycast(new Ray(worldPosition, -transform.forward), out raycastInfo, 20f)) {
            var house = Instantiate(Astronaut.HousePrefab);
            var main = house.GetComponent<ParticleSystem>().main;
            main.startColor = new ParticleSystem.MinMaxGradient(Color);
            SpawnHouse(_astronautCollider, raycastInfo.point + raycastInfo.normal * 0.001f, raycastInfo.normal, house);
            _houses.Add(house);
        }
    }

    public static void SpawnHouse(Collider astronaut, Ray ray, House house)
    {
        RaycastHit raycastInfo;

        if (astronaut.Raycast(ray, out raycastInfo, 20f)) {
            SpawnHouse(astronaut, raycastInfo.point, raycastInfo.normal, house);
        }
    }

    public static void SpawnHouse(Collider astronaut, Vector3 point, Vector3 normal, House house)
    {
        house.transform.SetParent(astronaut.transform, worldPositionStays: false);
        house.transform.SetPositionAndRotation(point, Quaternion.LookRotation(normal) * Quaternion.AngleAxis(90, Vector3.right));
    }

    public static IEnumerable<Vector2> DrawSpiral(int coils, float radius, float chord)
    {
        var angleMax = coils * 2 * Mathf.PI;
        var step = radius / angleMax;

        yield return Vector2.zero;

        for (var angle = chord / step; angle <= angleMax;) {
            var currentRadius = step * angle;
            var x = Mathf.Cos(angle) * Mathf.Pow(currentRadius, 1f + Random.Range(-0.2f, 0.2f));
            var y = Mathf.Sin(angle) * Mathf.Pow(currentRadius, 1f + Random.Range(-0.2f, 0.2f));
            yield return new Vector2(x, y);
            var l = chord / currentRadius;
            angle += Random.Range(l - 0.5f * l, l + 0.5f * l);
        }
    }

    public void SetPopulationCount(int count, int max)
    {
        var visualCount = (int)(Astronaut.PopulationVisualCurve.Evaluate((float)count / max) * _houseSpiral.Length);

        if (visualCount < _houses.Count) {
            for (int i = _houses.Count - 1; i >= visualCount; i--) {
                Destroy(_houses[i].gameObject);
                _houses.RemoveAt(i);
            }
        } else if (visualCount > _houses.Count) {
            for (int i = _houses.Count; i < visualCount; i++) {
                SpawnHouse(_houseSpiral[i]);
            }
        }
    }
}
