using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace App.Input
{
	public class EditorHelper
	{
		public static readonly float largeLabelWidth = 100f;
		public static readonly GUILayoutOption miniButtonLength = GUILayout.Width(18f);
		public static readonly float minLW = 80f;
		public static readonly float maxLW = 120f;
		public static readonly float checkW = 10f;
		public static readonly float space = 5f;
		public static readonly float smallSpace = 2f;
	}

	[CustomEditor(typeof(App.Input.InputHandlerModel))]
	public class InputHandlerView : Editor
	{
		
		#region Data

		bool inputFoldout;
		bool[] inputObjectFoldouts;

		#endregion

		public override void OnInspectorGUI()
		{
			DrawInputObjects();
		}

		public void DrawInputObjects()
		{
			inputFoldout = EditorGUILayout.Foldout(inputFoldout, "Input");

			if(inputFoldout){
				if(InputHandlerModel.inputs == null)
					InputHandlerModel.inputs = new InputObject[1];
				

				if(inputObjectFoldouts == null)
					inputObjectFoldouts = new bool[1];
				

				if(inputObjectFoldouts.Length != InputHandlerModel.inputs.Length)
					inputObjectFoldouts = new bool[InputHandlerModel.inputs.Length];
				
				for(int i = 0; i < InputHandlerModel.inputs.Length; i++){
					Debug.Log("Name: " + name);

					if(InputHandlerModel.inputs[i] == null)
						InputHandlerModel.inputs[i] = new InputObject();

					EditorGUI.indentLevel = 1;

					inputObjectFoldouts[i] = EditorGUILayout.Foldout(inputObjectFoldouts[i], InputHandlerModel.inputs[i].name);

					if(inputObjectFoldouts[i]){
						InputHandlerModel.inputs[i].name = EditorGUILayout.TextField(InputHandlerModel.inputs[i].name);
						InputHandlerModel.inputs[i].button = (GameObject)EditorGUILayout.ObjectField(InputHandlerModel.inputs[i].button, typeof(GameObject));
					}

					EditorGUI.indentLevel = 0;
				}
			}
		}
	}
}