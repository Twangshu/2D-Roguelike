using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSuit : MonoBehaviour
{
	void Start()
	{
		int ManualWidth = 1600;
		int ManualHeight = 900;
		int manualHeight;
		if (System.Convert.ToSingle(Screen.height) / Screen.width > System.Convert.ToSingle(ManualHeight) / ManualWidth)
			manualHeight = Mathf.RoundToInt(System.Convert.ToSingle(ManualWidth) / Screen.width * Screen.height);
		else
			manualHeight = ManualHeight;
		Camera camera = GetComponent<Camera>();
		float scale = System.Convert.ToSingle(manualHeight / 900f);
		camera.fieldOfView *= scale;
	}
}
