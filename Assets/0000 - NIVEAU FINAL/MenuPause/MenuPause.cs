using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuPause : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    private bool set=false;
    private GameObject separation;
    private GameObject checkVillage;
    [SerializeField] private AudioSource _source;
    public AudioClip ClipFin;
    // Start is called before the first frame update
    
    void Start(){
        separation=GameObject.Find("HUD/Canvas/Separation");
        checkVillage=GameObject.Find("HUD");

    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            set=!set;
            _source.PlayOneShot(ClipFin);
            Menu.SetActive(set);
            //Debug.Log(checkVillage.activeSelf);
            if (checkVillage.activeSelf==true){
                separation.SetActive(!set);
            }
            
            if (set==true){
                Time.timeScale=0f;
            } else{
                Time.timeScale=1f;
            }

            
        }
    }
    
    public void Resume(){
        _source.PlayOneShot(ClipFin);
        Menu.SetActive(false);
        Time.timeScale=1f;
    } 
}
