using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DonDestroyObject : MonoBehaviour
{
    public bool _isExitActive;
    private Transform pos;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        /*
        if(_isExitActive!=null){
            this.gameObject.SetActive(_isExitActive);
        }
        */
        pos=this.gameObject.GetComponent<Transform>();
    }
    void Start()
    {
        if(SceneManager.GetActiveScene().name=="Dice-game")
        {
            //this.gameObject.SetActive(false);
            Debug.Log("desact");
            this.gameObject.GetComponent<Transform>().position=new Vector3(pos.position.x,pos.position.y-100,pos.position.z);
        } else if(SceneManager.GetActiveScene().name=="5 - Village Hub" && _isExitActive!=null){
            this.gameObject.GetComponent<Transform>().position=new Vector3(pos.position.x,pos.position.y,pos.position.z);
            //this.gameObject.SetActive(_isExitActive);
        }
    }
     
     void OnGUI()
    {
        //Debug.Log(_isExitActive);
        /*
        if(SceneManager.GetActiveScene().name=="Dice-game")
        {
            //this.gameObject.SetActive(false);
            Debug.Log("desact");
            this.gameObject.GetComponent<Transform>().position=new Vector3(pos.position.x,pos.position.y-100,pos.position.z);
        } else if(SceneManager.GetActiveScene().name=="5 - Village Hub" && _isExitActive!=null){
            this.gameObject.GetComponent<Transform>().position=new Vector3(pos.position.x,pos.position.y,pos.position.z);
            //this.gameObject.SetActive(_isExitActive);
        }*/
    }
}
