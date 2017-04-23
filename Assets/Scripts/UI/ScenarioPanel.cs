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

    private readonly List<ScenarioButton> _buttons = new List<ScenarioButton>();

    public void Show(Scenario scenario, Action<Scenario.Decision> onClick)
    {
        gameObject.SetActive(true);
        ParametersCounter.ActiveTime = false;

        _descriptionLabel.text = scenario.EventDescription;

        foreach (var item in scenario.Decisions)
        {
            var decision = item;
            var button = Instantiate(_buttonTemplate, _buttonsContainer);

            button.Show(() =>
            {
                onClick(decision);
                Hide();
            }, 
            GetSpriteByType(item.PandaApprovalStatus),
            item.DecisionDescription);
            _buttons.Add(button);
        }
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