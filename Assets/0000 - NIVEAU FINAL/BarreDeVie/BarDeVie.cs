using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarDeVie : MonoBehaviour
{
    public Slider sante;

    public void setLife(int pv)
    {
        sante.value = pv;
    }
    public void setMaxLife(int maxpv)
    {
        sante.maxValue = maxpv;
        sante.value = maxpv;
    }
    private void Start()
    {
        //player_vie= gameObject.FindGameObjectWithTag("Player").GetComponent<Sante>;
        //sante.value=player_vie.PV_actuels;
        //sante.maxValue=player_vie.PV_max;
    }
}
