using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController2 : MonoBehaviour
{
    [SerializeField]
    private GameObject _ToggledObject;
    [SerializeField]
    private GameObject _ToggledObject2;

    [SerializeField] private AudioSource _source;
    public AudioClip ClipButton;
    private bool active=true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is on top of the button
        if (collision.contacts[0].normal.y < 0 || collision.contacts[0].normal.z!=0)
        {
            _source.PlayOneShot(ClipButton);
            active=!active;
            _ToggledObject.SetActive(active);
            _ToggledObject2.SetActive(active);
            var renderer = transform.GetComponent<Renderer>();
            if (active==false){
                renderer.material.color = Color.green;
            }else{
                renderer.material.color = Color.red;
            }
            
        }
    }
}
