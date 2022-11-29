using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteControler : MonoBehaviour
{
    [SerializeField] private Animator _porte;
    [SerializeField] private Animator _prisonnier;
    [SerializeField] private SkillsManager _skillsManager;
    [SerializeField] private GameObject _successMessage;


    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Hero")
        {
            _porte.Play("Ouverture", 0, 0.0f);
            gameObject.SetActive(false);
            _prisonnier.Play("Avance", 0, 0.0f);
            _skillsManager.XP += 75;
            _successMessage.SetActive(true);

        }
        if (coll.gameObject.tag == "Prisoner")
        {
            _porte.Play("Fermeture", 0, 0.0f);
            //prisonnier.Play("idle");
            gameObject.SetActive(false);
            _successMessage.SetActive(false);
        }
    }
}
