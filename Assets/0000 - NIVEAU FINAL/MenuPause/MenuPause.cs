using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuPause : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    private bool set=false;
    // Start is called before the first frame update
    
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            set=!set;
            Menu.SetActive(set);
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
