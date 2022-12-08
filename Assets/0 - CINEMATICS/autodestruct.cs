using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autodestruct : MonoBehaviour
{   
    public ParticleSystem particle;

    public float début;
    public float duree;
    
    // Start is called before the first frame update
    void Start()
    {   
        particle.Pause();
        StartCoroutine(Autodestruct());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Autodestruct()
    {
        yield return new WaitForSeconds(début);
        particle.Play();
        yield return new WaitForSeconds(duree);
        Destroy(gameObject);
    }
}
