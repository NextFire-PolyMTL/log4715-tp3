using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbre : MonoBehaviour
{
    [SerializeField] private GameObject _HUD;
    [SerializeField] private GameObject _Arbre;
    private bool allume = false;
    [SerializeField] SkillsManager SkillsManagerh;
    private bool set_arbre;

    // Start is called before the first frame update
    void Start()
    {
        _Arbre.SetActive(allume);
        
    }


    // Update is called once per frame
    void Update()
    {
        set_arbre=SkillsManagerh.set_arbre;
        if (Input.GetKeyDown(KeyCode.M) && set_arbre==true)
        {
            allume = !allume;
            _Arbre.SetActive(allume);
            _HUD.SetActive(!allume);
            if (allume){
                Time.timeScale=0f;
            }else{
                Time.timeScale=1f;
            }
        }
    }
}
