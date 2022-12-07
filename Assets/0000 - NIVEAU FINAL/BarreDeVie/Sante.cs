using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sante : MonoBehaviour
{
    //Variable pour le setup de la lifebar;
    [SerializeField] private int _PV_max = 10;
    public static int PV_max = 10;
    public static int PV_actuels = PV_max;
    [SerializeField] private BarDeVie bar_de_vie;
    [SerializeField] private GameObject gameover;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject Fade;
    [SerializeField] private GameObject hero;
    [SerializeField] private GameObject shadow;
    public int Degats_Projectiles = 1;
    public int Degats_Lave = 1;

    public static int set_degats = 0; // Pour utiliser la fonction Degats

    // Variables pour la lave
    //[SerializeField] LayerMask ThisFloorIsLava;
    [HideInInspector] public bool _isOnLava = false;
    private float intervalle;

    // Variables pour le piège:
    [SerializeField] private GameObject _Eboul;
    [SerializeField] private GameObject _Press;
    [HideInInspector] public bool _piegeActive;

    public static bool niveau_diff = false; // false = facile, true = difficile

    private float time;
    private Animator _animGameOver;
    private Animator _animDeathScreen;
    private Animator _animFade;

    private Animator _animHero;

    private Animator _animShadow;

    private bool first_death = true;



    public void OnAfterDeserialize()
    {
        PV_max = _PV_max;
        PV_actuels = PV_max;
    }

    void Awake()
    {
        _animGameOver = gameover.GetComponent<Animator>();
        _animDeathScreen = deathScreen.GetComponent<Animator>();
        _animFade = Fade.GetComponent<Animator>();
        _animHero = hero.GetComponent<Animator>();
        _animShadow = shadow.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Niveau_PlayerControler.DialogueStop = true;
        if (!Niveau_PlayerControler.StartOpening)
        {
            _animFade.Play("Out");
        }
        //eboul=GetComponent<GameObject>();
        _isOnLava = false;
        intervalle = 0;
        deathScreen.SetActive(false);

        _piegeActive = false;
        time = 0;
        // Degats_Projectiles = 2;
        //Degats_Lave = 2;

        bar_de_vie.setLife(PV_actuels);
        Niveau_PlayerControler.DialogueStop = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("FPS : " + 1.0f / Time.deltaTime);

        if (set_degats != 0)
        {
            Degats(set_degats);
            set_degats = 0;
        }

        if (_isOnLava)
        {
            intervalle += Time.deltaTime;
            if (intervalle > 0.5)
            {
                Degats(Degats_Lave);
                intervalle = 0;
            }

        }
        if (PV_actuels <= 0 && first_death)
        {
            first_death = false;
            StartCoroutine(Death());//gameover.SetActive(true);
        }
        if (PV_actuels > 0)
        {
            gameover.SetActive(false);
            deathScreen.SetActive(false);
        }


        if (_piegeActive == true)
        {
            //trap1.SetActive(true);
            //trap2.SetActive(true);
            //trap3.SetActive(true);
            //trap4.SetActive(true);
            //trap5.SetActive(true);
            _Eboul.SetActive(true);
            time += Time.deltaTime;
        }
        else
        {
            _Eboul.SetActive(false);
        }
        if (time == 0.8)
        {
            Degats(5);
        }
        if (time > 1)
        {
            _Eboul.SetActive(false);
            _piegeActive = false;
        }

    }

    public void Degats(int degats)
    {
        PV_actuels = Mathf.Max(PV_actuels - degats, 0);
        bar_de_vie.setLife(PV_actuels);
    }

    IEnumerator Death()
    {
        _animHero.Play("MeleeWarrior@Death01_A");
        _animShadow.Play("MeleeWarrior@Death01_A");
        gameover.SetActive(true);
        _animGameOver.Play("gameoverIn");
        yield return new WaitForSeconds(2f);
        deathScreen.SetActive(true);
        _animDeathScreen.Play("death_fade_in");
        yield return new WaitForSeconds(2.5f);
        _animGameOver.Play("gameOverOut");
        yield return new WaitForSeconds(1f);
        gameover.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        PV_actuels = PV_max;
        Niveau_PlayerControler.s_gameOver = false;
        Scene scene = SceneManager.GetActiveScene();
        if (niveau_diff == false)
        {
            SceneManager.LoadScene(scene.name);
        }
        else
        {
            if (char.GetNumericValue(scene.name[0]) < 5)
            {
                SceneManager.LoadScene("1 - previllage");
            }
            else 
            {
                SceneManager.LoadScene("5 - Village Hub");
            }
        }
    }
}   
