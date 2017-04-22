using UnityEngine;

public class HouseTester : MonoBehaviour
{
    [SerializeField] private GameObject _housePrefab;

    private void OnMouseDown()
    {
        SpawnArea.SpawnHouse(GetComponent<Collider>(), Camera.main.ScreenPointToRay(Input.mousePosition), Instantiate(_housePrefab));
    }
}
