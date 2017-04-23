using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlyingGarbage : MonoBehaviour
{
    [SerializeField] private Transform _center;
    [SerializeField] private float _minDistance = 10;
    [SerializeField] private float _maxDistance = 20;
    [SerializeField] private float _minSpeed = 10;
    [SerializeField] private float _maxSpeed = 20;
    [SerializeField] private int Quantity = 3;
    public List<GameObject> Prefabs;
    private Dictionary<GameObject, GarbageDescription> _debris;
    
	private void Start ()
	{
	    if (_center == null)
            _center = transform;

        _debris = new Dictionary<GameObject, GarbageDescription>();
        CreateDebris();
	}

    private void CreateDebris()
    {
        for (int i = 0; i < Quantity; i++)
        {
            var garbage = Instantiate(GetRandomPrefab(i));
            garbage.transform.SetParent(gameObject.transform, false);
            garbage.transform.localPosition = Vector3.zero;
            SetGarbagePosition(garbage);
            var garbageDescription = new GarbageDescription(garbage.transform.position, GetRotatoryDirection(garbage.transform.localPosition), GetRandomSpeed());
            _debris.Add(garbage, garbageDescription);
        }
    }

    private void SetGarbagePosition(GameObject garbage)
    {
        garbage.transform.position  = Random.insideUnitSphere.normalized * Random.Range(_minDistance, _maxDistance) + transform.position;
    }

    private Vector3 GetRotatoryDirection(Vector3 position)
    {
        var dir = new Vector3();

        if (position.x > 0)
        {
            dir.x = (-position.y - position.z) / position.x;
            dir.y = 1;
            dir.z = 1;
        }
        else if (position.y > 0)
        {
            dir.x = 1;
            dir.y = (-position.x - position.z) / position.y;
            dir.z = 1;
        }
        else
        {
            dir.x = 1;
            dir.y = 1;
            dir.z = (-position.x - position.y) / position.z;
        }
        
        return dir;
    }

    private GameObject GetRandomPrefab(int index)
    {
        if (index < Prefabs.Count)
            return Prefabs.ElementAt(index); 

        var currentIndex = UnityEngine.Random.Range(0, Prefabs.Count);
        return Prefabs.ElementAt(currentIndex);
    }

    private float GetRandomSpeed()
    {
        return Random.Range(_minSpeed, _maxSpeed);
    }

	private void Update ()
	{
	    RotateGarbage();
	}

    private void RotateGarbage()
    {
        foreach (var garbage in _debris)
        {
            garbage.Key.transform.RotateAround(_center.position, garbage.Value.Rotation, garbage.Value.Speed * Time.deltaTime);
        }
    }

    public class GarbageDescription
    {
        public Vector3 Position { get; private set; }
        public Vector3 Rotation { get; private set; }
        public float Speed { get; private set; }

        public GarbageDescription(Vector3 position, Vector3 rotation, float speed)
        {
            Position = position;
            Rotation = rotation;
            Speed = speed;
        }
    }
}
