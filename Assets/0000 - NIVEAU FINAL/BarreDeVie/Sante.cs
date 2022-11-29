using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sante : MonoBehaviour
{
    //Variable pour le setup de la lifebar;
    [SerializeField] private int _PV_max = 10;
    public static int PV_max = 10;
    public static int PV_actuels = PV_max;
    [SerializeField] private BarDeVie bar_de_vie;
    [SerializeField] private GameObject gameover;
    public int Degats_Projectiles = 1;
    public int Degats_Lave = 1;

    // Variables pour la lave
    //[SerializeField] LayerMask ThisFloorIsLava;
    [HideInInspector] public bool _isOnLava = false;
    private float intervalle;

    // Variables pour le piège:
    [SerializeField] private GameObject _Eboul;
    [SerializeField] private GameObject _Press;
    [HideInInspector] public bool _piegeActive;
    private float time;

    public void OnAfterDeserialize()
    {
        PV_max = _PV_max;
        PV_actuels = PV_max;
    }

    // Start is called before the first frame update
    void Start()
    {
        //eboul=GetComponent<GameObject>();
        _isOnLava = false;
        intervalle = 0;

        _piegeActive = false;
        time = 0;
        // Degats_Projectiles = 2;
        //Degats_Lave = 2;

        bar_de_vie.setLife(PV_actuels);
    }

    // Update is called once per frame
    void Update()
    {

        if (_isOnLava)
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
        }

    }

    public void Degats(int degats)
    {
        PV_actuels = Mathf.Max(PV_actuels - degats, 0);
        bar_de_vie.setLife(PV_actuels);
    }
}
