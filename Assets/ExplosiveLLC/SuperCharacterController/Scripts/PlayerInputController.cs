﻿using System.Collections;
using UnityEngine;

namespace ExplosiveLLC.SuperCharacterController.Scripts {
	public class PlayerInputController : MonoBehaviour {

		public PlayerInput Current;
		public Vector2 RightStickMultiplier = new Vector2(3, -1.5f);

		// Use this for initialization
		void Start () {
			Current = new PlayerInput();
		}

		void Update () {
        
			// Retrieve our current WASD or Arrow Key input
			// Using GetAxisRaw removes any kind of gravity or filtering being applied to the input
			// Ensuring that we are getting either -1, 0 or 1
			Vector3 moveInput = new Vector3(UnityEngine.Input.GetAxisRaw("Horizontal"), 0, UnityEngine.Input.GetAxisRaw("Vertical"));

			Vector2 mouseInput = new Vector2(UnityEngine.Input.GetAxis("Mouse X"), UnityEngine.Input.GetAxis("Mouse Y"));

			Vector2 rightStickInput = new Vector2(UnityEngine.Input.GetAxisRaw("AimHorizontal"), UnityEngine.Input.GetAxisRaw("AimVertical"));

			// pass rightStick values in place of mouse when non-zero
			mouseInput.x = rightStickInput.x != 0 ? rightStickInput.x * RightStickMultiplier.x : mouseInput.x;
			mouseInput.y = rightStickInput.y != 0 ? rightStickInput.y * RightStickMultiplier.y : mouseInput.y;

			bool jumpInput = UnityEngine.Input.GetButtonDown("Jump"); 
			

			Current = new PlayerInput()
			{
				MoveInput = moveInput,
				MouseInput = mouseInput,
				JumpInput = jumpInput
			};
		}
	}

	public struct PlayerInput
	{
		public Vector3 MoveInput;
		public Vector2 MouseInput;
		public bool JumpInput;
	}
}