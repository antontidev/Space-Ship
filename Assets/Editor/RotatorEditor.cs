using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(PlayerRotator))]
public class RotatorEditor : Editor
{
    private float degree = 15f;

    private PlayerRotator playerRotator;

    private void OnEnable()
    {
        playerRotator = (PlayerRotator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Start custom editor
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Rotate by degree plus"))
        {
            playerRotator.Rotate(degree);
        }

        if (GUILayout.Button("Rotate by degree minus"))
        {
            playerRotator.Rotate(-degree);
        }

        GUILayout.EndHorizontal();
        // End custom editor
    }
}
