using UnityEngine;

public class UICamera : MonoBehaviour
{
    private Transform _followingTransform;

    public Camera Camera
    {
        get { return GetComponent<Camera>(); }
    }

    public void Init(Astronaut astronaut)
    {
        _followingTransform = astronaut.transform;
    }

    private void Update()
    {
        transform.LookAt(_followingTransform, Vector3.up);
        transform.Translate(Vector3.right * Time.deltaTime * 0.08f);
    }
}