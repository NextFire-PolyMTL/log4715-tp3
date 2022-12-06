using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class animation_ground : MonoBehaviour
{   

    Animator _Anim { get; set; }
    [SerializeField] Camera _MainCamera;

    [SerializeField] Light Point_Light; 

    [SerializeField] GameObject Hero;

    [SerializeField] GameObject Shadow_soldier;

    [SerializeField] GameObject Fondu;

    [SerializeField] GameObject Eye1; 

    [SerializeField] GameObject Eye2; 

    [SerializeField] GameObject Plane;

    [SerializeField]
    GameObject image_dialogue_heros;

    [SerializeField]
    GameObject text_heros;

    [SerializeField]
    GameObject text_heros2;

    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip clip_dialogue;

    [SerializeField]
    GameObject text_fin;

    Animator _AnimCam;
    Animator _AnimFin;
    Animator _AnimPlane;
    Animator _AnimTxt;

    public static bool affiche_text1 = false;
    public static bool affiche_text2 = false;
    public static bool begin_dialogue = false;
    public static bool ok = false;

    // Start is called before the first frame update
    void Awake()
    {
        _Anim = GetComponent<Animator>();
        _AnimCam = _MainCamera.GetComponent<Animator>();
        _AnimFin = Fondu.GetComponent<Animator>();
        _AnimPlane = Plane.GetComponent<Animator>();
        _AnimTxt = text_fin.GetComponent<Animator>();
    }

    void Start()
    {   
        image_dialogue_heros.SetActive(false);
        text_heros.SetActive(false);
        text_heros2.SetActive(false);
        StartCoroutine(WaitforStart());
    }

    // Update is called once per frame
    void Update()
    {   
        if (affiche_text1)
        {   
            image_dialogue_heros.SetActive(true);
            text_heros.SetActive(true);
            text_heros2.SetActive(false);
        }
        else if (affiche_text2)
        {   
            image_dialogue_heros.SetActive(true);
            text_heros.SetActive(false);
            text_heros2.SetActive(true);
        }
        else
        {   
            image_dialogue_heros.SetActive(false);
            text_heros.SetActive(false);
            text_heros2.SetActive(false);
        }

    }
    void FixedUpdate()
    {   
        if (begin_dialogue) 
        {   
            begin_dialogue = false;
            affiche_text1 = true;
            affiche_text2 = false;
        }

        if (affiche_text1 && Input.GetKeyDown (KeyCode.Space)) 
        {   
            _Anim.Play("Buff");
            _AnimCam.Play("ground_camera_animator");
            source.PlayOneShot(clip_dialogue);
            affiche_text1 = false;
            affiche_text2 = true;
            StartCoroutine(WaitforDialogue());
        }

        if (ok && affiche_text2 && Input.GetKeyDown (KeyCode.Space))
        {   
            source.PlayOneShot(clip_dialogue);
            affiche_text1 = false;
            affiche_text2 = false;
            _AnimCam.Play("rotate_ground");
            _AnimFin.Play("fondu_final");
            _AnimPlane.Play("plane");
            _AnimTxt.Play("chapitre1");
            StartCoroutine(Shadow_appear());
            StartCoroutine(Decrease_light());
        }

    }

    IEnumerator Decrease_light()
    {   for(int i = 1; i < 11; i++)
        {   
            yield return new WaitForSeconds(1/5f);
            Point_Light.intensity = 4.0f - (4 * i)/10;
        }
    }
    IEnumerator Shadow_appear()
    {   
        yield return new WaitForSeconds(1);
        Shadow_soldier.SetActive(true);
        Hero.SetActive(false);
        Load_game.change_scene = true;
    }

    IEnumerator WaitforStart()
    {   
        _AnimFin.Play("fondu_init");
        _Anim.Play("StunnedLoop");
        yield return new WaitForSeconds(1f);
        begin_dialogue = true;
    }

    IEnumerator WaitforDialogue()
    {
        yield return new WaitForSeconds(0.5f);
        ok = true;
    }


}
