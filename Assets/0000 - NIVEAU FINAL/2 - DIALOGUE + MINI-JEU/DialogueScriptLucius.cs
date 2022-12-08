using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueScriptLucius : MonoBehaviour
{
    [SerializeField] private Niveau_PlayerControler _PlayerControler;
    [SerializeField] private GameObject _VisualCue;

    [SerializeField]
    GameObject image_dialogue_lucius;

    [SerializeField]
    GameObject text_lucius1;

    [SerializeField]
    GameObject text_lucius2;

    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip clip_dialogue;

    [SerializeField]
    GameObject exit_door;

    private bool affiche_d1 = false;
    private bool affiche_d2 = false;
    private bool begin_dialogue = false;
    public static bool start_opening = false;

    public static bool affiche_exit_door = false;

    // Start is called before the first frame update
    void Start()
    {
        _VisualCue.SetActive(false);
        if (affiche_exit_door)
        {
            exit_door?.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (begin_dialogue && Input.GetKeyDown(KeyCode.I)) // Phase 1 dialogue : le marchand parle
        {
            source.PlayOneShot(clip_dialogue);
            Niveau_PlayerControler.DialogueStop = true; // On gèle les mouvements du joueur
            affiche_d1 = true;
            affiche_d2 = false;
            image_dialogue_lucius.SetActive(true);
            text_lucius1.SetActive(true);
            text_lucius2.SetActive(false);
            exit_door?.SetActive(true);
            affiche_exit_door = true;
        }

        else if (affiche_d1 && Input.GetKeyDown(KeyCode.Space)) // Phase 2 dialogue : le joueur répond
        {
            source.PlayOneShot(clip_dialogue);
            affiche_d1 = false;
            affiche_d2 = true;
            image_dialogue_lucius.SetActive(true);
            text_lucius1.SetActive(false);
            text_lucius2.SetActive(true);
        }

        else if (affiche_d2 && Input.GetKeyDown(KeyCode.Space))
        {
            source.PlayOneShot(clip_dialogue);
            affiche_d2 = false;
            image_dialogue_lucius.SetActive(false);
            text_lucius1.SetActive(false);
            text_lucius2.SetActive(false);
            StartCoroutine(StopTime());

        }


    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == _PlayerControler.gameObject)
        {
            _VisualCue.SetActive(true);
            begin_dialogue = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == _PlayerControler.gameObject)
        {
            _VisualCue.SetActive(false);
            begin_dialogue = false;
        }
    }

    IEnumerator StopTime()
    {
        yield return new WaitForSeconds(0.5f);
        Niveau_PlayerControler.DialogueStop = false;

    }

}
