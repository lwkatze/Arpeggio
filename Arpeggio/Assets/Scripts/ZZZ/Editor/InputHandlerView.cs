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

	//[CustomEditor(typeof(App.Input.InputHandlerModel))]
	public class InputHandlerView : Editor
	{
		
		#region Data

		private InputHandlerModel model;
		bool inputFoldout;
		bool[] inputObjectFoldouts;
		private App.Input.InputObject[] inputs { get { return model.inputs; } set { model.inputs = value; } }

		#endregion

		void OnEnable()
		{
			model = (InputHandlerModel)target;
		}

		public override void OnInspectorGUI()
		{
			DrawInputObjects();
			Undo.RecordObject(model, "InputHandler");
		}

		public void DrawInputObjects()
		{
			inputFoldout = EditorGUILayout.Foldout(inputFoldout, "Input");

			if(inputFoldout){
				if(inputs == null)
					inputs = new InputObject[1];
				

				if(inputObjectFoldouts == null)
					inputObjectFoldouts = new bool[1];
				

				if(inputObjectFoldouts.Length != inputs.Length)
					inputObjectFoldouts = new bool[inputs.Length];
				
				for(int i = 0; i < inputs.Length; i++){

					if(inputs[i] == null)
						inputs[i] = new InputObject();

					EditorGUI.indentLevel = 1;

					inputObjectFoldouts[i] = EditorGUILayout.Foldout(inputObjectFoldouts[i], inputs[i].name);

					if(inputObjectFoldouts[i]){
						inputs[i].name = EditorGUILayout.TextField("Name: ",inputs[i].name);
						inputs[i].inputObj = (GameObject)EditorGUILayout.ObjectField("Input Object", inputs[i].inputObj, typeof(GameObject));
					}

					EditorGUI.indentLevel = 0;
				}
			}
		}
	}
}