using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle_fin_bas : MonoBehaviour
{
    [SerializeField] private Niveau_PlayerControler _PlayerControler;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider collider)
    {   
        if (collider.gameObject == _PlayerControler.gameObject)
        {
            T_4_5.coll_fin_bas = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == _PlayerControler.gameObject)
        {
            T_4_5.coll_fin_bas = false;
        }
    }
}
