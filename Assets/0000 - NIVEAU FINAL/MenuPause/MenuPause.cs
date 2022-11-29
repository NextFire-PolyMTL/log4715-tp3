using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuPause : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    // Start is called before the first frame update
    
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            Menu.SetActive(true);
            Time.timeScale=0f;
        }
    }
    
    public void Resume(){
        Menu.SetActive(false);
        Time.timeScale=1f;
    } 
}
