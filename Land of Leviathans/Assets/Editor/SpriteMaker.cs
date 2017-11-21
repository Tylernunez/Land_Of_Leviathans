using UnityEngine;
using System.IO;
using UnityEditor;
using System.Collections;

public class SpriteMaker : MonoBehaviour {

	public bool StartTakingImages; //If we want to start taking images
	int count = 0;

	public int framesTo = 10; //For how many frames do we want to start taking pictures of?
	int countFrames;

	public float MaxTimer =5;
	float timer;

	string path = "";


	// Update is called once per frame
	void Update () {
	
		if(StartTakingImages)
		{
			if(path.Equals(""))
			{
				path = EditorUtility.SaveFilePanel("Save Screenshots Folder", Application.dataPath, "", "");
				Debug.Log(path);

				if(path.Equals(""))
				{
					StartTakingImages = false;
				}
			}

			timer += Time.deltaTime;
			countFrames++;

			if(countFrames>framesTo)
			{
				StartCoroutine("CaptureScreenshots");
				countFrames = 0;
				Debug.Log("taking pictures");
			}

			if(timer>MaxTimer)
			{
				timer = 0;
				StartTakingImages = false;
			}

		}
	}

	IEnumerator CaptureScreenshots()
	{
		yield return new WaitForEndOfFrame();

		Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

		texture.ReadPixels(new Rect(0,0, Screen.width, Screen.height),0,0);
		texture.Apply();

		yield return 0;

		byte[] bytes = texture.EncodeToPNG();

		File.WriteAllBytes(path + count + ".png",bytes);

		count++;

		DestroyObject(texture);
	}
}
