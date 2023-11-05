using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCollision : MonoBehaviour
{

    public float speed;
    
    public float YPos;
    int random;
    public int lane;
    public float xPos;
    Transform player;
    public float zPos;
    float zOffset;

    ParticleSystem bombExplosion;
    ParticleSystem head;
    void Start()
    {
        head = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        bombExplosion = transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
        Physics.IgnoreLayerCollision(7, 8);
        bombExplosion.Stop();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        lane = FindObjectOfType<PlayerController>().desiredLane;
        switch (lane)
        {
            case 0:
                xPos = 0;
                break;
            case 1:

                int rand = Random.Range(0, 2);

                if(rand==0)
                    xPos = -1.2f;
                else
                    xPos = 1.2f;

                break;
            case 2:
                xPos = 0;
                break;

        }

        YPos = 15;


        zPos = player.position.z;
        zOffset = 15;

        transform.position = new Vector3(xPos, YPos, zPos + zOffset);

        FindObjectOfType<AudioManager>().PlaySound("BombFalling");
    }







    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Tile")
        {
            bombExplosion.Play();
            head.Stop();

            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, 1);

            Destroy(collision.gameObject);

            FindObjectOfType<AudioManager>().PlaySound("BobmCollision");
            
        }
    }
}
