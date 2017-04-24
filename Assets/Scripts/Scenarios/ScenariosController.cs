using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScenariosController : MonoBehaviour
{
    [SerializeField] private Events _scenarios;

    private UIController _uiController;
    private List<EventsData> _passedEvents = new List<EventsData>();

    public void Init(UIController uiController)
    {
        _uiController = uiController;

        ParametersCounter.SetValue(EAffectionType.Farming,    10, 50, 1);
        ParametersCounter.SetValue(EAffectionType.Military,   10, 50, 1);
        ParametersCounter.SetValue(EAffectionType.Insurgency,     10, 50, 1);
        ParametersCounter.SetValue(EAffectionType.Religion,  10, 50, 1);
        ParametersCounter.SetValue(EAffectionType.Science, 10, 50, 1);
        ParametersCounter.SetValue(EAffectionType.Oxygen,    100, 100, -1);
        ParametersCounter.SetValue(EAffectionType.Population, 50, 1000, 1);
    }

    public void ChangeParameter(EAffectionType type, int value, int maxValue, int income, bool displayLog = true)
    {
        if (_uiController == null) return;

        if (type != EAffectionType.Oxygen) {
            maxValue = ParametersCounter.GetPopulationSum();
        }

        ParametersCounter.SetValue(type, value, maxValue, income, displayLog);

        foreach (var scenario in _scenarios.dataArray.Where(x => !_passedEvents.Contains(x)))
        {
            if (scenario.EAFFECTIONTYPE == type && !_passedEvents.Contains(scenario) &&
                    (scenario.Breakpoint > 0 
                    ? value >= scenario.Breakpoint 
                    : value <= scenario.Breakpoint))
            {
                _uiController.BeginScenario(scenario, OnButtonClick);

                _passedEvents.Add(scenario);

                return;
            }
        }
    }

    private static void OnButtonClick(int[] income, int[] instant)
    {
        for (var i = 0; i < income.Length; i++)
        {
            var info = ParametersCounter.GetValue((EAffectionType)i);

            ParametersCounter.UpdateIncome((EAffectionType)i, info.Income + income[i]);
        }

        for (var i = 0; i < income.Length; i++)
        {
            var info = ParametersCounter.GetValue((EAffectionType)i);

            ParametersCounter.SetValue((EAffectionType)i, info.CurrentCount + instant[i], info.MaxCount, info.Income);
        }
    }
}