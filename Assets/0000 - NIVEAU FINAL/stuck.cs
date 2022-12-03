using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stuck : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.parent = transform;
        }
        
    }

    // Update is called once per frame
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.parent = null;
        }
        
    }
}
