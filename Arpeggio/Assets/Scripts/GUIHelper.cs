using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Reflection;
using System;

public static class InspectorHelper
{
	public static readonly GUILayoutOption miniButtonLength = GUILayout.Width(18f);
	public static readonly float minLW = 80f;
	public static readonly float maxLW = 120f;
	public static readonly float checkW = 10f;
	public static readonly float space = 5f;
	public static readonly float smallSpace = 2f;

	public static void drawAddRemoveButtons<T>(ref List<T> list, T toAdd)
	{
		using ( new HorizontalBlock())
		{
			EditorGUILayout.LabelField("");

			if(GUILayout.Button("+", EditorStyles.miniButtonLeft, miniButtonLength))
				list.Add(toAdd);

			else if(GUILayout.Button("-", EditorStyles.miniButtonRight, miniButtonLength))
				list.RemoveAt(list.Count - 1);
		}
	}

	public static void drawAddRemoveButtons(Action addAction, Action removeAction)
	{
		using ( new HorizontalBlock())
		{
			EditorGUILayout.LabelField("");

			if(GUILayout.Button("+", EditorStyles.miniButtonLeft, miniButtonLength))
				addAction();

			else if(GUILayout.Button("-", EditorStyles.miniButtonRight, miniButtonLength))
				removeAction();
		}
	}

	public static void drawAddRemoveButtons<T>(Action<T> addAction, Action<T> removeAction, T t)
	{
		using ( new HorizontalBlock())
		{
			EditorGUILayout.LabelField("");

			if(GUILayout.Button("+", EditorStyles.miniButtonLeft, miniButtonLength))
				addAction(t);

			else if(GUILayout.Button("-", EditorStyles.miniButtonRight, miniButtonLength))
				removeAction(t);
		}
	}

	public static void drawMoveDownArrow<T, TResult>(Func<T, TResult> moveUpAction, ref T t)
	{
		if(GUILayout.Button("\u25B4", GUI.skin.label, GUILayout.Width(5f)))
			moveUpAction(t);
	}

	/// <summary>
	/// Draws a "Move up arrow" and moves the specified element up in the list.
	/// </summary>
	/// <param name="list">Reference of the list.</param>
	/// <param name="id">The integer id of the element you wish to move up in the list.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static void drawDownArrowAndMove<T>(ref List<T> list, int id, GUIStyle style = null, float width = 10f, int fontSize = 18)
	{
		if(id <= 0){
			GUILayout.Label(" ", GUILayout.Width(width));
			return;
		}

		if(style == null){
			style = GUI.skin.label;
			style.fontSize = fontSize;
		}

		T item = list[id];
		if(GUILayout.Button("\u25B3", style, GUILayout.Width(width))){
			list.RemoveAt(id);
			list.Insert(id - 1, item); 
		}
	}

	/// <summary>
	/// Draws a "Move down arrow" and moves the specified element down in the list.
	/// </summary>
	/// <param name="list">Reference of the list.</param>
	/// <param name="id">The integer id of the element you wish to move up in the list.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static void drawUpArrowAndMove<T>(ref List<T> list, int id, GUIStyle style = null, float width = 10f, int fontSize = 18)
	{
		if(id >= list.Count - 1){
			GUILayout.Label("\u0020", GUILayout.Width(width));
			return;
		}

		if(style == null){
			style = GUI.skin.label;
			style.fontSize = fontSize;
		}

		T item = list[id];
		if(GUILayout.Button("\u25BD", style, GUILayout.Width(width))){
			list.RemoveAt(id);
			list.Insert(id + 1, item); 
		}
	}
}
public class VerticalBlock : IDisposable
{
	public VerticalBlock(params GUILayoutOption[] options)
	{
		GUILayout.BeginVertical(options);
	}

	public VerticalBlock(GUIStyle style, params GUILayoutOption[] options)
	{
		GUILayout.BeginVertical(style, options);
	}

	public void Dispose()
	{
		GUILayout.EndVertical();
	}
}

public class ScrollviewBlock : IDisposable
{
	public ScrollviewBlock(ref Vector2 scrollPos, params GUILayoutOption[] options)
	{
		scrollPos = GUILayout.BeginScrollView(scrollPos, options);
	}

	public void Dispose()
	{
		GUILayout.EndScrollView();
	}
}

public class HorizontalBlock : IDisposable
{
	public HorizontalBlock(params GUILayoutOption[] options)
	{
		GUILayout.BeginHorizontal(options);
	}

	public HorizontalBlock(GUIStyle style, params GUILayoutOption[] options)
	{
		GUILayout.BeginHorizontal(style, options);
	}

	public void Dispose()
	{
		GUILayout.EndHorizontal();
	}
}

public class AreaBlock : IDisposable
{
	public AreaBlock(Rect rect)
	{
		GUILayout.BeginArea(rect);
	}

	public AreaBlock(Rect rect, GUIStyle style)
	{
		GUILayout.BeginArea(rect, style);
	}

	public void Dispose()
	{
		GUILayout.EndArea();
	}
}

public class ColoredBlock : System.IDisposable
{
	public ColoredBlock(Color color)
	{
		GUI.color = color;
	}

	public void Dispose()
	{
		GUI.color = Color.white;
	}
}

public class TabsBlock
{
	private  Dictionary<string, Action> methods;
	private Action currentGuiMethod;
	public int curMethodIndex = -1;

	public TabsBlock(Dictionary<string, Action> _methods)
	{
		methods = _methods;
		SetCurrentMethod(0);
	}

	public void Draw()
	{
		var keys = methods.Keys.ToArray();
		using (new VerticalBlock(EditorStyles.helpBox))
		{
			using (new HorizontalBlock())
			{
				for (int i = 0; i < keys.Length; i++)
				{
					var btnStyle = i == 0 ? EditorStyles.miniButtonLeft : i == (keys.Length - 1) ? EditorStyles.miniButtonRight : EditorStyles.miniButtonMid;
					using (new ColoredBlock(currentGuiMethod == methods[keys[i]] ? Color.grey : Color.white))
						if (GUILayout.Button(keys[i], btnStyle))
							SetCurrentMethod(i);
				}
			}
			GUILayout.Label(keys[curMethodIndex], EditorStyles.centeredGreyMiniLabel);
			currentGuiMethod();
		}
	}

	public void SetCurrentMethod(int index)
	{
		curMethodIndex = index;
		currentGuiMethod = methods[methods.Keys.ToArray()[index]];
	}
}
