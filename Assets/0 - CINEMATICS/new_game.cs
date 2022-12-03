using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class new_game : MonoBehaviour
{   
    public TMP_Text Lost;
    public TMP_Text Shadows;
    public TMP_Text Info;
    public GameObject Shuttle;
    public Camera Cam;
    // Start is called before the first frame update

    Animator L_anim;
    Animator S_anim;
    Animator I_anim;
    Animator Shuttle_anim;
    Animator Cam_anim;
    void Awake()
    {
        L_anim = Lost.GetComponent<Animator>();
        S_anim = Shadows.GetComponent<Animator>();
        I_anim = Info.GetComponent<Animator>();
        Shuttle_anim = Shuttle.GetComponent<Animator>();
        Cam_anim = Cam.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            L_anim.SetBool("debut", true);
            S_anim.SetBool("debut", true);
            Destroy(Info);
            Shuttle_anim.SetBool("debut", true);
            Cam_anim.SetBool("debut", true);
            StartCoroutine(Waitfinal());
        }
        
    }
    IEnumerator Waitfinal()
    {
        yield return new WaitForSeconds(9f);
        SceneManager.LoadScene("2 - SPACE cinematic");
    }
}
