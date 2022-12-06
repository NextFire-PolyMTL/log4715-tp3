using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sante))]
public class LifeRefill : MonoBehaviour
{
    private Sante _sante;

    void Awake()
    {
        _sante = GetComponent<Sante>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(refill());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator refill()
    {
        var toRefill = Sante.PV_max - Sante.PV_actuels;
        for (int i = 0; i < toRefill; i++)
        {
            _sante.Degats(-1);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
