using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotseat : MonoBehaviour 
{
	//Have an inactive camera
	public Camera mainCamera;
	public Camera blackoutCamera;
	public Canvas blackoutCanvas;

	//Enable it during hotseat, disable on button press
	// Use this for initialization
	public void SwapToBlack()
	{
		mainCamera.enabled = false;
		blackoutCamera.enabled = true;
		blackoutCanvas.enabled = true;
		//activate camera

	}
	
	public void SwapToMain()
	{
		blackoutCamera.enabled = false;
		mainCamera.enabled = true;
		blackoutCanvas.enabled = true;
	}
}
