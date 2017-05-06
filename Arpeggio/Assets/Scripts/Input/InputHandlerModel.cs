using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Input
{
	public class InputHandlerModel : MonoBehaviour 
	{
		public static InputHandlerModel inputHandlerModel;

		public static InputObject[] inputs;

		void Awake()
		{
			if(inputHandlerModel == null)
				inputHandlerModel = this;
			
			if (this != inputHandlerModel)
				Destroy(this);
		}
	}

	public class InputObject
	{
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
		/// Use this to apply a float 
		/// </summary>
		public float axis { get { return axis; } }

		public GameObject button = null;

		private float m_axis;

		public InputObject ()
		{
			touch = new Touch();
			keyCode = KeyCode.None;
			name = "";
		}
	}


}