using UnityEngine;

public enum AsteroidType
{
    Damage,
    Heal
}

public class AsteroidParameter : ScriptableObject
{
    public AsteroidObject Template;
    public float Amount;
    public float Speed;
}