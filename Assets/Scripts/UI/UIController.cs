using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image _populationSlider;
    [SerializeField] private Image _healthSlider;

    public void Init()//Action<float> onHealthChanged, Action<float> onPopulationChanged)
    {
        //onHealthChanged += ChangeHealthSliderValue;
        //onPopulationChanged += ChangePopulationSliderValue;

        _healthSlider.fillAmount = 1;
        _populationSlider.fillAmount = 1;
    }

    public void ChangeHealthSliderValue(float value)
    {
        _healthSlider.fillAmount = value / 100;
    }

    public void ChangePopulationSliderValue(float value)
    {
        _populationSlider.fillAmount = Mathf.Lerp(0, 1, value);
    }
}