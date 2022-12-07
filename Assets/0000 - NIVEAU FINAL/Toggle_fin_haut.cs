using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle_fin_haut : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Niveau_PlayerControler _PlayerControler;
    public static bool coll_fin_haut = false;
    // Start is called before the first frame update
    void Awake()
    {
        coll_fin_haut = false;
    }
    void OnTriggerEnter(Collider collider)
    {   
        if (collider.gameObject == _PlayerControler.gameObject)
        {
            coll_fin_haut = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == _PlayerControler.gameObject)
        {
            coll_fin_haut = false;
        }
    }
}
