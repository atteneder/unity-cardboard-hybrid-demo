using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CbTest : MonoBehaviour {

	const int width = 300;
	const int height = 100;

	bool autoRotate = true;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void OnGUI () {
		bool isCb = XRSettings.loadedDeviceName == "cardboard";
		
		if (GUI.Button (new Rect (0, Screen.height-height, width, height), isCb ? "stop" : "start" )) {
			//StartCoroutine (SwitchToVr ());
			XRSettings.LoadDeviceByName ( isCb ? "None" : "cardboard" );
		}
		
		if (GUI.Button (new Rect (Screen.width-width, Screen.height-height, width, height), XRSettings.enabled ? "disable":"enable")) {
			bool oldVal = XRSettings.enabled;
			XRSettings.enabled = !oldVal;
			if(!oldVal) {
				StartCoroutine(WaitForLandscape());
			}
		}
 
		if (GUI.Button (new Rect (Screen.width-width, 0, width, height), autoRotate ? "go landscape" : "go auto")) {
			if(autoRotate) {
				//Screen.orientation = ScreenOrientation.LandscapeLeft;
				Screen.autorotateToPortrait = false;
				Screen.autorotateToLandscapeLeft = true;
				Screen.autorotateToLandscapeRight = false;
				Screen.autorotateToPortraitUpsideDown = false;
			} else {
				//Screen.orientation = ScreenOrientation.AutoRotation;
				Screen.autorotateToPortrait = true;
				Screen.autorotateToLandscapeLeft = true;
				Screen.autorotateToLandscapeRight = true;
				Screen.autorotateToPortraitUpsideDown = true;
			}
			autoRotate = !autoRotate;
		}
		
		GUI.skin.label.fontSize = 40;
		GUI.Label(new Rect(0,Screen.height/2,width,height), XRSettings.isDeviceActive?"active":"inactive");
		GUI.Label(
			new Rect(Screen.width - width, Screen.height / 2, width, height)
			,string.Format("{0}:{1}", Screen.orientation.ToString(), Input.deviceOrientation.ToString())
			);
	}

	IEnumerator SwitchToVr() {
		XRSettings.LoadDeviceByName ("cardboard");
		yield return null;
		XRSettings.enabled = true;
	}
	
	IEnumerator WaitForLandscape() {
		while( Screen.orientation != ScreenOrientation.LandscapeLeft ) { 
			yield return null;
		}
		XRSettings.LoadDeviceByName ("cardboard");
	}

	private void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			XRSettings.enabled = false;
			//XRSettings.LoadDeviceByName("None");
		}
	}
}
