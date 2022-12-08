using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueScript2 : MonoBehaviour
{
    [SerializeField] private Niveau_PlayerControler _PlayerControler;
    [SerializeField] private GameObject _VisualCue;

    [SerializeField]
    GameObject image_dialogue_marchand;

    [SerializeField]
    GameObject image_dialogue_heros;

    [SerializeField]
    GameObject text_marchand;

    [SerializeField]
    GameObject text_marchand2;

    [SerializeField]
    GameObject text_heros;

    [SerializeField]
    RectTransform fader;

    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip clip_dialogue;

    private bool affiche_d_m = false;
    private bool affiche_d_h = false;
    private bool begin_dialogue = false;
    public static bool start_opening = false;

    // Start is called before the first frame update
    void Start()
    {
        if (start_opening)
        {
            start_opening = false;
            LeanTween.scale(fader, new Vector3(10, 10, 10), 0);
            fader.gameObject.SetActive(true);
            LeanTween.scale(fader, Vector3.zero, 1.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
            {
                fader.gameObject.SetActive(false);
            });
        }
        else
        {
            fader.gameObject.SetActive(false);
        }
        _VisualCue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (begin_dialogue && Input.GetKeyDown(KeyCode.I)) // Phase 1 dialogue : le marchand parle
        {
            source.PlayOneShot(clip_dialogue);
            Niveau_PlayerControler.DialogueStop = true; // On gèle les mouvements du joueur
            affiche_d_m = true;
            affiche_d_h = false;
            image_dialogue_marchand.SetActive(true);
            text_marchand.SetActive(true);
            text_marchand2.SetActive(true);
            image_dialogue_heros.SetActive(false);
            text_heros.SetActive(false);
        }

        else if (affiche_d_m && Input.GetKeyDown(KeyCode.Space)) // Phase 2 dialogue : le joueur répond
        {
            source.PlayOneShot(clip_dialogue);
            affiche_d_m = false;
            affiche_d_h = true;
            image_dialogue_heros.SetActive(true);
            text_heros.SetActive(true);
            image_dialogue_marchand.SetActive(false);
            text_marchand.SetActive(false);
            text_marchand2.SetActive(false);
        }

        else if (affiche_d_h && Input.GetKeyDown(KeyCode.Space))
        {
            source.PlayOneShot(clip_dialogue);
            affiche_d_h = false;
            image_dialogue_heros.SetActive(false);
            text_heros.SetActive(false);
            image_dialogue_marchand.SetActive(false);
            text_marchand.SetActive(false);
            text_marchand2.SetActive(false);
            StartCoroutine(StopTime());

        }

        else if (affiche_d_h && Input.GetKeyDown(KeyCode.Y))
        {
            source.PlayOneShot(clip_dialogue);
            affiche_d_h = false;
            image_dialogue_heros.SetActive(false);
            text_heros.SetActive(false);
            image_dialogue_marchand.SetActive(false);
            text_marchand.SetActive(false);
            text_marchand2.SetActive(false);
            StartCoroutine(StopTime());
            CloseScene();
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

    void CloseScene()
    {
        LeanTween.scale(fader, Vector3.zero, 0f);
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, new Vector3(10, 10, 10), 0.7f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LoadGame", 1f);
        });

    }

    private void LoadGame()
    {
        DiceScript.start_opening = true;
        DiceScript.niveau_final = true;
        SceneManager.LoadScene("Dice-game");
    }

    IEnumerator StopTime()
    {
        yield return new WaitForSeconds(0.5f);
        Niveau_PlayerControler.DialogueStop = false;

    }
}
