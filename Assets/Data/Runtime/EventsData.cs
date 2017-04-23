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
  string textru;
  public string Textru { get {return textru; } set { textru = value;} }
  
  [SerializeField]
  string texten;
  public string Texten { get {return texten; } set { texten = value;} }
  
  [SerializeField]
  int[] ignore = new int[0];
  public int[] Ignore { get {return ignore; } set { ignore = value;} }
  
  [SerializeField]
  int[] agree = new int[0];
  public int[] Agree { get {return agree; } set { agree = value;} }
  
  [SerializeField]
  int[] contra = new int[0];
  public int[] Contra { get {return contra; } set { contra = value;} }
  
}