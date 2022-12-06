using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class T_10_11 : MonoBehaviour
{
    [SerializeField] private GameObject Fade;
    private Animator _animFade;

    private bool first_time = true;
    public static bool retourMenu=false;

    void Start()
    {
        _animFade = Fade.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (retourMenu && first_time)
        {   
            retourMenu=false;
            first_time = false; // On ne rentre qu'un fois dans cette boucle
            StartCoroutine(Move_to_next());
        }
    }

    IEnumerator Move_to_next()
    {
        _animFade.Play("In");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("11 - Village 2");
    }



}
