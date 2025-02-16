using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour {

	public float speed = 3.0f;
	public Vector3 camPos;
	[Header("Distance Values")]
	public float distance;
	public float minDist = 1f;
	public float maxDist = 4f;
    public float distModifier = 0.5f;

	void Start () 
	{
		camPos = transform.localPosition.normalized;
		distance = transform.localPosition.magnitude;
	}

	void Update()
	{
        CameraPosition();
	}

	void CameraPosition()
	{
		Vector3 camPosDes = transform.TransformPoint (camPos * maxDist);
		RaycastHit rayHit;

		if (Physics.Linecast (transform.parent.position, camPosDes, out rayHit)) 
		{
			distance = Mathf.Clamp ((rayHit.distance * distModifier), minDist, maxDist);
		} 
		else 
		{
			distance = maxDist;
		}

		transform.localPosition = Vector3.Lerp (transform.localPosition, camPos * distance, Time.deltaTime * speed);
	}
}