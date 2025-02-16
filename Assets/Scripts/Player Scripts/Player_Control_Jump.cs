﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control_Jump : MonoBehaviour {

	//Player_Control_Ground_Check playerCGC;
	//Rigidbody rb;

	Player_Reference_Holder playerRefs;

	public float jumpForce;

	void Start ()
	{
		playerRefs = GetComponent<Player_Reference_Holder> ();
	}
	
	void Update () 
	{
		if (Input.GetButtonDown ("Jump") && playerRefs.playerPCM.isRunning) 
		{
			if (playerRefs.playerCGC.groundJump) 
			{
                playerRefs.anim.SetTrigger("RunningJump");
                playerRefs.rb.velocity = new Vector3 (playerRefs.rb.velocity.x, 0, playerRefs.rb.velocity.z);
				playerRefs.rb.AddForce (new Vector3 (0.0f, jumpForce, 0.0f), ForceMode.Impulse);
			}
			if ((!playerRefs.playerCGC.groundJump) && (!playerRefs.playerCGC.doubleJump)) 
			{
                playerRefs.anim.SetTrigger("RunningJump");
                playerRefs.rb.velocity = new Vector3 (playerRefs.rb.velocity.x, 0, playerRefs.rb.velocity.z);
				playerRefs.rb.AddForce (new Vector3 (0.0f, jumpForce, 0.0f), ForceMode.Impulse);
				playerRefs.playerCGC.doubleJump = true;
			}
		}
        if (Input.GetButtonDown("Jump") && playerRefs.playerPCM.isIdle)
        {
            if (playerRefs.playerCGC.groundJump)
            {
                playerRefs.anim.SetTrigger("StandingJump");
                playerRefs.rb.velocity = new Vector3(playerRefs.rb.velocity.x, 0, playerRefs.rb.velocity.z);
                playerRefs.rb.AddForce(new Vector3(0.0f, jumpForce, 0.0f), ForceMode.Impulse);
            }
            if ((!playerRefs.playerCGC.groundJump) && (!playerRefs.playerCGC.doubleJump))
            {
                playerRefs.anim.SetTrigger("StandingJump");
                playerRefs.rb.velocity = new Vector3(playerRefs.rb.velocity.x, 0, playerRefs.rb.velocity.z);
                playerRefs.rb.AddForce(new Vector3(0.0f, jumpForce, 0.0f), ForceMode.Impulse);
                playerRefs.playerCGC.doubleJump = true;
            }
        }
    }
}
