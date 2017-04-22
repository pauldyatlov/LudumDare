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
[CustomEditor(typeof(Asteroids))]
public class AsteroidsEditor : BaseGoogleEditor<Asteroids>
{	    
    public override bool Load()
    {        
        Asteroids targetData = target as Asteroids;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<AsteroidsData>(targetData.WorksheetName) ?? db.CreateTable<AsteroidsData>(targetData.WorksheetName);
        
        List<AsteroidsData> myDataList = new List<AsteroidsData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            AsteroidsData data = new AsteroidsData();
            
            data = Cloner.DeepCopy<AsteroidsData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
