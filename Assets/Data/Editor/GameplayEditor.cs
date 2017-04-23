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
[CustomEditor(typeof(Gameplay))]
public class GameplayEditor : BaseGoogleEditor<Gameplay>
{	    
    public override bool Load()
    {        
        Gameplay targetData = target as Gameplay;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<GameplayData>(targetData.WorksheetName) ?? db.CreateTable<GameplayData>(targetData.WorksheetName);
        
        List<GameplayData> myDataList = new List<GameplayData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            GameplayData data = new GameplayData();
            
            data = Cloner.DeepCopy<GameplayData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
