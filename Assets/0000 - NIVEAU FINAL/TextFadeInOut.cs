using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextFadeInOut : MonoBehaviour
{
    private TMP_Text _text;

    void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FadeIn()
    {
        _text.alpha = 0;
        while (_text.alpha < 1)
        {
            _text.alpha += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        while (_text.alpha > 0)
        {
            _text.alpha -= Time.deltaTime;
            yield return null;
        }
    }
}
