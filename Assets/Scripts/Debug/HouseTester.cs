using UnityEngine;

public class HouseTester : MonoBehaviour
{
    [SerializeField] private House _housePrefab;

    private void OnMouseDown()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        foreach (var point in SpawnArea.DrawSpiral(Coils, Radius, Chord)) {
            DoSome(point);
        }
    }

    public int Coils = 30;
    public float Radius = 10;
    public float Chord = 0.5f;

    public void DoSome(Vector2 point)
    {
        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.SetParent(transform, worldPositionStays: false);
        sphere.transform.position = point;
        sphere.transform.localScale = Vector3.one * 0.001f;
    }
}
