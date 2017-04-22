using UnityEngine;

public class KeyControls : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;

    private void Update()
    {
        transform.Rotate(new Vector2(Input.GetAxis("Vertical"), -Input.GetAxis("Horizontal")) * _speed, Space.World);
    }
}
