using UnityEngine;
using UnityEditor;

public class FindMissingScripts : Editor
{
    [MenuItem("Tools/Find Missing Scripts")]
    public static void FindMissing()
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject go in allObjects)
        {
            Component[] components = go.GetComponents<Component>();
            foreach (Component component in components)
            {
                if (component == null)
                {
                    Debug.LogError("GameObject " + go.name + " has a missing script!", go);
                }
            }
        }
    }
}
