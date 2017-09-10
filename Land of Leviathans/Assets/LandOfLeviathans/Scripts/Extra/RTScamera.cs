using UnityEngine;
using System.Collections;

public class RTScamera : MonoBehaviour {

	public float horizontalSpeed;
	public float verticalSpeed;
	public float cameraRotateSpeed;
	public float UpwardsSpeed;
	
	// Update is called once per frame
	void Update () 
	{
		float Horizontal = Input.GetAxis ("Horizontal") * horizontalSpeed * Time.deltaTime;
		float Vertical = Input.GetAxis ("Vertical") * verticalSpeed * Time.deltaTime;

		transform.Translate (Vector3.forward * Vertical);
		transform.Translate (Vector3.right * Horizontal);

		float Rotation = Input.GetAxis("Rotation");

		if(Input.GetAxis("Rotation") != 0)
		{
			transform.Rotate(Vector3.up, Rotation * cameraRotateSpeed * Time.deltaTime, Space.World);
		}

		float middle = Input.GetAxis ("Mouse ScrollWheel");

		if(middle > 0.1)
		{
			transform.Translate(Vector3.up * UpwardsSpeed * Time.deltaTime);
		}

		if(middle < -0.1)
		{
			transform.Translate(-Vector3.up * UpwardsSpeed * Time.deltaTime);
		}
	}
}
