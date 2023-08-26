using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(Trap))]
public class TrapEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        var script = (Trap)target;
        GUILayout.Space(20);
        if(GUILayout.Button("Generate New Id")) {
            script.AssignNewID();
        }
    }
}
