using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sky_time : MonoBehaviour
{   
    public static bool change_scene = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Waitfinal());
    }

    // Update is called once per frame
    void Update()
    {   
        if (change_scene)
        {
            change_scene = false;
            StartCoroutine(Waitfinal());
        }
    }

    IEnumerator Waitfinal()
    {   
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("4 - GROUND-cinematic 1");
    }
}
