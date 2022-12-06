using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class T_4_5 : MonoBehaviour
{
    [SerializeField] private GameObject Fade;
    public static bool coll_fin_haut = false;
    public static bool coll_fin_bas = false;
    private Animator _animFade;

    private bool first_time = true;

    void Start()
    {
        _animFade = Fade.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("bas : " + coll_fin_bas + ", haut : " + coll_fin_haut);
        if (coll_fin_bas && coll_fin_haut && first_time)
        {   
            first_time = false; // On ne rentre qu'un fois dans cette boucle
            StartCoroutine(Move_to_next());
        }
    }

    IEnumerator Move_to_next()
    {
        _animFade.Play("In");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("5 - Village Hub");
    }



}
