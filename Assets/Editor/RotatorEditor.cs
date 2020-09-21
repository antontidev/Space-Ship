using System;
using UniRx;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerRotator))]
public class RotatorEditor : Editor
{
    private float degree = 15f;

    private Vector3 axisVector;

    private PlayerRotator playerRotator;

    private void OnEnable()
    {
        axisVector = new Vector3(0.0f, degree, 0.0f);

        playerRotator = (PlayerRotator)target;

        playerRotator.rotationType.Subscribe(x =>
        {
            playerRotator.ChangeRotator(x);
        });
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Rotate by degree plus"))
        {
            //playerRotator.Rotate(degree);
            playerRotator._RotateEditor(axisVector);
        }

        if (GUILayout.Button("Rotate by degree minus"))
        {
            var rotationVector = axisVector * -1;

            playerRotator._RotateEditor(rotationVector);
        }

        GUILayout.EndHorizontal();
    }
}
