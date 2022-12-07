using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle_fin_bas : MonoBehaviour
{
    [SerializeField] private Niveau_PlayerControler _PlayerControler;
    [SerializeField] private GameObject Boule_Energie;
    public static bool coll_fin_bas = false;
    private ParticleSystem particle;
    // Start is called before the first frame update
    void Awake()
    {
        particle=Boule_Energie.GetComponent<ParticleSystem>();
        particle.startColor = Color.red;
        coll_fin_bas = false;
    }
    void OnTriggerEnter(Collider collider)
    {   
        if (collider.gameObject == _PlayerControler.gameObject)
        {
            particle.startColor = Color.green;
            coll_fin_bas = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == _PlayerControler.gameObject)
        {
            particle.startColor = Color.red;
            coll_fin_bas = false;
        }
    }
}
