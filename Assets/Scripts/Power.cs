using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour {



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (FindObjectOfType<Player>().guns < 3)
            {
                FindObjectOfType<Player>().guns++;
                Destroy(gameObject);
            }
            
        }
    }
}
