using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbre : MonoBehaviour
{
    [SerializeField] private GameObject _HUD;
    [SerializeField] private GameObject _Arbre;
    private bool allume = false;

    // Start is called before the first frame update
    void Start()
    {
        _Arbre.SetActive(allume);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            allume = !allume;
            _Arbre.SetActive(allume);
            _HUD.SetActive(!allume);
        }
    }
}
