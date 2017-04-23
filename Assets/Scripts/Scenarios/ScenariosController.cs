using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScenariosController : MonoBehaviour
{
    [SerializeField] private List<Scenario> _scenarios;

    private UIController _uiController;

    public void Init(UIController uiController)
    {
        _uiController = uiController;

        ParametersCounter.SetValue(EAffectionType.Farmers,    10, 50, 1);
        ParametersCounter.SetValue(EAffectionType.Military,   10, 50, 1);
        ParametersCounter.SetValue(EAffectionType.Rebels,     10, 50, 1);
        ParametersCounter.SetValue(EAffectionType.Religious,  10, 50, 1);
        ParametersCounter.SetValue(EAffectionType.Scientists, 10, 50, 1);
        ParametersCounter.SetValue(EAffectionType.Oxygen,    100, 100, -1);
        ParametersCounter.SetValue(EAffectionType.Population, 50, 1000, 1);
    }

    public void ChangeParameter(EAffectionType type, int value, int maxValue, int income)
    {
        if (_uiController == null) return;

        if (type != EAffectionType.Oxygen) {
            maxValue = ParametersCounter.GetPopulationSum();
        }

        ParametersCounter.SetValue(type, value, maxValue, income);

        foreach (var scenario in _scenarios.Where(x => !x.Passed))
        {
            if (scenario.Condition.AffectionType == type && 
                value >= scenario.Condition.Amout)
            {
                scenario.Passed = true;
                _uiController.BeginScenario(scenario, OnButtonClick);

                return;
            }
        }
    }

    private static void OnButtonClick(Scenario.Decision decision)
    {
        foreach (var affection in decision.Affections)
        {
            var info = ParametersCounter.GetValue(affection.AffectionType);

            ParametersCounter.UpdateCurrentValue(affection.AffectionType, info.CurrentCount + affection.Amout);
        }
    }
}