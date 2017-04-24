using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EAffectionType
{
    Military,
    Science,
    Farming,
    Religion,
    Insurgency,
    Oxygen,
    Population
}

public class AffectionParameters
{
    public EAffectionType AffectionType;
    public int CurrentCount;
    public int MaxCount;
    public int Income;

    public void Update(AffectionParameters @params)
    {
        CurrentCount = @params.CurrentCount;
        Income = @params.Income;
        MaxCount = @params.MaxCount;

        if (CurrentCount < 0) {
            CurrentCount = 0;
        }
    }
}

public static class ParametersCounter
{
    private static Dictionary<EAffectionType, AffectionParameters> _variables;

    public static Action<EAffectionType, AffectionParameters> OnValueChanged;
    public static bool ActiveTime;

    public static void Init()
    {
        _variables = new Dictionary<EAffectionType, AffectionParameters>();

        foreach (EAffectionType item in Enum.GetValues(typeof(EAffectionType)))
        {
            var income = item == EAffectionType.Oxygen ? -1 : 1;

            _variables.Add(item, new AffectionParameters
            {
                AffectionType = item,
                CurrentCount = 10,
                Income = income
            });
        }
    }

    public static void SetValue(EAffectionType type, int current, int max, int income, bool displayLog = true)
    {
        var newParams = new AffectionParameters
        {
            AffectionType = type,
            CurrentCount = current,
            Income = income,
            MaxCount = max
        };

        _variables[type].Update(newParams);

        if (displayLog)
        {
            Debug.Log("<b>Parameters.</b> Change parameter (" + type + ") to value (" + _variables[type].CurrentCount + ")");
        }

        if (type != EAffectionType.Population)
        {
            foreach (EAffectionType affectionType in Enum.GetValues(typeof(EAffectionType)))
            {
                if (OnValueChanged != null)
                {
                    OnValueChanged.Invoke(affectionType, new AffectionParameters
                    {
                        AffectionType = affectionType,
                        CurrentCount = GetValue(affectionType).CurrentCount,
                        Income = GetValue(affectionType).Income,
                        MaxCount = GetValue(affectionType).MaxCount
                    });
                }
            }
        }

        if (type != EAffectionType.Oxygen)
        {
            _variables[EAffectionType.Population].Update(new AffectionParameters
            {
                AffectionType = EAffectionType.Population,
                CurrentCount = GetPopulationSum(),
                Income = 0,
                MaxCount = 1000
            });

            if (OnValueChanged != null)
            {
                OnValueChanged.Invoke(EAffectionType.Population, _variables[EAffectionType.Population]);
            }
        }
    }

    public static AffectionParameters GetValue(EAffectionType type)
    {
        return _variables[type];
    }
    
    public static void UpdateCurrentValue(EAffectionType type, int value)
    {
        _variables[type].CurrentCount = value;

        if (OnValueChanged != null) {
            OnValueChanged.Invoke(type, _variables[type]);
        }
    }

    public static void UpdateIncome(EAffectionType type, int value)
    {
        Debug.Log("<color=blue><b>Income.</b></color> On type (" + type +") has been increased to (" + value + ")");

        _variables[type].Income = value;

        if (OnValueChanged != null) {
            OnValueChanged.Invoke(type, _variables[type]);
        }
    }

    public static int GetPopulationSum()
    {
        return (int) Enum.GetValues(typeof(EAffectionType))
            .Cast<EAffectionType>()
            .Where(x => x != EAffectionType.Population)
            .Where(x => x != EAffectionType.Oxygen)
            .Aggregate(0f, (current, affectionType) => current + _variables[affectionType].CurrentCount);
    }
}