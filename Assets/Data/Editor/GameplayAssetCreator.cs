using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/Gameplay")]
    public static void CreateGameplayAssetFile()
    {
        Gameplay asset = CustomAssetUtility.CreateAsset<Gameplay>();
        asset.SheetName = "LudumDareSpreadsheet";
        asset.WorksheetName = "Gameplay";
        EditorUtility.SetDirty(asset);        
    }
    
}