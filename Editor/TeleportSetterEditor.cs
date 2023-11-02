using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace net.narazaka.vrchat.teleport_setter.editor
{
    [CustomEditor(typeof(TeleportSetter))]
    public class TeleportSetterEditor : Editor
    {
        SerializedProperty Teleporters;
        SerializedProperty TeleportTargets;
        SerializedProperty TeleportTargetIndexes;
        ReorderableList TeleportersList;
        ReorderableList TeleportTargetsList;

        void OnEnable()
        {
            Teleporters = serializedObject.FindProperty("Teleporters");
            TeleportTargets = serializedObject.FindProperty("TeleportTargets");
            TeleportTargetIndexes = serializedObject.FindProperty("TeleportTargetIndexes");

            TeleportersList = new ReorderableList(serializedObject, Teleporters);
            TeleportersList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "Teleporters");
            TeleportersList.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = Teleporters.GetArrayElementAtIndex(index);
                var indexElement = TeleportTargetIndexes.GetArrayElementAtIndex(index);
                var allWidth = rect.width;
                rect.width = allWidth * 0.5f - 33;
                EditorGUI.PropertyField(rect, element, GUIContent.none);
                rect.x += rect.width + 3;
                rect.width = 60;
                EditorGUIUtility.labelWidth = 20;
                EditorGUI.PropertyField(rect, indexElement, new GUIContent("=>"));
                EditorGUIUtility.labelWidth = 0;
                rect.x += rect.width + 3;
                rect.width = allWidth * 0.5f - 33;
                if (indexElement.intValue >= TeleportTargets.arraySize)
                {
                    EditorGUI.LabelField(rect, "Out of range");
                }
                else if (indexElement.intValue < 0)
                {
                    EditorGUI.LabelField(rect, "Not set");
                }
                else
                {
                    using (var changeCheck = new EditorGUI.ChangeCheckScope())
                    {
                        var newObject = EditorGUI.ObjectField(rect, GUIContent.none, TeleportTargets.GetArrayElementAtIndex(indexElement.intValue).objectReferenceValue, typeof(Transform), true) as Transform;
                        if (changeCheck.changed)
                        {
                            for (var i = 0; i < TeleportTargets.arraySize; i++)
                            {
                                var target = TeleportTargets.GetArrayElementAtIndex(i);
                                if (target.objectReferenceValue == newObject)
                                {
                                    indexElement.intValue = i;
                                    break;
                                }
                            }
                        }
                    }
                }
            };
            TeleportersList.onAddCallback = (list) =>
            {
                Teleporters.InsertArrayElementAtIndex(Teleporters.arraySize);
                TeleportTargetIndexes.arraySize = Teleporters.arraySize;
                TeleportTargetIndexes.GetArrayElementAtIndex(TeleportTargetIndexes.arraySize - 1).intValue = -1;
            };
            TeleportersList.onRemoveCallback = (list) =>
            {
                Teleporters.DeleteArrayElementAtIndex(list.index);
                TeleportTargetIndexes.DeleteArrayElementAtIndex(list.index);
            };
            TeleportersList.onReorderCallbackWithDetails = (list, oldIndex, newIndex) =>
            {
                TeleportTargetIndexes.MoveArrayElement(oldIndex, newIndex);
            };
            TeleportTargetsList = new ReorderableList(serializedObject, TeleportTargets);
            TeleportTargetsList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "TeleportTargets");
            TeleportTargetsList.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = TeleportTargets.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(rect, element, GUIContent.none);
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            TeleportersList.DoLayoutList();
            TeleportTargetsList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
