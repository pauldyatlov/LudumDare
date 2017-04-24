using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsteroidsController : MonoBehaviour
{
    [SerializeField] private List<string> _asteroidPresets;

    [Range(0.0f, 99.0f)]  [SerializeField] private float _asteroidMinSpawnDelay;
    [Range(1.0f, 100.0f)] [SerializeField] private float _asteroidMaxSpawnDelay;

    private DateTime _lastUpdate;
    
    private readonly List<AsteroidObject> _asteroids = new List<AsteroidObject>();

    private Camera _camera;
    private Asteroids _spreadsheet;
    
    private float _delay;

    private bool _active;

    public void Init(Camera cam, Asteroids spreadsheet)
    {
        _camera = cam;
        _spreadsheet = spreadsheet;

        _active = true;
    }

    private void Update()
    {
        if (!_active) return;

        var now = DateTime.UtcNow;
        var nextDateTime = _lastUpdate.AddSeconds(_delay);

        if (now < nextDateTime || now == _lastUpdate)
            return;

        _delay = UnityEngine.Random.Range(_asteroidMinSpawnDelay, _asteroidMaxSpawnDelay);
        _lastUpdate = DateTime.UtcNow;
        
        CreateAsteroid();
    }

    private void CreateAsteroid()
    {
        var index = UnityEngine.Random.Range(0, _asteroidPresets.Count);

        var preset = _spreadsheet.FindByKey(_asteroidPresets[index]);

        var pos = new Vector3(UnityEngine.Random.Range(-20, 20), 
                              UnityEngine.Random.Range(-20, 20), 
                              UnityEngine.Random.Range(_camera.transform.position.z - 1, 1));

        var prefab = Resources.Load<AsteroidObject>("Asteroids/Asteroid" + preset.PREFAB);

        var asteroid = Instantiate(prefab, pos, Quaternion.identity, transform);

        asteroid.Show(preset.SPEED);

        _asteroids.Add(asteroid);
    }
}