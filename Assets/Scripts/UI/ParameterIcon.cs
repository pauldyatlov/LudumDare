using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ParameterIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator _animator;

    [SerializeField] private GameObject _amountObject;
    [SerializeField] public EAffectionType _affectionType;
    [SerializeField] private Image _slider;
    [SerializeField] private Text _label;
    
    private float _memoValue;

    public void Set(float value, float maxValue)
    {
        _slider.fillAmount = value / maxValue;

        _label.text = value.ToString(CultureInfo.InvariantCulture);

        if (gameObject.activeSelf && _memoValue != value)
        {
            StartCoroutine(Co_ChangeSliderAlpha(value > _memoValue));
        }

        _memoValue = value;
    }

    private IEnumerator Co_ChangeSliderAlpha(bool green)
    {
        _animator.SetBool(green ? "Green" : "Red", true);

        yield return new WaitForSeconds(0.19f);

        _animator.SetBool(green ? "Green" : "Red", false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_amountObject != null) {
            _amountObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_amountObject != null) {
            _amountObject.SetActive(false);
        }
    }
}