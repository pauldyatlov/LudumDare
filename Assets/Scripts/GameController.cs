using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Item _itemData;

    private void Awake()
    {
        foreach (var item in _itemData.dataArray)
        {
            Debug.Log("item: " + item.KEYS + " value: " + item.VALUES);
        }
    }
}