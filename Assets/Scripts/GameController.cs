using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Spreadsheet _itemData;
    [SerializeField] private Camera _camera;

    [SerializeField] private AsteroidsController _asteroidsController;

    private void Awake()
    {
        foreach (var item in _itemData.dataArray)
        {
            Debug.Log("item: " + item.KEYS + " value: " + item.VALUES);
        }

        _asteroidsController.Init(_camera);
    }
}