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
    private Vector3 _forward;
    
    private float _speed;
    
    public void Show(Vector3 forward, float amount, float speed)
    {
        _speed = speed;
        _forward = Vector3.forward;

        _rotationVector = new Vector3(UnityEngine.Random.Range(MinXRotationSpeed, MaxXRotationSpeed),
                                      UnityEngine.Random.Range(MinYRotationSpeed, MaxYRotationSpeed),
                                      UnityEngine.Random.Range(MinZRotationSpeed, MaxZRotationSpeed));

        DestroyAsteroid(25f);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _forward, Time.deltaTime * _speed);
        transform.Rotate(_rotationVector * Time.deltaTime);

        //if (Vector3.Distance(transform.position, _forward) < Mathf.Epsilon)
        //{
        //   DestroyAsteroid();
        //}
    }

    private void DestroyAsteroid(float time = 0.0f)
    {
        Destroy(gameObject, time);
    }

    private void OnTriggerEnter(Collider col)
    {
        var astronaut = col.GetComponent<Astronaut>();

        if (astronaut != null)
        {
            astronaut.Health -= 20;

            Debug.Log("Astronaut hit!");

            DestroyAsteroid();
        }
    }
}