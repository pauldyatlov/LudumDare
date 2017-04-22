using UnityEditor;

[CustomEditor(typeof(SpawnArea))]
public class SpawnAreaEditor : Editor
{
    private SpawnArea _spawnArea;

    private void OnEnable()
    {
        _spawnArea = (SpawnArea)target;
    }

    private void OnSceneGUI()
    {
        var astronaut = _spawnArea.Astronaut;

        if (astronaut == null)
            return;

        AstronautEditor.DoSceneGUI(astronaut);
    }
}
