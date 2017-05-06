using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Input
{
	public delegate void TouchEvent(TouchInfo info);

	public enum InputState { none, began, changed, stationary, end, off, value };

	public class InputObjectController : MonoBehaviour 
	{
		public event TouchEvent touchStart;
		public event TouchEvent touchEnded;

		/*[HideInInspector]*/public InputState state;
		private InputState prevState;

		void Awake()
		{
			
		}

		//update the states
		void Update()
		{
			if(prevState == InputState.end)
				state = InputState.off;

			if(prevState == InputState.began)
				state = InputState.stationary;
			
			prevState = state;
		}

		void OnTouchReceived(App.Input.TouchInfo info)
		{
			if(info.phase == TouchPhase.Began){
				if(touchStart != null)
					touchStart(info);
				
				state = InputState.began;
			}

			if(info.phase == TouchPhase.Moved){
				state = InputState.changed;
			}

			if(info.phase == TouchPhase.Ended){
				if(touchEnded != null)
					touchEnded(info);
				
				state = InputState.end;
			}
		}

		void OnKeyDown()
		{
			state = InputState.began;
		}

		void OnKeyUp()
		{
			state = InputState.end;
		}
	}
}