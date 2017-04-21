using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/Spreadsheet")]
    public static void CreateSpreadsheetAssetFile()
    {
        Spreadsheet asset = CustomAssetUtility.CreateAsset<Spreadsheet>();
        asset.SheetName = "LudumDareSpreadsheet";
        asset.WorksheetName = "Spreadsheet";
        EditorUtility.SetDirty(asset);        
    }
    
}