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
[CustomEditor(typeof(Events))]
public class EventsEditor : BaseGoogleEditor<Events>
{	    
    public override bool Load()
    {        
        Events targetData = target as Events;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<EventsData>(targetData.WorksheetName) ?? db.CreateTable<EventsData>(targetData.WorksheetName);
        
        List<EventsData> myDataList = new List<EventsData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            EventsData data = new EventsData();
            
            data = Cloner.DeepCopy<EventsData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
