using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueScriptSoldat : MonoBehaviour
{
    [SerializeField] private Niveau_PlayerControler _PlayerControler;
    [SerializeField] private GameObject _VisualCue;

    [SerializeField]
    GameObject image_dialogue_soldat;

    [SerializeField]
    GameObject image_dialogue_heros;

    [SerializeField]
    GameObject text_soldat;


    [SerializeField]
    GameObject text_soldat_m;

    [SerializeField]
    GameObject text_soldat_v;

    [SerializeField]
    GameObject text_heros;

    [SerializeField]
    GameObject text_soldat_p;

    [SerializeField]
    GameObject Shared;

    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip clip_dialogue;

    //[SerializeField]
    //RectTransform fader;

    //[SerializeField]
    //AudioSource source;

    public bool lance_mort=false;
    public bool lance_tourne=false;

    //[SerializeField]
    //AudioClip clip_dialogue;

    private bool affiche_d_m = false;
    private bool affiche_d_h = false;
    private bool affiche_d_h3=false;
    private bool affiche_d_h2=false;
    private bool affiche_d_h4=false;
    private bool begin_dialogue = false;
    public static bool start_opening = false;
    private bool mort=false;
    private bool affiche_d_h22=false;

    public bool passe_par_fin=false; // Pour retour au village, savoir s'il est passé par la fin

    private GameObject viewzone;
    private GameObject targetzone;



    // Start is called before the first frame update
    void Start()
    {
        //if (start_opening)
       // {
        //    start_opening = false;
         //   LeanTween.scale(fader, new Vector3(10, 10, 10), 0);
          //  fader.gameObject.SetActive(true);
       //     LeanTween.scale(fader, Vector3.zero, 1.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
     //       {
     //           fader.gameObject.SetActive(false);
     //       });
    //    }
     //   else
      //  {
      //      fader.gameObject.SetActive(false);
      //  }
        _VisualCue.SetActive(false);
        Shared=GameObject.Find("Shared");
        viewzone=GameObject.Find("Top/Characters/garde1/viewZone");
        targetzone=GameObject.Find("Top/Characters/garde1/targetZone");
    }

    // Update is called once per frame
    void Update()
    {

        if (begin_dialogue && Input.GetKeyDown(KeyCode.I)) // Phase 1 dialogue : le soldat apostrophe le héro
        {
            source.PlayOneShot(clip_dialogue);
            Niveau_PlayerControler.DialogueStop = true; // On gèle les mouvements du joueur
            affiche_d_m = true;
            affiche_d_h = false;
            affiche_d_h2=false;
            affiche_d_h22=false;
            affiche_d_h3=false;
            affiche_d_h4=false;
            image_dialogue_soldat.SetActive(true);
            text_soldat.SetActive(true);
           // text_soldat2.SetActive(true);
            text_soldat_m.SetActive(false);
            text_soldat_v.SetActive(false);
            text_soldat_p.SetActive(false);
            image_dialogue_heros.SetActive(false);
            text_heros.SetActive(false);
        }

        else if (affiche_d_m && Input.GetKeyDown(KeyCode.Space)) // Phase 2 dialogue : le joueur répond 
        {
            source.PlayOneShot(clip_dialogue);
            Niveau_PlayerControler.DialogueStop = true; 
            affiche_d_m = false;
            affiche_d_h = false;
            affiche_d_h2=false;
            affiche_d_h22=true;
            affiche_d_h3=false;
            affiche_d_h4=false;
            image_dialogue_heros.SetActive(true);
            text_heros.SetActive(true);
            image_dialogue_soldat.SetActive(false);
            text_soldat.SetActive(false);
           // text_soldat2.SetActive(false);
            text_soldat_m.SetActive(false);
            text_soldat_v.SetActive(false);
            text_soldat_p.SetActive(false);
        }

        else if (affiche_d_h2 && Input.GetKeyDown(KeyCode.Space))
        {
            source.PlayOneShot(clip_dialogue);
            Niveau_PlayerControler.DialogueStop = true; 
            affiche_d_h = false;
            affiche_d_h2=false;
            affiche_d_h22=true;
            affiche_d_h3=false;
            affiche_d_h4=false;
            image_dialogue_heros.SetActive(false);
            text_heros.SetActive(false);
            image_dialogue_soldat.SetActive(false);
            text_soldat.SetActive(false);
           // text_soldat2.SetActive(false);
            text_soldat_m.SetActive(false);
            text_soldat_v.SetActive(false);
            text_soldat_p.SetActive(false);
            StartCoroutine(StopTime());

        }

        else if (affiche_d_h22 && Input.GetKeyDown(KeyCode.N)) // Le joueur choisit 'n' 
        {
            source.PlayOneShot(clip_dialogue);
            Niveau_PlayerControler.DialogueStop = true; 
            affiche_d_h = false;
            affiche_d_h2=false;
            affiche_d_h22=false;
            affiche_d_h3=true;
            affiche_d_h4=false;
            image_dialogue_heros.SetActive(false);
            text_heros.SetActive(false);
            image_dialogue_soldat.SetActive(true);
            text_soldat.SetActive(false);
           // text_soldat2.SetActive(false);
            text_soldat_m.SetActive(true);
            text_soldat_v.SetActive(false);
           
            text_soldat_p.SetActive(false);
            //StartCoroutine(StopTime());
            //CloseScene();
            StartCoroutine(FewTime());
            
        }else if (affiche_d_h3 && Input.GetKeyDown(KeyCode.Space)) // Le joueur choisit 'n' (suite et mort)
        {   
            source.PlayOneShot(clip_dialogue);
            lance_mort=true;
            Niveau_PlayerControler.DialogueStop = true; 
            lance_tourne=false;
            text_soldat_m.SetActive(false);
            image_dialogue_soldat.SetActive(false);
        }
        else if (affiche_d_h22 && Input.GetKeyDown(KeyCode.Y)) // Le joueur choisit 'y'
        {
            source.PlayOneShot(clip_dialogue);
            Niveau_PlayerControler.DialogueStop = true; 
            affiche_d_h = false;
            affiche_d_h2=false;
            affiche_d_h22=false;
            affiche_d_h3=false;
            affiche_d_h4=false;
            image_dialogue_heros.SetActive(false);
            text_heros.SetActive(false);
            text_soldat.SetActive(false);
            text_soldat_m.SetActive(false);
            text_soldat_v.SetActive(false);
            text_soldat_p.SetActive(false);
            image_dialogue_soldat.SetActive(false);
            
            bool pasAssez = Shared.GetComponent<SkillsManager>().EnleverXP(700);
            if (pasAssez==false){
                text_soldat_p.SetActive(true);
                image_dialogue_soldat.SetActive(true);
                affiche_d_h4=true;
                
                
            }
            else{
                lance_mort=false;
                lance_tourne=true;
                Destroy(_VisualCue);
                viewzone.SetActive(false);
                targetzone.SetActive(false);
                Niveau_PlayerControler.DialogueStop = false; 
            }
            //StartCoroutine(StopTime());
            //CloseScene();
        } else if (affiche_d_h4 && Input.GetKeyDown(KeyCode.Y)){
            source.PlayOneShot(clip_dialogue);
            Niveau_PlayerControler.DialogueStop = true; 
            
            affiche_d_h =false; 
            affiche_d_h2=false;
            affiche_d_h3=false;
            affiche_d_h4=false;
            image_dialogue_heros.SetActive(false);
            text_heros.SetActive(false);
            text_soldat.SetActive(false);
            text_soldat_m.SetActive(false);
            text_soldat_v.SetActive(false);
            text_soldat_p.SetActive(false);
            image_dialogue_soldat.SetActive(false);
            T_10_11.retourMenu=true;
            passe_par_fin=true;
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == _PlayerControler.gameObject)
        {
            _VisualCue.SetActive(true);
            begin_dialogue = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == _PlayerControler.gameObject)
        {
            _VisualCue.SetActive(false);
            begin_dialogue = false;
        }
    }

    

    

    IEnumerator StopTime()
    {
        yield return new WaitForSeconds(0.5f);
        Niveau_PlayerControler.DialogueStop = false;

    }
    IEnumerator FewTime()
    {
        yield return new WaitForSeconds(4);
        Niveau_PlayerControler.DialogueStop = false;

    }
}
