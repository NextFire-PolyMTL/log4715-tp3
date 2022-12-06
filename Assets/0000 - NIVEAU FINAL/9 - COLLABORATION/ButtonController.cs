using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    private GameObject _ToggledObject;
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
            active=!active;
            _ToggledObject.SetActive(active);
            var renderer = transform.GetComponent<Renderer>();
            if (active==false){
                renderer.material.color = Color.green;
            }else{
                renderer.material.color = Color.red;
            }
            
        }
    }
}
