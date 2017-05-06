using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace App.Input
{
	public class InputHandlerModel : MonoBehaviour 
	{
		public static InputHandlerModel inputHandlerModel;

		public InputObject[] inputs;
		[HideInInspector]public GameObject[] InputObjs;

		void Awake()
		{
			if(inputHandlerModel == null)
				inputHandlerModel = this;
			
			if (this != inputHandlerModel)
				Destroy(this);
		}

		void Start()
		{
			getAllInputObjects();
		}

		void getAllInputObjects()
		{
			InputObjs = inputs.Select(x => x.inputObj).ToArray();

			for(int i = 0; i < InputObjs.Length; i++)
			{
				if(InputObjs[i])
					if(!InputObjs[i].GetComponent<InputObjectController>())
						InputObjs[i].AddComponent<InputObjectController>();
			}
		}
	}

	[System.Serializable]
	public class InputObject
	{
		public enum InputObjectType { state, x_axis, y_axis, z_axis };

		/// <summary>
		/// The type of input object
		/// </summary>
		public InputObjectType type;

		/// <summary>
		/// The name of this input
		/// </summary>
		public string name;

		/// <summary>
		/// Use this to map a touch to this object
		/// </summary>
		public Touch touch;

		/// <summary>
		/// Use this to map a keycode
		/// </summary>
		public KeyCode keyCode;

		/// <summary>
		/// Get value of float 
		/// </summary>
		public float getAxis { get { return m_axis; } }

		/// <summary>
		/// Set value of float
		/// </summary>
		/// <value>The set axis.</value>
		public float setAxis { set { m_axis = value; } }

		public GameObject inputObj = null;

		private float m_axis;

		public InputObject ()
		{
			touch = new Touch();
			keyCode = KeyCode.None;
			name = "";
		}
	}

	public struct TouchInfo
	{
		public TouchPhase phase;
		public Vector2 position;

		public TouchInfo(TouchPhase touchPhase, Vector2 touchPos)
		{
			phase = touchPhase;
			position = touchPos;
		}

		public TouchInfo(Touch touch)
		{
			phase = touch.phase;
			position = touch.position;
		}
	}
}