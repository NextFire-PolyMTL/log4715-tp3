using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteControler : MonoBehaviour
{
    [SerializeField] private Animator _porte;
    [SerializeField] private Animator _prisonnier;
    [SerializeField] private SkillsManager _skillsManager;
    //[SerializeField] private GameObject _successMessage;
     private Animator garde1;
     private Animator garde2;
     private Animator garde3;
     private Animator garde4;
    //[SerializeField] private GameObject p;
    
    


    void Start()
    {
        garde1=GameObject.Find("Top/Characters/garde1").GetComponent<Animator>();
        garde2=GameObject.Find("Top/Characters/garde2").GetComponent<Animator>();
        garde3=GameObject.Find("Top/Characters/garde3").GetComponent<Animator>();
        garde4=GameObject.Find("Top/Characters/garde1 (1)").GetComponent<Animator>();
        

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Hero")
        {
            _porte.Play("Ouverture", 0, 0.0f);
            gameObject.SetActive(false);
            //_prisonnier.Play("Avance", 0, 0.0f);
            
            SkillsManager.XP += 75;
            //_successMessage.SetActive(true);

        }
        if (coll.gameObject.tag == "Prisoner")
        {
            _porte.Play("Fermeture", 0, 0.0f);
            garde1.Play("Danse",0,0.0f);
            garde2.Play("Danse",0,0.0f);
            garde3.Play("Danse",0,0.0f);
            garde4.Play("Danse",0,0.0f);
            _prisonnier.Play("Danse",0,0.0f);
            //prisonnier.Play("idle");
            gameObject.SetActive(false);
            //_successMessage.SetActive(false);
        }
    }
}
