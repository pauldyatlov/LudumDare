using UnityEngine;

public class MouseControls : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private Astronaut _astronaut;

    private void OnValidate()
    {
        _astronaut = GetComponent<Astronaut>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            _astronaut.Rotate(Camera.main.transform.rotation * new Vector2(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X")) * _speed);
    }
}
