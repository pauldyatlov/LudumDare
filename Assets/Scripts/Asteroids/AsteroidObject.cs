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

    private float _amount;
    private float _speed;

    public void Show(float amount, float speed)
    {
        _amount = amount;
        _speed = speed;

        _rotationVector = new Vector3(UnityEngine.Random.Range(MinXRotationSpeed, MaxXRotationSpeed),
                                      UnityEngine.Random.Range(MinYRotationSpeed, MaxYRotationSpeed),
                                      UnityEngine.Random.Range(MinZRotationSpeed, MaxZRotationSpeed));
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime * _speed);
        transform.Rotate(_rotationVector * Time.deltaTime);

        if (Vector3.Distance(transform.position, Vector3.zero) < Mathf.Epsilon)
        {
            DestroyAsteroid();
        }
    }

    private void DestroyAsteroid()
    {
        Destroy(gameObject);
    }
}