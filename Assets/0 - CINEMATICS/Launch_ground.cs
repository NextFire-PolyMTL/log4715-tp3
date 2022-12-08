using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launch_ground : MonoBehaviour
{   


    [SerializeField]
    Camera Cam;

    public GameObject Shuttle;

    [SerializeField] private AudioSource _source;
    
    [SerializeField] AudioClip clip_explosion;
    [SerializeField] AudioClip clip_feu;

    [SerializeField] AudioClip clip_vaisseau;
    Animator Shuttle_anim;

    Animator Cam_anim;
    void Awake()
    {
        Shuttle_anim = Shuttle.GetComponent<Animator>();
        Cam_anim = Cam.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Shuttle_anim.SetBool("shuttle", true);
        Cam_anim.SetBool("cam", true);
        StartCoroutine(Change_scene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Change_scene()
    {   
        _source.PlayOneShot(clip_vaisseau);
        yield return new WaitForSeconds(2f);
        //_source.Stop(clip_vaisseau);
        _source.PlayOneShot(clip_explosion);
        yield return new WaitForSeconds(2f);
        _source.PlayOneShot(clip_feu);
        yield return new WaitForSeconds(9f);
        SceneManager.LoadScene("5 - AWAKE-cinematic");
    }
}
