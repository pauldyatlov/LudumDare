using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using GDataDB;
using GDataDB.Linq;

using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
[CustomEditor(typeof(Spreadsheet))]
public class SpreadsheetEditor : BaseGoogleEditor<Spreadsheet>
{	    
    public override bool Load()
    {        
        Spreadsheet targetData = target as Spreadsheet;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<SpreadsheetData>(targetData.WorksheetName) ?? db.CreateTable<SpreadsheetData>(targetData.WorksheetName);
        
        List<SpreadsheetData> myDataList = new List<SpreadsheetData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            SpreadsheetData data = new SpreadsheetData();
            
            data = Cloner.DeepCopy<SpreadsheetData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
