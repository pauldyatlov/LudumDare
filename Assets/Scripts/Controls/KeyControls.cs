using UnityEngine;

public class KeyControls : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private Astronaut _astronaut;

    private void OnValidate()
    {
        _astronaut = GetComponent<Astronaut>();
    }

    private void Update()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(vertical) > 0.2 || Mathf.Abs(horizontal) > 0.2)
            _astronaut.Rotate(new Vector2(vertical, horizontal) * _speed);
    }
}
