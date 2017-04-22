using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Asteroids _spreadsheet;
    [SerializeField] private Camera _camera;

    [SerializeField] private AsteroidsController _asteroidsController;

    [SerializeField] private Astronaut _astronaut;
    [SerializeField] private PopulationController _populationController;

    [SerializeField] private UIController _uiController;
    
    private void Awake()
    {
        _asteroidsController.Init(_camera, _spreadsheet);

        _astronaut.Init(DamageDealtHandler);
        _populationController.Init();

        _uiController.Init();
    }

    private void DamageDealtHandler(float health)
    {
        if (health <= 0)
        {
            Debug.LogError("Game over!");
        }
        else
        {
            _uiController.ChangeHealthSliderValue(health);
        }
    }
}