using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/Cities")]
    public static void CreateCitiesAssetFile()
    {
        Cities asset = CustomAssetUtility.CreateAsset<Cities>();
        asset.SheetName = "LudumDareSpreadsheet";
        asset.WorksheetName = "Cities";
        EditorUtility.SetDirty(asset);        
    }
    
}