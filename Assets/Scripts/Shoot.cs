using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Shoot : MonoBehaviour
{
    public float speed;
     Rigidbody rb;
    public float YPos;
    int random;
    public int lane;
    public float xPos;
    Transform player;
    public float zPos;

    public TextMeshProUGUI arrowText;

    void Start()
    {
        arrowText = GetComponentInChildren<TextMeshProUGUI>();
        Physics.IgnoreLayerCollision(7, 8);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        lane = FindObjectOfType<PlayerController>().desiredLane;
        switch (lane)
        {
            case 0:
                xPos = -1.2f;
                break;
            case 1:
                xPos = 0;
                break;
            case 2:
                xPos = 1.2f;
                break;

        }

        random = Random.Range(0, 2);

        if(random == 0)
        {
            arrowText.text = "Down";
            arrowText.color = Color.yellow;
            YPos = 2.5f;
        }
        else
        {
            arrowText.text = "Up";
            arrowText.color = Color.green;
            YPos = 3.5f;
        }


        zPos = player.position.z;


        transform.position = new Vector3(xPos, YPos, zPos + 40);

        FindObjectOfType<AudioManager>().PlaySound("ArrowWoosh");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rb.AddForce(Vector3.back * speed * Time.fixedDeltaTime,ForceMode.Impulse);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag =="Player")
        {
            rb.isKinematic = true;
            PlayerManager.isGameOver = true;
        }
    }
}
