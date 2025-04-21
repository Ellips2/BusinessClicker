//Put this script to Assets/Editor/

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class ExtendedHotkeys : ScriptableObject 
{
    [MenuItem("ExtHotKeys/Select Prefab Asset")] //Shift+A
    static void SelectPrefabAsset()
    {
		var path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(Selection.activeObject);
		EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath<Object>(path));
		Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(path);
	}

	[MenuItem("ExtHotKeys/Refresh Saving Objs")] //Ctrl+S
    static void RefreshSavingObjs() => 
		EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

	[MenuItem ("ExtHotKeys/Select MainCamera")] //Shift+2
    static void SelectMainCamera()
	{
        EditorGUIUtility.PingObject(Camera.main);        
        Selection.activeObject = Camera.main;        
    }
}