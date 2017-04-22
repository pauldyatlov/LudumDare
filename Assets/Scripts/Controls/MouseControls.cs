using UnityEngine;

public class MouseControls : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;

    private void Update()
    {
        if (Input.GetMouseButton(0)) {
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0) * _speed, Space.World);
        }
    }
}
