using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    private Action<float> _onHealthChanged;

    private float _health;
    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;

            _onHealthChanged.Invoke(_health);
        }
    }

    public void Init(Action<float> onHealthChanged)
    {
        _onHealthChanged = onHealthChanged;
        Health = 100;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Health -= 5;
        }
    }      
}