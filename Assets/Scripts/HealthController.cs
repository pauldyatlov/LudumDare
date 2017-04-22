using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private CharacterHealth _characterHealth;

    public void Init(Action<float> onHealthChanged)
    {
        _characterHealth.Init(onHealthChanged);
    }
}