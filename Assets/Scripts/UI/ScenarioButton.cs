using System;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Text _label;

    public void Show(Action onClick, Sprite sprite, string text)
    {
        gameObject.SetActive(true);

        _iconImage.sprite = sprite;

        _button.onClick.AddListener(() =>
        {
            onClick();
        });

        if (_label != null) {
            _label.text = text;
        }
    }
}