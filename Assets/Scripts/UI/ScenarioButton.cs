using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScenarioButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _selectedImage;
    [SerializeField] private Text _label;

    public void Show(Action onClick, string text)
    {
        gameObject.SetActive(true);

        _button.onClick.AddListener(() =>
        {
            onClick();
        });

        if (_label != null) {
            _label.text = text;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_selectedImage != null) {
            _selectedImage.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_selectedImage != null) {
            _selectedImage.SetActive(false);
        }
    }
}