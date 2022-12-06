using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle_fin_haut : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Niveau_PlayerControler _PlayerControler;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider collider)
    {   
        if (collider.gameObject == _PlayerControler.gameObject)
        {
            T_4_5.coll_fin_haut = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == _PlayerControler.gameObject)
        {
            T_4_5.coll_fin_haut = false;
        }
    }
}
