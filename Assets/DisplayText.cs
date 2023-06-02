﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class DisplayText : MonoBehaviour {


	public bool GuiOn;


	[Space(10)]
	[Header("The text to Display on Trigger")]
	[Tooltip("To edit the look of the text Go to Assets > Create > GUIskin. Add the new Guiskin to the Custom Skin proptery. If you select the GUIskin in your project tab you can now adjust the Label section to change this text")]
	public string Text = "Turn Back";

	[Tooltip("This is the window Box's size. It will be mid screen. Add or reduce the X and Y to move the box in Pixels. ")]
	public Rect BoxSize = new Rect( 0, -300, 200, 10);


	[Space(10)]
	[Tooltip("To edit the look of the text Go to Assets > Create > GUIskin. Add the new Guiskin to the Custom Skin proptery. If you select the GUIskin in your project tab you can now adjust the font, colour, size etc of the text")]
	public GUISkin customSkin;

	

	// if this script is on an object with a collider display the Gui
	void OnTriggerEnter(Collider collision) 

	{
		GameObject other = collision.GetComponent<Collider>().gameObject;
		if (other.gameObject.CompareTag("Player"))
        {
			GuiOn = true;
		}
			
	}


	void OnTriggerExit(Collider collision) 
	{
		GameObject other = collision.GetComponent<Collider>().gameObject;
		if (other.gameObject.CompareTag("Player"))
        {
			GuiOn = false;
		}
		
	}

	void OnGUI()
	{

		

		if (customSkin != null)
		{
			GUI.skin = customSkin;
		}

		if (GuiOn == true)
		{
			// Make a group on the center of the screen
			GUI.BeginGroup (new Rect ((Screen.width - BoxSize.width) / 2, (Screen.height - BoxSize.height) / 2, BoxSize.width, BoxSize.height));
			// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

			GUI.Box(new Rect(BoxSize), " ");
			GUI.Label(BoxSize, Text);
		
			// End the group we started above. This is very important to remember!
			GUI.EndGroup ();

		}


	}

}
