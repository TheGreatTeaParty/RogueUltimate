using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Example : EditorWindow
{
    [MenuItem("Window/Example")]
    public static void ShowWindow()
    {
        GetWindow<Example>("Example");
    }
    private void OnGUI()
    {
        
    }
}
