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

    [SerializeField] private Button _acceptButton;

    private readonly List<ScenarioButton> _buttons = new List<ScenarioButton>();
    private Action<int[]> _onClick;

    private void Awake()
    {
        _acceptButton.onClick.AddListener(Hide);
    }

    public void Show(EventsData scenario, Action<int[]> onClick)
    {
        gameObject.SetActive(true);
        ParametersCounter.ActiveTime = false;
        _acceptButton.gameObject.SetActive(false);

        _descriptionLabel.text = scenario.Textru;

        _onClick = onClick;

        CreateDecisionButton(EStupidAffectionType.Good, scenario);
        CreateDecisionButton(EStupidAffectionType.Neutral, scenario);
        CreateDecisionButton(EStupidAffectionType.Bad, scenario);
    }

    private void CreateDecisionButton(EStupidAffectionType type, EventsData scenario)
    {
        var button = Instantiate(_buttonTemplate, _buttonsContainer);

        button.Show(() =>
        {
            switch (type)
            {
                case EStupidAffectionType.Good:
                    _onClick(scenario.Agree);
                    break;
                case EStupidAffectionType.Neutral:
                    _onClick(scenario.Ignore);
                    break;
                case EStupidAffectionType.Bad:
                    _onClick(scenario.Contra);
                    break;
            }
            
            ShowResultActionDescription("CREATE TEXT AFTER DECISION!");
        },
        GetSpriteByType(type),
        scenario.Textru);
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

    private Sprite GetSpriteByType(EStupidAffectionType type)
    {
        switch (type)
        {
            case EStupidAffectionType.Good:
                return _approvalSprite;
            case EStupidAffectionType.Neutral:
                return _neutralSprite;
            case EStupidAffectionType.Bad:
                return _disapprovalSprite;
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