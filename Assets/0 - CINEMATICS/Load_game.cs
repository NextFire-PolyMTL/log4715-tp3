using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_game : MonoBehaviour
{
    public static bool change_scene = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (change_scene)
        {
            change_scene = false;
            StartCoroutine(Load());
        }
    }

    IEnumerator Load()
    {   
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("1 - previllage");
    }
}
