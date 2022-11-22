using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Lifebar;
public class Sante : MonoBehaviour
{
    //Variable pour le setup de la lifebar;
    public int PV_max = 10;
    public int PV_actuels;
    [SerializeField] private BarDeVie bar_de_vie;
    [SerializeField] private GameObject gameover;
    [SerializeField] int Degats_Projectiles;
    [SerializeField] int Degats_Lave;

    // Variables pour la lave
    //[SerializeField] LayerMask ThisFloorIsLava;
    bool _Lavaed { get; set; }
    private float intervalle;

    // Variables pour le piège:
    [SerializeField] private GameObject _Eboul;
    [SerializeField] private GameObject _Press;
    private bool active;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        PV_actuels = PV_max;
        //eboul=GetComponent<GameObject>();
        _Lavaed = false;
        intervalle = 0;

        active = false;
        time = 0;
        Degats_Projectiles = 2;
        Degats_Lave = 2;
    }

    // Update is called once per frame
    void Update()
    {

        if (_Lavaed)
        {
            intervalle += Time.deltaTime;
            if (intervalle > 0.5)
            {
                Degats(Degats_Lave);
                intervalle = 0;
            }

        }
        if (PV_actuels <= 0)
        {
            gameover.SetActive(true);
        }
        if (PV_actuels > 0)
        {
            gameover.SetActive(false);
        }


        if (active == true)
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
        }

    }

    void Degats(int degats)
    {
        if (PV_actuels - degats >= 0)
        {
            PV_actuels = PV_actuels - degats;
        }
        else
        {
            PV_actuels = 0;
        }

        bar_de_vie.setLife(PV_actuels);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer == 9)
        {
            _Lavaed = true;
        }  //créer une animation?
        if (coll.gameObject.layer == 10)
        {
            active = true;
        }
        if (coll.gameObject.tag == "projectile")
        {
            Degats(Degats_Projectiles);

        }
    }

}