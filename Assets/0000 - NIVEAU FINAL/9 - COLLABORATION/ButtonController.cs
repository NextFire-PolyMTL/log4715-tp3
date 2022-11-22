using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    private GameObject _ToggledObject;

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
        if (collision.contacts[0].normal.y < 0)
        {
            _ToggledObject.SetActive(false);
            var renderer = transform.GetComponent<Renderer>();
            renderer.material.color = Color.green;
        }
    }
}
