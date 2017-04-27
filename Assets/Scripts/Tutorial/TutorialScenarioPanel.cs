using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScenarioPanel : MonoBehaviour
{
    [SerializeField] private Text _descriptionLabel;
    [SerializeField] private Button _acceptButton;

    private Action _onProceed;

    private void Awake()
    {
        _acceptButton.onClick.AddListener(() =>
        {
            _onProceed();
        });
    }

    public void Show(Action onProceed)
    {
        gameObject.SetActive(true);

        _onProceed = onProceed;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}