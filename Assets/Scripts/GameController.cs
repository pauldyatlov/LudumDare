using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Spreadsheet _spreadsheet;

    private void Awake()
    {
        foreach (var item in _spreadsheet.dataArray)
        {
            Debug.Log("item: " + item.KEYS + " value: " + item.VALUES);
        }
    }
}