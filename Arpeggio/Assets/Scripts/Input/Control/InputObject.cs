using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Input
{
	public delegate void TouchEvent(TouchInfo info);

	public class InputObject : MonoBehaviour 
	{
		public event TouchEvent touchStart;
		public event TouchEvent touchEnded;

		void Awake()
		{

		}

		void Update()
		{

		}

		void OnTouchReceived(App.Input.TouchInfo info)
		{
			if(info.phase == TouchPhase.Began && touchStart != null)
				touchStart();

			if(info.phase == TouchPhase.Ended && touchEnded != null)
				touchEnded();
		}
	}
}