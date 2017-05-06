using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Input
{
	public class InputObject
	{
		Touch touch;
		KeyCode keyCode;

		float axis;

		public InputObject (KeyCode code = KeyCode.None)
		{
			touch = new Touch();
			keyCode = KeyCode.None;
		}
	}

	public class InputHandlerModel
	{

	}
}