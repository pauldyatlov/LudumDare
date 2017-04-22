using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
///
[System.Serializable]
public class CitiesData
{
  [SerializeField]
  string key;
  public string KEY { get {return key; } set { key = value;} }
  
  [SerializeField]
  string name;
  public string NAME { get {return name; } set { name = value;} }
  
  [SerializeField]
  int startpopulation;
  public int STARTPOPULATION { get {return startpopulation; } set { startpopulation = value;} }
  
  [SerializeField]
  float growthspeed;
  public float GROWTHSPEED { get {return growthspeed; } set { growthspeed = value;} }
  
}