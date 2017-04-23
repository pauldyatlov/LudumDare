using UnityEngine;

public class UICamera : MonoBehaviour
{
    private Transform _followingTransform;

    public void Init(Astronaut astronaut)
    {
        _followingTransform = astronaut.transform;
    }

    private void Update()
    {
        transform.LookAt(_followingTransform);
        transform.Translate(Vector3.right * Time.deltaTime * 0.1f);
    }
}