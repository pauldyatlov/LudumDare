using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
///
[System.Serializable]
public class EventsData
{
  [SerializeField]
  string key;
  public string KEY { get {return key; } set { key = value;} }
  
  [SerializeField]
  EAffectionType eaffectiontype;
  public EAffectionType EAFFECTIONTYPE { get {return eaffectiontype; } set { eaffectiontype = value;} }
  
  [SerializeField]
  int breakpoint;
  public int Breakpoint { get {return breakpoint; } set { breakpoint = value;} }
  
  [SerializeField]
  string description;
  public string Description { get {return description; } set { description = value;} }
  
  [SerializeField]
  int[] ignore = new int[0];
  public int[] Ignore { get {return ignore; } set { ignore = value;} }
  
  [SerializeField]
  int[] ignoreinstant = new int[0];
  public int[] Ignoreinstant { get {return ignoreinstant; } set { ignoreinstant = value;} }
  
  [SerializeField]
  string ignorebuttonlabel;
  public string Ignorebuttonlabel { get {return ignorebuttonlabel; } set { ignorebuttonlabel = value;} }
  
  [SerializeField]
  int[] agree = new int[0];
  public int[] Agree { get {return agree; } set { agree = value;} }
  
  [SerializeField]
  int[] agreeinstant = new int[0];
  public int[] Agreeinstant { get {return agreeinstant; } set { agreeinstant = value;} }
  
  [SerializeField]
  string agreebuttonlabel;
  public string Agreebuttonlabel { get {return agreebuttonlabel; } set { agreebuttonlabel = value;} }
  
  [SerializeField]
  int[] contra = new int[0];
  public int[] Contra { get {return contra; } set { contra = value;} }
  
  [SerializeField]
  int[] contrainstant = new int[0];
  public int[] Contrainstant { get {return contrainstant; } set { contrainstant = value;} }
  
  [SerializeField]
  string contrabuttonlabel;
  public string Contrabuttonlabel { get {return contrabuttonlabel; } set { contrabuttonlabel = value;} }

}