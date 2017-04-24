using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScenariosController : MonoBehaviour
{
    [SerializeField] private Events _scenarios;

    private UIController _uiController;
    private readonly List<EventsData> _passedEvents = new List<EventsData>();

    public void Init(UIController uiController)
    {
        _uiController = uiController;

        ParametersCounter.SetValue(EAffectionType.Farming,     ParametersCounter.StartFarming, 50, 1);
        ParametersCounter.SetValue(EAffectionType.Military,    ParametersCounter.StartMilitary, 50, 1);
        ParametersCounter.SetValue(EAffectionType.Insurgency,  ParametersCounter.StartInsurgency, 50, 1);
        ParametersCounter.SetValue(EAffectionType.Religion,    ParametersCounter.StartReligion, 50, 1);
        ParametersCounter.SetValue(EAffectionType.Science,     ParametersCounter.StartScience, 50, 1);
        ParametersCounter.SetValue(EAffectionType.Oxygen,      ParametersCounter.StartOxygen, ParametersCounter.StartOxygen, -1);

        ParametersCounter.SetValue(EAffectionType.Population,  ParametersCounter.StartFarming
                                                             + ParametersCounter.StartMilitary 
                                                             + ParametersCounter.StartInsurgency 
                                                             + ParametersCounter.StartReligion 
                                                             + ParametersCounter.StartReligion 
                                                             + ParametersCounter.StartScience, 2500, 1);
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
            if (scenario.EAFFECTIONTYPE == type && !_passedEvents.Contains(scenario) && !_uiController.ScenarioActive && 
                    (scenario.Breakpoint > 0 
                    ? value >= scenario.Breakpoint 
                    : value <= scenario.Breakpoint))
            {
                Debug.Log("<color=magenta><b>Scenario.</b></color> Starting scenario with type (" + type + "). Value is (" + value + "), breakpoint is (" + scenario.Breakpoint + ")");

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