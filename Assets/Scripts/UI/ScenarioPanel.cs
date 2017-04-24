using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioPanel : MonoBehaviour
{
    [SerializeField] private Text _descriptionLabel;
    [SerializeField] private ScenarioButton _buttonTemplate;
    [SerializeField] private RectTransform _buttonsContainer;

    [SerializeField] private Sprite _approvalSprite;
    [SerializeField] private Sprite _neutralSprite;
    [SerializeField] private Sprite _disapprovalSprite;

    [SerializeField] private Text _acceptButtonLabel;
    [SerializeField] private Button _acceptButton;

    private readonly List<ScenarioButton> _buttons = new List<ScenarioButton>();
    private Action<int[], int[]> _onClick;

    private void Awake()
    {
        _acceptButton.onClick.AddListener(Hide);
    }

    public void Show(EventsData scenario, Action<int[], int[]> onClick)
    {
        gameObject.SetActive(true);
        ParametersCounter.ActiveTime = false;
        _acceptButton.gameObject.SetActive(false);

        _descriptionLabel.text = scenario.Description;

        _onClick = onClick;

        if (scenario.Gameover)
        {
            ShowResultActionDescription(scenario.Description);

            _acceptButtonLabel.text = "END";
            _acceptButton.onClick.AddListener(() =>
            {
                Application.Quit();

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            });
        }
        else
        {
            CreateDecisionButton(EStupidAffectionType.Good, scenario);
            CreateDecisionButton(EStupidAffectionType.Neutral, scenario);
            CreateDecisionButton(EStupidAffectionType.Bad, scenario);
        }
    }

    private void CreateDecisionButton(EStupidAffectionType type, EventsData scenario)
    {
        var button = Instantiate(_buttonTemplate, _buttonsContainer);

        button.Show(() =>
        {
            switch (type)
            {
                case EStupidAffectionType.Good:
                    _onClick(scenario.Agree, scenario.Agreeinstant);
                    break;
                case EStupidAffectionType.Neutral:
                    _onClick(scenario.Ignore, scenario.Ignoreinstant);
                    break;
                case EStupidAffectionType.Bad:
                    _onClick(scenario.Contra, scenario.Contrainstant);
                    break;
            }
            
            ShowResultActionDescription("CREATE TEXT AFTER DECISION!");
        },
        GetButtonLabelByType(type, scenario));
        _buttons.Add(button);
    }

    private void ShowResultActionDescription(string description)
    {
        foreach (var button in _buttons) {
            Destroy(button.gameObject);
        }

        _buttons.Clear();

        _descriptionLabel.text = description;
        _acceptButton.gameObject.SetActive(true);
    }

    private string GetButtonLabelByType(EStupidAffectionType type, EventsData scenario)
    {
        switch (type)
        {
            case EStupidAffectionType.Good:
                return scenario.Agreebuttonlabel;
            case EStupidAffectionType.Neutral:
                return scenario.Ignorebuttonlabel;
            case EStupidAffectionType.Bad:
                return scenario.Contrabuttonlabel;
            default:
                throw new ArgumentOutOfRangeException("type", type, null);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ParametersCounter.ActiveTime = true;

        foreach (var button in _buttons) {
            Destroy(button.gameObject);
        }

        _buttons.Clear();
    }
}