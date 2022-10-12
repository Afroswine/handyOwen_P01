using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BossMovement))]
public class BossMovementEditor : Editor
{
    public void OnSceneGUI()
    {
        var t = target as BossMovement;
        var tr = t.transform;
        var pos = t.Destination;
        var color = Color.magenta;

        Handles.color = color;
        Handles.DrawWireDisc(pos, Vector3.up, 3f);
        Handles.DrawWireDisc(pos, tr.forward, 3f);
    }
}
