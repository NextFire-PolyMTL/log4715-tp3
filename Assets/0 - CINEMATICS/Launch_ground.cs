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

    public ParticleSystem particle1;
    public ParticleSystem particle2;
    public ParticleSystem particle3;
    public float début1;
    public float duree1;

    public float début2;
    public float duree2;

    public float début3;
    public float duree3;
    Animator Shuttle_anim;

    Animator Cam_anim;
    void Awake()
    {
        Shuttle_anim = Shuttle.GetComponent<Animator>();
        Cam_anim = Cam.GetComponent<Animator>();
        particle1.Pause();
        particle2.Pause();
        particle3.Pause();
    }
    // Start is called before the first frame update
    void Start()
    {
        Shuttle_anim.SetBool("shuttle", true);
        Cam_anim.SetBool("cam", true);
        StartCoroutine(Autodestruct1());
        StartCoroutine(Autodestruct2());
        StartCoroutine(Autodestruct3());
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
    IEnumerator Autodestruct1()
    {
        yield return new WaitForSeconds(début1);
        particle1.Play();
        yield return new WaitForSeconds(duree1);
        Destroy(particle1.gameObject);
    }
    IEnumerator Autodestruct2()
    {
        yield return new WaitForSeconds(début2);
        particle2.Play();
        yield return new WaitForSeconds(duree2);
        Destroy(particle2.gameObject);
    }
    IEnumerator Autodestruct3()
    {
        yield return new WaitForSeconds(début3);
        particle3.Play();
        yield return new WaitForSeconds(duree3);
        Destroy(particle3.gameObject);
    }
}
