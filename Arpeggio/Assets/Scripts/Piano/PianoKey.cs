using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Piano
{
	public class PianoKey : MonoBehaviour 
	{
		public UnityEngine.UI.Text text;
		private App.Input.InputObjectController inputControl;

		private bool began { get { return (inputControl.state == App.Input.InputState.began)? true : false; } }
		private bool stationary { get { return (inputControl.state == App.Input.InputState.stationary)? true : false; } }
		private bool end { get { return (inputControl.state == App.Input.InputState.end)? true : false; } }
		private bool off { get { return (inputControl.state == App.Input.InputState.off)? true : false; } }

		void Start()
		{
			inputControl = GetComponent<App.Input.InputObjectController>();

			if(inputControl == null)
				inputControl = gameObject.AddComponent<App.Input.InputObjectController>();
		}

		void Update()
		{
			play();
		}

		private void play()
		{
			string test = "Nothing";

			if(began)
				test = "Began";

			if(stationary)
				test = "Stationary";
			
			if(end)
				test = "End";

			if(off)
				test = "Off";

			text.text = test;
			Debug.Log(test);
		}
	}
}