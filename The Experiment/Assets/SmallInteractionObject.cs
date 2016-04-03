﻿using UnityEngine;
using System.Collections;

public class SmallInteractionObject : MonoBehaviour 
{
	//public Renderer outlineRenderer;
	public Vector3 interactionRotation;
	public Vector3 interactionScale;
	public DialogCard objectDialog;
	public GameObject interactionObject;

	public bool isUseable = true;
	public bool disableAfterUse = true;

	InteractionCameraView interactionCameraView;
	GameObject player;

	public bool Clicked { get; private set; }

	void Start()
	{
		//outlineRenderer.enabled = false;

		interactionScale = transform.localScale;
		interactionRotation = transform.localEulerAngles;
		interactionObject = gameObject;
		interactionCameraView = FindObjectOfType<InteractionCameraView> ();

		player = GameObject.FindGameObjectWithTag ("Player");


		if (player == null)
			Debug.LogError ("Player not found; set its tag to Player");

		interactionCameraView.AddToDictionary (this);
	}

	/*void OnMouseOver()
	{
		if (isUseable)
			outlineRenderer.enabled = true;
	}

	void OnMouseExit()
	{
		if (isUseable)
			outlineRenderer.enabled = false;
	}*/

	void OnMouseDown()
	{
		if (isUseable && !interactionCameraView.IsDisplaying()) {
			print ("Mouse click registered");
			// Add stopped movement
			interactionCameraView.DisplayObject (name, player.transform.localEulerAngles);
			Clicked = true;
			if (disableAfterUse)
				isUseable = false;
		}
	}
}