using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eboul_script : MonoBehaviour
{
    public GameObject player;
    public GameObject _eboul;
    private Vector3 eboul_pos;

    private Rigidbody rb;

    private float time;

    private bool start_fall;
    [SerializeField] float cadence=1.5f;
    [SerializeField] float entree=1f;

    // Start is called before the first frame update
    void Start()
    {
        //_eboul.SetActive(false);
        eboul_pos = _eboul.transform.position;
        start_fall = true;
        rb = _eboul.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(start_fall)
        {
            start_fall = false;
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        // Debug.Log("Fall");
        _eboul.SetActive(true);
        yield return new WaitForSeconds(entree);
        _eboul.SetActive(false);
        _eboul.transform.position = eboul_pos;
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(cadence);
        start_fall = true;
    }
}
