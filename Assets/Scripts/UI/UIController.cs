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

    public void BeginScenario(EventsData scenario, Action<int[], int[]> onClick)
    {
        if (_scenarioPanel.gameObject.activeSelf)
        {
            Debug.Log("Scenario panel is already active");
            return;
        }

        _scenarioPanel.Show(scenario, onClick);
    }

    private void SetValue(EAffectionType type, AffectionParameters parameters)
    {
        var icon = _uiParameterIcons.FirstOrDefault(x => x._affectionType == type);
        if (icon != null)
        {
            icon.Set(parameters.CurrentCount, parameters.MaxCount);
        }
        else
        {
            Debug.LogError("<b>UIController.</b> Type of (" + type + ") is null.");
        }
    }
}