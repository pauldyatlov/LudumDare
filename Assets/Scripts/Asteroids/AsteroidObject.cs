using UnityEngine;

public class AsteroidObject : MonoBehaviour
{
    private const float MinXRotationSpeed = 0;
    private const float MaxXRotationSpeed = 50;

    private const float MinYRotationSpeed = 0;
    private const float MaxYRotationSpeed = 50;

    private const float MinZRotationSpeed = 0;
    private const float MaxZRotationSpeed = 50;

    private Vector3 _rotationVector;
    private Vector3 _forwardVector;

    private float _speed;

    private void Awake()
    {
        _forwardVector = new Vector3(Vector3.left.x, Vector3.left.y, Vector3.left.z);
    }

    public void Show(float speed)
    {
        _speed = speed;

        _rotationVector = new Vector3(UnityEngine.Random.Range(MinXRotationSpeed, MaxXRotationSpeed),
                                      UnityEngine.Random.Range(MinYRotationSpeed, MaxYRotationSpeed),
                                      UnityEngine.Random.Range(MinZRotationSpeed, MaxZRotationSpeed));

        DestroyAsteroid(25f);
    }

    private void Update()
    {
        transform.position += _forwardVector * Time.deltaTime * _speed;
        transform.Rotate(_rotationVector * Time.deltaTime);
    }

    private void DestroyAsteroid(float time = 0.0f)
    {
        Destroy(gameObject, time);
    }
}