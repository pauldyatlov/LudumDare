using System;
using System.Collections.Generic;
using UnityEngine;

public enum EStupidAffectionType
{
    Good,
    Neutral,
    Bad
}

public class Scenario : ScriptableObject
{
    [Serializable]
    public class Decision
    {
        [Serializable]
        public class Affection
        {
            public EAffectionType AffectionType;
            public int Amout;
        }

        public string DecisionDescription;
        public string AffectionDescription;

        public List<Affection> Affections;
        public EStupidAffectionType PandaApprovalStatus;
    }

    public string EventDescription;
    public List<Decision> Decisions;

    public Decision.Affection Condition;

    [NonSerialized] public bool Passed;
}