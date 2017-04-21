using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private const string WebServiceUrl = "https://script.google.com/macros/s/AKfycbwnj4fzz6sd8-sWyi0sndVsagazypHLr-TPSl31e9Ls-1_qoQ/exec";

    private static readonly Dictionary<string, string> GoogleSpreadsheets = new Dictionary<string, string>();

    private const string Query = WebServiceUrl + "?action=GetSpreadsheetList";

    private WWW _spreadsheetConnection;
    private Action<string, string> _connectionCallback;

    private void Awake()
    {
        NewSpreadsheet();
    }

    private void FindSpreadsheet()
    {
        _spreadsheetConnection = new WWW(Query);
        _connectionCallback = OnFindSpreadsheets;

        EditorApplication.update += CheckForConnection;
    }

    private void CheckForConnection()
    {
        if (_spreadsheetConnection == null || !_spreadsheetConnection.isDone) return;

        var callback = _connectionCallback;

        var result = string.Empty;
        var error = _spreadsheetConnection.error;

        if (string.IsNullOrEmpty(error))
            result = _spreadsheetConnection.text;

        StopConnectionWww();
        if (callback != null)
            callback(result, error);
    }

    private static void OnFindSpreadsheets(string result, string error)
    {
        if (!string.IsNullOrEmpty(error))
        {
            Debug.LogError("Unable to access google");
            return;
        }
        try
        {
            GoogleSpreadsheets.Clear();
            var data = I2.Loc.SimpleJSON.JSON.Parse(result).AsObject;

            foreach (KeyValuePair<string, I2.Loc.SimpleJSON.JSONNode> element in data)
            {
                GoogleSpreadsheets[element.Key] = element.Value;

                Debug.Log("Element: " + element);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private void NewSpreadsheet()
    {
        const string spreadsheetName = "LudumDare38";

        var query = WebServiceUrl + "?action=NewSpreadsheet&name=" + Uri.EscapeDataString(spreadsheetName);

        _spreadsheetConnection = new WWW(query);
        _connectionCallback = OnNewSpreadsheet;

        EditorApplication.update += CheckForConnection;
    }

    private void OnNewSpreadsheet(string Result, string Error)
    {
        if (!string.IsNullOrEmpty(Error))
        {
            Debug.LogError("Unable to access google");
            return;
        }
        try
        {
            var data = I2.Loc.SimpleJSON.JSON.Parse(Result).AsObject;

            string sheetname = data["name"];
            string key = data["id"];

            //mSerializedObj_Source.Update();
            //mProp_Google_SpreadsheetKey.stringValue = key;
            //mProp_Google_SpreadsheetName.stringValue = name;
            //mSerializedObj_Source.ApplyModifiedProperties();
            GoogleSpreadsheets[sheetname] = key;

        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private void StopConnectionWww()
    {
        EditorApplication.update -= CheckForConnection;

        _spreadsheetConnection = null;
        _connectionCallback = null;
    }
}