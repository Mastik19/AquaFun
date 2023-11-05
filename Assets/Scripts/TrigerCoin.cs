using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerCoin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            PlayerManager.coins++;
            FindObjectOfType<AudioManager>().PlaySound("PickCoin");
            Destroy(gameObject);
        }
    }
}
