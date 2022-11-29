using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteControler : MonoBehaviour
{
    [SerializeField] private Animator _Porte;
    [SerializeField] private Animator _Prisonnier;
    private Niveau_PlayerControler playerctrl;
    private GameObject Message;



    void Start()
    {
        playerctrl=GameObject.Find("Top/Characters/CyberSoldier").GetComponent<Niveau_PlayerControler>();
        Message=GameObject.Find("HUD/Canvas/Succès");

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
            playerctrl.EnleverXP(-75);
            Message.SetActive(true);

        }
        if (coll.gameObject.tag == "Prisoner")
        {
            _Porte.Play("Fermeture", 0, 0.0f);
            //prisonnier.Play("idle");
            gameObject.SetActive(false);
            Message.SetActive(false);
        }
    }
}
