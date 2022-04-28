
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [ChildGameObjectsOnly]
    public Image img;


    [Button]
    void TestMenu()
    {
        EditorUtility.DisplayCustomMenu(new Rect(0, 0, 400, 400), new GUIContent[] { new GUIContent("test1"), new GUIContent("test2") }, 0, (data,
                                                            options,
                                                            selected) =>
                                                        {
                                                            Debug.Log(data + "\t" + options + "\t" + selected);
                                                        }, null);

    }

}