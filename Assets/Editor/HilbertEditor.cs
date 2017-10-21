using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Algorithms;
using UnityEditor;

[CustomEditor(typeof(HilbertCurve))]
public class HilbertEditor : Editor {
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var mytarget  = (HilbertCurve)target;
        EditorGUILayout.TextField("NumberOfPoints", mytarget.m_Points.Count.ToString());
    }
}
