using System;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private AsteroidsController _asteroidsController;
    [SerializeField] private Asteroids _spreadsheet;

    [SerializeField] private Astronaut _astronaut;
    [SerializeField] private ScenariosController _scenariosController;

    [SerializeField] private Gameplay _gameplayData;

    [SerializeField] private UIController _uiController;
    [SerializeField] private UICamera _uiCamera;
    
    private DateTime _lastUpdate;

    private const int CALL_DELAY = 5;

    private void Awake()
    {
        ParametersCounter.Init();

        ParametersCounter.StartMilitary = _gameplayData.dataArray.FirstOrDefault(x => x.KEY == "StartMilitary").AMOUNT;
        ParametersCounter.StartScience = _gameplayData.dataArray.FirstOrDefault(x => x.KEY == "StartScience").AMOUNT;
        ParametersCounter.StartFarming = _gameplayData.dataArray.FirstOrDefault(x => x.KEY == "StartFarming").AMOUNT;
        ParametersCounter.StartReligion = _gameplayData.dataArray.FirstOrDefault(x => x.KEY == "StartReligion").AMOUNT;
        ParametersCounter.StartInsurgency = _gameplayData.dataArray.FirstOrDefault(x => x.KEY == "StartInsurgency").AMOUNT;

        ParametersCounter.StartOxygen = _gameplayData.dataArray.FirstOrDefault(x => x.KEY == "StartOxygen").AMOUNT;

        _asteroidsController.Init(_uiCamera.Camera, _spreadsheet);
        _astronaut.Init(DamageDealtHandler);
        _uiController.Init();
        _scenariosController.Init(_uiController);

        _uiCamera.Init(_astronaut);

        ParametersCounter.ActiveTime = true;
    }

    private void DamageDealtHandler(int health)
    {
        _scenariosController.ChangeParameter(EAffectionType.Oxygen, health, ParametersCounter.StartOxygen, 1);

        if (health <= 0)
        {
            Debug.LogWarning("<b>Game over!</b>");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _scenariosController.ChangeParameter(EAffectionType.Farming, ParametersCounter.GetValue(EAffectionType.Farming).CurrentCount + 5, ParametersCounter.GetPopulationSum(), 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            _scenariosController.ChangeParameter(EAffectionType.Military, ParametersCounter.GetValue(EAffectionType.Military).CurrentCount - 1, ParametersCounter.GetPopulationSum(), 1);
        }

        if (!ParametersCounter.ActiveTime) return;

        var now = DateTime.UtcNow;
        var nextDateTime = _lastUpdate.AddSeconds(CALL_DELAY);

        if (now < nextDateTime || now == _lastUpdate)
            return;

        _lastUpdate = DateTime.UtcNow;

        Debug.Log("<b>TICK.</b>");

        foreach (EAffectionType affectionType in Enum.GetValues(typeof(EAffectionType)))
        {
            var info = ParametersCounter.GetValue(affectionType);

            _scenariosController.ChangeParameter(affectionType, info.CurrentCount + info.Income, affectionType == EAffectionType.Oxygen ? ParametersCounter.StartOxygen : ParametersCounter.GetPopulationSum(), info.Income, false);
        }
    }
}