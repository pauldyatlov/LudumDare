using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/Events")]
    public static void CreateEventsAssetFile()
    {
        Events asset = CustomAssetUtility.CreateAsset<Events>();
        asset.SheetName = "LudumDareSpreadsheet";
        asset.WorksheetName = "Events";
        EditorUtility.SetDirty(asset);        
    }
    
}