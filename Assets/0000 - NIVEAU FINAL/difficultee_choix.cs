using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class difficultee_choix : MonoBehaviour
{
    [SerializeField] GameObject Rect1;

    [SerializeField] GameObject Rect2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Rect1.SetActive(true);
            Rect2.SetActive(false);
            Sante.niveau_diff = false;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Rect1.SetActive(false);
            Rect2.SetActive(true);
            Sante.niveau_diff = true;
        }
    }
}
