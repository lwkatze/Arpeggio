﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.Input;

namespace App.Input
{
	public class InputHandlerControl : MonoBehaviour
	{
		public Camera mainCamera;
		public LayerMask inputLayer;
		public float raycastDist = 100f;
		RaycastHit2D hit;

		void Start()
		{
			if(mainCamera == null)
				mainCamera = Camera.main;
		}

		void OnGUI()
		{
			getTouches();
		}

		void getTouches()
		{
			for(int i = 0; i < UnityEngine.Input.touches.Length; i++){
				hit = Physics2D.Raycast(UnityEngine.Input.touches[i].position, mainCamera.transform.forward, raycastDist, inputLayer);

				if(hit.collider != null){
					hit.collider.gameObject.SendMessage("OnTouchReceived", new TouchInfo(UnityEngine.Input.touches[i]));
				}
			}
		}
	}
}