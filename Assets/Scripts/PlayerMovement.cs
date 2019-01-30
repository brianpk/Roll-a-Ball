using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float hspeed = 1.0f;
    public float vspeed = 1.0f;
    public float speed = 10.0f;
    public float jumpSpeed = 5f;
    public Text countText;
    public Text goalText;
    public bool isGrounded;
    private int count;


    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 21;
        SetCount();
        goalText.text = "";
    }

    void FixedUpdate()
    {
        Movement();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Ground") && isGrounded == false)
        {
            isGrounded = true;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Capsule")
        {
            Destroy(other.gameObject);
            count = count - 1;
            SetCount();
        }
    }

    void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal * hspeed, 0.0f, moveVertical * vspeed);

        rb.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, 1, 0) * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void SetCount()
    {
        countText.text = "Coins left: " + count.ToString();
        if (count == 0)
        {
            goalText.text = "You got all of the coins! Head to the goal!";
        }
    }

 
}

