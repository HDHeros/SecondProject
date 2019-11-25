using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCubeJump : MonoBehaviour
{   
    Rigidbody2D rb;
    float horizontal;

    bool firstStart = true;//Скрипт запущен только что

    void Start(){

    }
    void Update()
    {
        if(!firstStart)
            if(Application.platform == RuntimePlatform.Android)
            {
                horizontal = Input.acceleration.x;
                rb.velocity = new Vector2(horizontal * 12f, rb.velocity.y);
            }
            else
            {
                horizontal = Input.GetAxis("Horizontal");
                rb.velocity = new Vector2(horizontal * 7f, rb.velocity.y);

            }
    }
    void OnCollisionEnter2D(Collision2D collis)
    {
        
        if(firstStart)
        {
            rb = GetComponent<Rigidbody2D> ();
            firstStart = false;
            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            mainCamera.GetComponent <LevelGenerate>().cubeInGame = true;
        }
        if(collis.gameObject.tag == "Platform" && rb.velocity.y <= 0)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(transform.up * 14, ForceMode2D.Impulse);
        }
        if(collis.gameObject.tag == "HighJumpPlatform" && rb.velocity.y <= 0)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(transform.up * 24, ForceMode2D.Impulse);
        }
        if(collis.gameObject.tag == "Wall")
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(rb.velocity.x, rb.velocity.y + 10), ForceMode2D.Impulse);

        }
    }
}
