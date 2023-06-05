using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayEndKills : MonoBehaviour
{
	public bool GuiOn;

	public int kills;

	public string Text = "Turn Back";


	public Rect BoxSize = new Rect(0, -300, 200, 10);

	[Space(10)]
	[Tooltip("To edit the look of the text Go to Assets > Create > GUIskin. Add the new Guiskin to the Custom Skin proptery. If you select the GUIskin in your project tab you can now adjust the font, colour, size etc of the text")]
	public GUISkin customSkin;

	GUIStyle messageFont;


	void Start()
	{
		messageFont = new GUIStyle();
		messageFont.fontSize = 20;
		messageFont.alignment = TextAnchor.MiddleCenter;
		messageFont.normal.textColor = new Color32(220, 60, 60, 254);


	}

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

		kills = GameManager.Instance.enemiesKilled;

		if (customSkin != null)
		{
			GUI.skin = customSkin;
		}

		if (GuiOn == true)
		{

			GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 200, 200, 30), " ");
			GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 200, 200, 30), "Total enemies killed: " + kills, messageFont);

		}


	}

}

