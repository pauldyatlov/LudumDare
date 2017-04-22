using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/Asteroids")]
    public static void CreateAsteroidsAssetFile()
    {
        Asteroids asset = CustomAssetUtility.CreateAsset<Asteroids>();
        asset.SheetName = "LudumDareSpreadsheet";
        asset.WorksheetName = "Asteroids";
        EditorUtility.SetDirty(asset);        
    }
    
}