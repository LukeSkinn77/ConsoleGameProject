﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control_Movement : MonoBehaviour {

    public bool isAttacking = false;

	[Header("Player Inputs")]
	float vertInput;
	float horiInput;
    Vector3 combInput;

	[Header("Speed Values")]
	public float inputSpeed = 8.0f;
	public float maxSpeed = 15.0f;

	Player_Reference_Holder playerRefs;

	void Start () 
	{
		playerRefs = GetComponent<Player_Reference_Holder> ();
	}

	void Update () 
	{
		MovementInputRetrieve ();
		GroundMovement ();
	}

	void MovementInputRetrieve()
	{
		horiInput = Input.GetAxis ("Horizontal") * inputSpeed;
		vertInput = Input.GetAxis ("Vertical") * inputSpeed;
        combInput = new Vector3(horiInput, 0.0f, vertInput);
	}

	void GroundMovement()
	{
		Quaternion camQuad = playerRefs.camTran.transform.rotation;
        if (combInput != Vector3.zero)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, camQuad.eulerAngles.y, transform.eulerAngles.z);
            playerRefs.playerObject.localRotation = Quaternion.LookRotation (combInput, Vector3.up);
            if (!isAttacking)
            { 
                playerRefs.rb.AddRelativeForce(combInput, ForceMode.Impulse);
            }
            //playerRefs.playerObject.localRotation = Quaternion.LookRotation(transform.InverseTransformDirection(playerRefs.rb.velocity.x, 0.0f, playerRefs.rb.velocity.z), Vector3.up);

        }
        if (playerRefs.rb.velocity.magnitude > maxSpeed) 
		{
			playerRefs.rb.velocity = playerRefs.rb.velocity.normalized * maxSpeed;
		}
	}
}
