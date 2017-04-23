using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<ParameterIcon> _uiParameterIcons;

    [SerializeField] private Text _populationLabel;
    [SerializeField] private Text _healthLabel;

    [SerializeField] private ScenarioPanel _scenarioPanel;

    public void Init()
    {
        ParametersCounter.OnValueChanged += SetValue;
    }

    public void BeginScenario(Scenario scenario, Action<Scenario.Decision> onClick)
    {
        _scenarioPanel.Show(scenario, onClick);
    }

    private void SetValue(EAffectionType type, AffectionParameters parameters)
    {
        _uiParameterIcons.FirstOrDefault(x => x._affectionType == type).Set(parameters.CurrentCount, parameters.MaxCount);
    }
}