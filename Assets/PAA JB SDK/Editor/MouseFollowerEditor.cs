using UnityEditor;
using UnityEngine;
using PAA.JB.Core.Motion;

[CustomEditor(typeof(MouseFollower))]
[CanEditMultipleObjects]
public class MouseFollowerEditor : Editor
{
    SerializedProperty useAnchoredPosition;
    SerializedProperty smoothFollow;
    SerializedProperty moveSpeed;
    SerializedProperty mouseOffset;

    SerializedProperty limitToBounds;
    SerializedProperty followBounds;
    SerializedProperty autoStart;

    SerializedProperty holdOnClick;

    SerializedProperty changeSpriteOnClick;
    SerializedProperty clickSprite;

    SerializedProperty pulseEffectOnClick;
    SerializedProperty pulseMode;
    SerializedProperty pulseEase;
    SerializedProperty pulseScale;
    SerializedProperty pulseDuration;

    SerializedProperty rotationEffectOnClick;
    SerializedProperty rotationMode;
    SerializedProperty rotationEase;
    SerializedProperty rotationAngle;
    SerializedProperty rotationDuration;
    SerializedProperty rotationSnap;

    void OnEnable()
    {
        useAnchoredPosition = serializedObject.FindProperty("useAnchoredPosition");
        smoothFollow = serializedObject.FindProperty("smoothFollow");
        moveSpeed = serializedObject.FindProperty("moveSpeed");
        mouseOffset = serializedObject.FindProperty("mouseOffset");

        limitToBounds = serializedObject.FindProperty("limitToBounds");
        followBounds = serializedObject.FindProperty("followBounds");
        autoStart = serializedObject.FindProperty("autoStart");

        holdOnClick = serializedObject.FindProperty("holdOnClick");

        changeSpriteOnClick = serializedObject.FindProperty("changeSpriteOnClick");
        clickSprite = serializedObject.FindProperty("clickSprite");

        pulseEffectOnClick = serializedObject.FindProperty("pulseEffectOnClick");
        pulseMode = serializedObject.FindProperty("pulseMode");
        pulseEase = serializedObject.FindProperty("pulseEase");
        pulseScale = serializedObject.FindProperty("pulseScale");
        pulseDuration = serializedObject.FindProperty("pulseDuration");

        rotationEffectOnClick = serializedObject.FindProperty("rotationEffectOnClick");
        rotationMode = serializedObject.FindProperty("rotationMode");
        rotationEase = serializedObject.FindProperty("rotationEase");
        rotationAngle = serializedObject.FindProperty("rotationAngle");
        rotationDuration = serializedObject.FindProperty("rotationDuration");
        rotationSnap = serializedObject.FindProperty("rotationSnap");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(useAnchoredPosition);
        EditorGUILayout.PropertyField(smoothFollow);
        EditorGUILayout.PropertyField(moveSpeed);
        EditorGUILayout.PropertyField(mouseOffset);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(limitToBounds);
        if (limitToBounds.boolValue)
        {
            EditorGUILayout.PropertyField(followBounds);
        }
        EditorGUILayout.PropertyField(autoStart);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(holdOnClick);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(changeSpriteOnClick, new GUIContent("Enable Sprite Change"));
        if (changeSpriteOnClick.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(clickSprite, new GUIContent("Click Sprite"));
            
            // Hiển thị cảnh báo nếu không có Image component
            MouseFollower mouseFollower = (MouseFollower)target;
            if (mouseFollower.GetComponent<UnityEngine.UI.Image>() == null)
            {
                EditorGUILayout.HelpBox("Cần có Image component để sử dụng chức năng này!", MessageType.Warning);
            }
            else if (clickSprite.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("Vui lòng gán sprite cho Click Sprite!", MessageType.Info);
            }
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(pulseEffectOnClick, new GUIContent("Enable Pulse Effect"));
        if (pulseEffectOnClick.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(pulseMode);
            EditorGUILayout.PropertyField(pulseEase);
            EditorGUILayout.PropertyField(pulseScale);
            EditorGUILayout.PropertyField(pulseDuration);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.PropertyField(rotationEffectOnClick, new GUIContent("Enable Rotation Effect"));
        if (rotationEffectOnClick.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(rotationMode);
            EditorGUILayout.PropertyField(rotationEase);
            EditorGUILayout.PropertyField(rotationAngle);
            EditorGUILayout.PropertyField(rotationDuration);
            EditorGUILayout.PropertyField(rotationSnap);
            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
