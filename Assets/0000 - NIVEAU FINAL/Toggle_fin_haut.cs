using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle_fin_haut : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Niveau_PlayerControler _PlayerControler;
    public static bool coll_fin_haut = false;
    [SerializeField] private GameObject Boule_Energie;
    private ParticleSystem particle;
    // Start is called before the first frame update
    void Awake()
    {
        particle=Boule_Energie.GetComponent<ParticleSystem>();
        particle.startColor = Color.red;
        coll_fin_haut = false;
    }
    void OnTriggerEnter(Collider collider)
    {   
        if (collider.gameObject == _PlayerControler.gameObject)
        {
            particle.startColor = Color.green;
            coll_fin_haut = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == _PlayerControler.gameObject)
        {
            particle.startColor = Color.red;
            coll_fin_haut = false;
        }
    }
}
