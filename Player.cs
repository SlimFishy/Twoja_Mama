﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    public float maxspeed = 3;
    public float speed = 50f;
    public float jumppower = 150f;
    public bool grounded;
    public float mobilespeed;

    private Rigidbody2D rb2d;
    private Animator anim;


    // Use this for initialization
    void Start ()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        anim.SetBool("Ground", grounded);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        // Obrot postaci
        if (Input.GetAxis("Horizontal") > -0.1f || mobilespeed > 0)
        {
            transform.localScale = new Vector3(3, 3, 3);

        }
        if (Input.GetAxis("Horizontal") < -0.1f || mobilespeed < 0)
        {
            transform.localScale = new Vector3(-3, 3, 3);

        }
        // Skok
        if(Input.GetButtonDown("Jump") && grounded)
        {

            Jump();
            
        }

        
    }
    void FixedUpdate()
    {
        Vector2 Friction = rb2d.velocity;
        Friction.y = rb2d.velocity.y;
        Friction.x *= 0.6f;
        // Tarcie
        if (grounded)
        {
            rb2d.velocity = Friction;
        }
        // Predkosc Postaci PC
        float h = Input.GetAxis("Horizontal");
        rb2d.AddForce((Vector2.right * speed) * h);
        // Predkosc Postaci Mobile
        mobilespeed = CrossPlatformInputManager.GetAxis("Horizontal");
        rb2d.AddForce((Vector2.right * 1000) * mobilespeed);
        


        // Max Predkosc postaci
        if (rb2d.velocity.x > maxspeed)
        {
            rb2d.velocity = new Vector2(maxspeed, rb2d.velocity.y);
        }
        if (rb2d.velocity.x < -maxspeed)
        {
            rb2d.velocity = new Vector2(-maxspeed, rb2d.velocity.y);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Lava" || col.gameObject.tag == "Rock" || col.gameObject.tag == "Rock1")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (col.gameObject.tag == "Level Change")
        {
            SceneManager.LoadScene("2");
        }
        if (col.gameObject.tag == "Next Level")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void Jump()
    {
        if (grounded)
        {
            rb2d.AddForce(Vector2.up * jumppower);
        }
    }
    
}
