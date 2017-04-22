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
[CustomEditor(typeof(Cities))]
public class CitiesEditor : BaseGoogleEditor<Cities>
{	    
    public override bool Load()
    {        
        Cities targetData = target as Cities;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<CitiesData>(targetData.WorksheetName) ?? db.CreateTable<CitiesData>(targetData.WorksheetName);
        
        List<CitiesData> myDataList = new List<CitiesData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            CitiesData data = new CitiesData();
            
            data = Cloner.DeepCopy<CitiesData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
