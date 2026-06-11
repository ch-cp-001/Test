using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerObj : MonoBehaviour
{
    private Animator ani;
    private Rigidbody rb;

    private bool isJumping = false;
    public float len;

    private int hp = 100;
    void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        if(!isJumping)
            ani.SetFloat("xSpeed", v);

        if (v != 0)
        {
            rb.AddForce(transform.forward,ForceMode.Impulse);
        }

        if(isJumping && rb.velocity.y < 0.1f)
        {
            if (Physics.Raycast(transform.position, Vector3.down, len))
            {
                ani.SetBool("isJump", false);
                isJumping = false;
            }
        }

        if (!isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            ani.SetBool("isJump", true);
            isJumping= true;
        }

        if(isJumping)
            ani.SetFloat("ySpeed", rb.velocity.y);


    }


    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;   
        Gizmos.DrawLine(transform.position,transform.position+Vector3.down*len);
    }

}
