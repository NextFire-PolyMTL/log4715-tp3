using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brume_mag : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // Debug.Log("uubdesbbs");
            Sante.set_degats = Sante.PV_actuels;
        }

    }
}
