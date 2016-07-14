using UnityEngine;
using System.Collections;
using UnityEditor;


[InitializeOnLoad]
public class HierarchyGUI
{
	static Texture2D UITexture;
	static GUIStyle style;

	//静态构造方法
	static HierarchyGUI ()
	{
		//委托方法
		EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;

		UITexture = (Texture2D)Resources.Load("UI Texture");

		style = new GUIStyle(); 
		style.fontSize=12;

	}

	static void OnHierarchyGUI (int instanceID, Rect selectionRect)
	{
		//获取GO引用
		GameObject go = (GameObject)EditorUtility.InstanceIDToObject(instanceID);

		//获取Rect
		Rect rect = new Rect (selectionRect);
		rect.x = rect.width;


		//toggle开关
		if(EditorPrefs.GetInt(go.GetInstanceID()+"toggle")==1)
		{
			go.SetActive(GUI.Toggle(rect, go.activeInHierarchy, ""));
		}
	
		//添加标签
		if(EditorPrefs.GetInt(go.GetInstanceID()+"G")==1)
		{
			rect.x = rect.width-15;
			rect.width=15;
			rect.height=rect.height-2;

			style.normal.background = null;
			style.normal.textColor = Color.blue;
			GUI.Label(rect, "G", style);
		}

		//添加icon
		if(EditorPrefs.GetInt(go.GetInstanceID()+"UI")==1)
		{
			rect.x = rect.width-15;
			rect.width=15;
			rect.height=rect.height-2;
			style.normal.background = UITexture;
			GUI.Label(rect,"", style);
		}

	}


	[MenuItem("GameObject/Marker/Toggle", false, 0)]
	static void AddToggle()
	{
		foreach(Object o in Selection.gameObjects)
		{
			if(EditorPrefs.GetInt(o.GetInstanceID()+"toggle")==0)
			{
				EditorPrefs.SetInt(o.GetInstanceID()+"toggle", 1);
			}

			else
			{
				EditorPrefs.SetInt(o.GetInstanceID()+"toggle", 0);
			}
		}
	}

	[MenuItem("GameObject/Marker/UI", false, 1)]
	static void AddUIMarker()
	{
		foreach(Object o in Selection.gameObjects)
		{
			if(EditorPrefs.GetInt(o.GetInstanceID()+"UI")==0)
			{
				EditorPrefs.SetInt(o.GetInstanceID()+"UI", 1);
			}

			else
			{
				EditorPrefs.SetInt(o.GetInstanceID()+"UI", 0);
			}
		}
	}

	[MenuItem("GameObject/Marker/GameManager", false, 2)]
	static void AddGameManagerMarker()
	{
		foreach(Object o in Selection.gameObjects)
		{
			if(EditorPrefs.GetInt(o.GetInstanceID()+"G")==0)
			{
				EditorPrefs.SetInt(o.GetInstanceID()+"G", 1);
			}

			else
			{
				EditorPrefs.SetInt(o.GetInstanceID()+"G", 0);
			}
		}
	}

	//清除所有的标记
	[MenuItem("GameObject/Marker/Remove", false,13)]
	static void RemoveMarkers()
	{
		foreach(Object o in Selection.gameObjects)
		{
			EditorPrefs.SetInt(o.GetInstanceID()+"toggle", 0);
			EditorPrefs.SetInt(o.GetInstanceID()+"G", 0);
			EditorPrefs.SetInt(o.GetInstanceID()+"UI", 0);
		}
	}



}
