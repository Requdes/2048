#if UNITY_EDITOR

using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class BootstrapSceneAutoLoader {
    private const string EntranceSceneKey = "ENTRANCE_SCENE";

    static BootstrapSceneAutoLoader () {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged (PlayModeStateChange state) {
        if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode) OnChangedToGameState();
        if (!EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode) OnChangedToEditState();
    }

    private static void OnChangedToGameState () {
        SeveEntranceScene();

        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) EditorApplication.isPlaying = false;
        
        LoadScene(InfrastructureAssetsPath.BootstrapScene);
    }

    private static void SeveEntranceScene () {
        var path = SceneManager.GetActiveScene().path;
        EditorPrefs.SetString(EntranceSceneKey, path);
    }

    private static void OnChangedToEditState () {
        LoadScene(EditorPrefs.GetString(EntranceSceneKey));
    }

    private static void LoadScene (string path) {
        try {
            EditorSceneManager.OpenScene(path);
        }
        catch {
            Debug.LogError($"Unable to load scene: {path}");
            EditorApplication.isPlaying = false;
        }
    }
}

#endif