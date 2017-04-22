using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Asteroids _spreadsheet;
    [SerializeField] private Camera _camera;

    [SerializeField] private AsteroidsController _asteroidsController;
    
    private void Awake()
    {
        _asteroidsController.Init(_camera, _spreadsheet);
    }
}