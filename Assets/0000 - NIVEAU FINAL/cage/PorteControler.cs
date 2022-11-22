using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteControler : MonoBehaviour
{
    [SerializeField] private Animator _Porte;
    [SerializeField] private Animator _Prisonnier;

    void Start()
    {

    }

    void Update()
    {

    }
    
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Hero")
        {
            _Porte.Play("Ouverture", 0, 0.0f);
            gameObject.SetActive(false);
            _Prisonnier.Play("Avance", 0, 0.0f);

        }
        if (coll.gameObject.tag == "Prisoner")
        {
            _Porte.Play("Fermeture", 0, 0.0f);
            //prisonnier.Play("idle");
            gameObject.SetActive(false);
        }
    }
}
