using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuPause : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    private bool set=false;
    private GameObject separation;
    // Start is called before the first frame update
    
    void Start(){
        separation=GameObject.Find("HUD/Canvas/Separation");
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            set=!set;
            Menu.SetActive(set);
            separation.SetActive(!set);
            if (set==true){
                Time.timeScale=0f;
            } else{
                Time.timeScale=1f;
            }

            
        }
    }
    
    public void Resume(){
        Menu.SetActive(false);
        Time.timeScale=1f;
    } 
}
