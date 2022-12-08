using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class T_8_9 : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    public AudioClip ClipFin;
    [SerializeField] private GameObject Fade;
    private Animator _animFade;

    private bool first_time = true;

    void Start()
    {
        _animFade = Fade.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Toggle_fin_bas.coll_fin_bas && Toggle_fin_haut.coll_fin_haut && first_time)
        {   
            first_time = false; // On ne rentre qu'un fois dans cette boucle
            StartCoroutine(Move_to_next());
        }
    }

    IEnumerator Move_to_next()
    {
        _source.PlayOneShot(ClipFin);
        _animFade.Play("In");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("9 - bonus");
    }



}
