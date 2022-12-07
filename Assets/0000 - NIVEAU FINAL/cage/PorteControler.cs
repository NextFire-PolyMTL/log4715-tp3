using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteControler : MonoBehaviour
{
    [SerializeField] private Animator _porte;
    [SerializeField] private Animator _prisonnier;
    [SerializeField] private SkillsManager _skillsManager;
    [SerializeField] private GameObject feu_d_artif;
    private bool refaire=true;
    //[SerializeField] private GameObject _successMessage;
     private Animator garde1;
     private Animator garde2;
     private Animator garde3;
     private Animator garde4;
    //[SerializeField] private GameObject p;
    private GameObject win;
    private GameObject fade;
    
    


    void Start()
    {
        garde1=GameObject.Find("Top/Characters/garde1").GetComponent<Animator>();
        garde2=GameObject.Find("Top/Characters/garde2").GetComponent<Animator>();
        garde3=GameObject.Find("Top/Characters/garde3").GetComponent<Animator>();
        garde4=GameObject.Find("Top/Characters/garde1 (1)").GetComponent<Animator>();
        win=GameObject.Find("HUD/Canvas/Win");//.GetComponent<Animator>();
        fade=GameObject.Find("HUD/Canvas/victory_fade");//.GetComponent<Animator>();
        _prisonnier.Play("idle");
        
    

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Hero" && refaire)
        {
            _porte.Play("Ouverture", 0, 0.0f);
        
            refaire=false;
            //gameObject.SetActive(false);
            //_prisonnier.Play("Avance", 0, 0.0f);
            
            //SkillsManager.XP += 200;
            //_successMessage.SetActive(true);
            garde1.Play("Danse",0,0.0f);
            garde2.Play("Danse",0,0.0f);
            garde3.Play("Danse",0,0.0f);
            garde4.Play("Danse",0,0.0f);
            _prisonnier.Play("Danse",0,0.0f);
            feu_d_artif.SetActive(true);
           win.SetActive(true);
            
            win.GetComponent<Animator>().Play("gameoverIn");
            
        }
        else if (coll.gameObject.tag == "Hero"){
            StartCoroutine(waitabit());
        }
        
    }
    IEnumerator waitabit(){
        yield return new WaitForSeconds(4f);
        fade.SetActive(true);
        fade.GetComponent<Animator>().Play("death_fade_in");
       
         
    }
}
