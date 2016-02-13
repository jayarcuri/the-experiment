﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float grabRange = 5.0f;
	public float grabRadius = 5.0f;
	public DialogBubble dialogBubble;

	private bool inRangeInteract = false;
	private PlayerMovement p_Movement;
	private bool testFlag = false;
	// Use this for initialization
	void Start () {
		p_Movement = GetComponent<PlayerMovement> ();

	}

	void FixedUpdate() {
		// read inputs
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		p_Movement.Move (h, v);

		// Raycast from player to see what is in front of player
		bool interact = Input.GetKey (KeyCode.F);

		Ray grabRay = new Ray (transform.position, transform.forward);
		RaycastHit hit;
		if (Physics.SphereCast (grabRay, grabRadius, out hit, grabRange)){
			if (hit.transform.CompareTag ("Usable")) {
				inRangeInteract = true;
				if (interact) {
					hit.transform.gameObject.SendMessage ("Use", SendMessageOptions.DontRequireReceiver);
					// Testing dialog below
					if (!testFlag) {
						dialogBubble.SetDialogQueue (new DialogCard[] {
							new DialogCard (2000f, "Villain get the money like curls,\nthey just tryin' to get a nut like squirrels in his mad world..."),
							new DialogCard (2000f, "Land of milk and honey with the swirls,\nwhere reckless naked girls get necklaces of pearls...")
						});
						dialogBubble.DisplayNextCard ();
						testFlag = true;
					}
					// Items with usable tag will have a Use function
				}
			}
		}else {
			inRangeInteract = false;
		}

		// if in range with interactable object display interact button
        if (CanvasController.Instance)
        {
            if (inRangeInteract)
            {
                CanvasController.Instance.DisplayInteractButton(true);
            }
            else
            {
                CanvasController.Instance.DisplayInteractButton(false);
            }
        }
	}
}
