using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueSpace : MonoBehaviour
{

    [SerializeField]
    GameObject image_dialogue_heros;

    [SerializeField]
    GameObject text_heros;

    [SerializeField]
    GameObject text_heros2;

    [SerializeField]
    Camera Cam;

    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip clip_dialogue;

    public GameObject Shuttle;

    public float delai;
    
    Animator Shuttle_anim;

    Animator Cam_anim;
    public static bool affiche_text1 = false;
    public static bool affiche_text2 = false;
    public static bool begin_dialogue = false;

    public static bool ok = false;

    // Start is called before the first frame update

    void Awake()
    {
        Shuttle_anim = Shuttle.GetComponent<Animator>();
        Cam_anim = Cam.GetComponent<Animator>();
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
        if (begin_dialogue) 
        {   
            begin_dialogue = false;
            affiche_text1 = true;
            affiche_text2 = false;
        }

        if (affiche_text1 && Input.GetKeyDown (KeyCode.Space)) 
        {   
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
            Shuttle_anim.SetBool("fin", true);
            Cam_anim.speed = 0;
            StartCoroutine(Waitfinal());
        }

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

    IEnumerator WaitforStart()
    {
        yield return new WaitForSeconds(delai);
        Shuttle_anim.SetBool("debut", true);
        yield return new WaitForSeconds(0.7f);
        Cam_anim.SetBool("debut", true);
        yield return new WaitForSeconds(0.85f);
        Cam_anim.SetBool("debut", true);
        begin_dialogue = true;
    }
    IEnumerator WaitforDialogue()
    {
        yield return new WaitForSeconds(0.2f);
        ok = true;
    }

    IEnumerator Waitfinal()
    {
        yield return new WaitForSeconds(2f);
        Sky_time.change_scene = true;
        SceneManager.LoadScene("3 - SKY-cinematic 1");
    }


     

}
