using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioPanel : MonoBehaviour
{
    [SerializeField] private Text _descriptionLabel;
    [SerializeField] private ScenarioButton _buttonTemplate;
    [SerializeField] private RectTransform _buttonsContainer;

    [SerializeField] private Image _scenarioImage;

    [SerializeField] private Sprite _beginSprite;
    [SerializeField] private Sprite _farmingSprite;
    [SerializeField] private Sprite _insurgencySprite;
    [SerializeField] private Sprite _militarySprite;
    [SerializeField] private Sprite _religionSprite;
    [SerializeField] private Sprite _scienceSprite;

    [SerializeField] private Sprite _oxygenGameOverSprite;
    [SerializeField] private Sprite _farmingGameOverSprite;
    [SerializeField] private Sprite _insurgencyGameOverSprite;
    [SerializeField] private Sprite _militaryGameOverSprite;
    [SerializeField] private Sprite _religionGameOverSprite;
    [SerializeField] private Sprite _scienceGameOverSprite;

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

        switch (scenario.EAFFECTIONTYPE)
        {
            case EAffectionType.Population:
                _scenarioImage.sprite = _beginSprite;
                break;
            case EAffectionType.Farming:
                _scenarioImage.sprite = !scenario.Gameover ? _farmingSprite : _farmingGameOverSprite;
                break;
            case EAffectionType.Insurgency:
                _scenarioImage.sprite = !scenario.Gameover ? _insurgencySprite : _insurgencyGameOverSprite;
                break;
            case EAffectionType.Military:
                _scenarioImage.sprite = !scenario.Gameover ? _militarySprite : _militaryGameOverSprite;
                break;
            case EAffectionType.Religion:
                _scenarioImage.sprite = !scenario.Gameover ? _religionSprite : _religionGameOverSprite;
                break;
            case EAffectionType.Science:
                _scenarioImage.sprite = !scenario.Gameover ? _scienceSprite : _scienceGameOverSprite;
                break;
            case EAffectionType.Oxygen:
                _scenarioImage.sprite = !scenario.Gameover ? _beginSprite : _oxygenGameOverSprite;
                break;
        }

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

                    ShowResultActionDescription(GetLastScreenMessage(scenario.Agreeinstant[2], scenario.Agree[2], scenario.Agreeinstant[0], scenario.Agree[0], scenario.Agreeinstant[3], scenario.Agree[3], 
                        scenario.Agreeinstant[4], scenario.Agree[4], scenario.Agreeinstant[1], scenario.Agree[1]));
                    break;
                case EStupidAffectionType.Neutral:
                    _onClick(scenario.Ignore, scenario.Ignoreinstant);

                    ShowResultActionDescription(GetLastScreenMessage(scenario.Ignoreinstant[2], scenario.Ignore[2], scenario.Ignoreinstant[0], scenario.Ignore[0], scenario.Ignoreinstant[3], scenario.Ignore[3],
                        scenario.Ignoreinstant[4], scenario.Ignore[4], scenario.Ignoreinstant[1], scenario.Ignore[1]));
                    break;
                case EStupidAffectionType.Bad:
                    _onClick(scenario.Contra, scenario.Contrainstant);

                    ShowResultActionDescription(GetLastScreenMessage(scenario.Contrainstant[2], scenario.Contra[2], scenario.Contrainstant[0], scenario.Contra[0], scenario.Contrainstant[3], scenario.Contra[3],
                        scenario.Contrainstant[4], scenario.Contra[4], scenario.Contrainstant[1], scenario.Contra[1]));
                    break;
            }
           
        },
        GetButtonLabelByType(type, scenario));
        _buttons.Add(button);
    }

    private string GetLastScreenMessage(int farmers, int farmersIncome, int military, int militaryIncome, int religion, int religionIncome, int rebel, int rebelIncome, int science, int scienceIncome)
    {
        return                 "Parameters changed:\n" +
               (farmers > 0 ? ("<color=#DDA723FF>Farmers: " + farmers + ", </color>") : "") +
               (military > 0 ? ("<color=#8BFFADFF>Military: " + military + ", </color>") : "") +
               (religion > 0 ? ("<color=#DDD795FF>Religion: " + religion + ", </color>") : "") +
               (rebel > 0 ? ("<color=#D24A43FF>Rebels: " + rebel + ", </color>") : "") +
               (science > 0 ? ("<color=#B5A1F8FF>Science: " + science + "</color>") : "") + 
               
               "\nIncome changed:\n" +
               (farmersIncome > 0 ? ("<color=#DDA723FF>Farmers: " + farmersIncome + ", </color>") : "") +
               (militaryIncome > 0 ? ("<color=#8BFFADFF>Military: " + militaryIncome + ", </color>") : "") +
               (religionIncome > 0 ? ("<color=#DDD795FF>Religion: " + religionIncome + ", </color>") : "") +
               (rebelIncome > 0 ? ("<color=#D24A43FF>Rebels: " + rebelIncome + ", </color>") : "") +
               (scienceIncome > 0 ? ("<color=#B5A1F8FF>Science: " + scienceIncome + "</color>") : "");
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