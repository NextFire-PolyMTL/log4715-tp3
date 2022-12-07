using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle_fin_bas : MonoBehaviour
{
    [SerializeField] private Niveau_PlayerControler _PlayerControler;
    public static bool coll_fin_bas = false;
    // Start is called before the first frame update
    void Awake()
    {
        coll_fin_bas = false;
    }
    void OnTriggerEnter(Collider collider)
    {   
        if (collider.gameObject == _PlayerControler.gameObject)
        {
            coll_fin_bas = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == _PlayerControler.gameObject)
        {
            coll_fin_bas = false;
        }
    }
}
