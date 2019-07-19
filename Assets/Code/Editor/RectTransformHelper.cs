using UnityEditor;
using UnityEngine;
namespace Helper
{
	public class UIMenu
	{
		[MenuItem("Helper/UI/Anchor Around Object")]
		static void SetAnchorsAroundChild()
		{
			GameObject childGameObject = Selection.activeGameObject;
			if(childGameObject == null)
			{
				Debug.LogWarning("Cannot apply SetAnchorsAroundChild() as no active GameObject selected");
				return;
			}

			RectTransform childRectTransform = childGameObject.GetComponent<RectTransform>();
			if (childRectTransform == null)
			{
				Debug.LogWarning("Cannot apply SetAnchorsAroundChild() as no active GameObject selected");
				return;
			}

			RectTransform parentRectTransform = childGameObject.transform.parent.GetComponent<RectTransform>();
			if(parentRectTransform == null)
			{
				Debug.LogWarning("Cannot apply SetAnchorsAroundChild() as active GameObject does not have a parent!");
				return;
			}

			// Save an undo point before changes happen
			Undo.RecordObject(childRectTransform, "Set anchors around object");
			
			var originalOffsetMin = childRectTransform.offsetMin;
			var originalOffsetMax = childRectTransform.offsetMax;
			var originalAnchorMin = childRectTransform.anchorMin;
			var originalAnchorMax = childRectTransform.anchorMax;

			var parentRect = parentRectTransform.rect;
			var parentWidth = parentRect.width;
			var parentHeight = parentRect.height;

			var anchorMin = new Vector2(originalAnchorMin.x + (originalOffsetMin.x / parentWidth),
										originalAnchorMin.y + (originalOffsetMin.y / parentHeight));
			var anchorMax = new Vector2(originalAnchorMax.x + (originalOffsetMax.x / parentWidth),
										originalAnchorMax.y + (originalOffsetMax.y / parentHeight));

			childRectTransform.anchorMin = anchorMin;
			childRectTransform.anchorMax = anchorMax;

			childRectTransform.offsetMin = new Vector2(0, 0);
			childRectTransform.offsetMax = new Vector2(0, 0);
		}
	}
}
//using System;
//using System.Reflection;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;
//[CustomEditor(typeof(RectTransform), true)]
//public class RectTransformEditor : Editor
//{
//	private Editor editorInstance;
//	private Type nativeEditor;
//	private MethodInfo onSceneGui;
//	private MethodInfo onValidate;

//	public override void OnInspectorGUI()
//	{
//		editorInstance.OnInspectorGUI();
//		// Code here
//	}

//	private void OnEnable()
//	{
//		if (nativeEditor == null)
//			Initialize();

//		nativeEditor.GetMethod("OnEnable", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)?.Invoke(editorInstance, null);
//		onSceneGui = nativeEditor.GetMethod("OnSceneGUI", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
//		onValidate = nativeEditor.GetMethod("OnValidate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
//	}

//	private void OnSceneGUI()
//	{
//		onSceneGui.Invoke(editorInstance, null);
//	}

//	private void OnDisable()
//	{
//		nativeEditor.GetMethod("OnDisable", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(editorInstance, null);
//	}

//	private void Awake()
//	{
//		Initialize();
//		nativeEditor.GetMethod("Awake", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.Invoke(editorInstance, null);
//	}

//	private void Initialize()
//	{
//		nativeEditor = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.RectTransformEditor");
//		editorInstance = CreateEditor(target, nativeEditor);
//	}

//	private void OnDestroy()
//	{
//		nativeEditor.GetMethod("OnDestroy", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.Invoke(editorInstance, null);
//	}

//	private void OnValidate()
//	{
//		if (nativeEditor == null)
//			Initialize();

//		onValidate?.Invoke(editorInstance, null);
//	}

//	private void Reset()
//	{
//		if (nativeEditor == null)
//			Initialize();

//		nativeEditor.GetMethod("Reset", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.Invoke(editorInstance, null);
//	}
//}