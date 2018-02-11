using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class RockReset : MonoBehaviour
{
    public Vector3 Rock;
    public Vector3 Rock1;
    private Rigidbody2D rbd2d;
    void Start()
    {
        rbd2d = gameObject.GetComponent<Rigidbody2D>();
        Rock = GameObject.FindGameObjectWithTag("Rock").transform.position;
        Rock1 = GameObject.FindGameObjectWithTag("Rock1").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rbd2d.velocity = new Vector2(0, -8);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Teleport")
        {
            
            transform.position = Rock;
            
        }
        if (col.gameObject.tag == "Teleport1")
        {
            transform.position = Rock1;
            
        }
    }
}