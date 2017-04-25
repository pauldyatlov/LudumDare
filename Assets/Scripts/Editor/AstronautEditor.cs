using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Astronaut))]
public class AstronautEditor : Editor
{
    private Astronaut _astronaut;

    private void OnEnable()
    {
        _astronaut = (Astronaut)target;
    }

    private void OnSceneGUI()
    {
        DoSceneGUI(_astronaut);
    }

    public static void DoSceneGUI(Astronaut astronaut)
    {
        foreach (var spawnArea in astronaut.SpawnAreas) {
            var color = Handles.color;
            Handles.color = Selection.activeGameObject == spawnArea.gameObject ? Color.red : Color.yellow;
            Handles.DrawWireDisc(spawnArea.transform.position, spawnArea.transform.forward, spawnArea.Radius);
            Handles.ArrowHandleCap(0, spawnArea.transform.position, spawnArea.transform.rotation, spawnArea.Radius, Event.current.type);
            Handles.color = color;
        }

        var collider = astronaut.PandaCollider;
        if (collider == null)
            return;

        astronaut.RebakeMesh();

        RaycastHit hitInfo;

        if (collider.Raycast(HandleUtility.GUIPointToWorldRay(Event.current.mousePosition), out hitInfo, 100f)) {
            Handles.DrawWireCube(hitInfo.point, Vector3.one * 0.1f);
            Handles.ArrowHandleCap(0, hitInfo.point, Quaternion.LookRotation(hitInfo.normal), 1, Event.current.type);

            if (Event.current.type == EventType.MouseDown && Event.current.button == 0) {
                var spawnAreaGo = new GameObject("Spawn Area");
                var spawnArea = spawnAreaGo.AddComponent<SpawnArea>();
                spawnArea.transform.SetParent(astronaut.transform);
                spawnArea.transform.position = hitInfo.point;
                spawnArea.transform.rotation = Quaternion.LookRotation(hitInfo.normal);
                spawnArea.Init();
                astronaut.SpawnAreas.Add(spawnArea);
                Selection.activeObject = spawnArea.gameObject;
                Event.current.Use();
            }
        }

        SceneView.RepaintAll();
    }
}
