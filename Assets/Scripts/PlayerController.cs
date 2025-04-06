using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{    
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb; 
    private int count;
    private float movementX;
    private float movementY;
    private float timesJumped;
    private float jumpForce = 4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent <Rigidbody>(); 
        count = 0;
        
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>(); 
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 14)
        {
            winTextObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space detected");
            if (timesJumped == 0 || timesJumped == 1)
            {
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                Debug.Log("\t\tTimes Jumped: " + timesJumped.ToString());
                timesJumped++;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count += 1;

            SetCountText();
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Found a collision: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Ground"))
        {
            timesJumped = 0;
        }
    }
}
