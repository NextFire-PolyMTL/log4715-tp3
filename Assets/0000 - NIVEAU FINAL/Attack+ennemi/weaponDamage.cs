using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponDamage : MonoBehaviour
{
    public bool damage_mode = false;
    public bool gotuer;
    // Start is called before the first frame update
    void Start()
    {
        gotuer = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(SkillsManager.unlockedSkills[(int)Skill.CAC]);
        gotuer = SkillsManager.unlockedSkills[(int)Skill.CAC];
        // Debug.Log("coucou");
    }

    void OnCollisionEnter(Collision vision)
    {
        // Debug.Log("enemy:");
        // Debug.Log(vision.gameObject.tag);
        if (vision.gameObject.tag == "Enemy")
        {
            // Debug.Log("touché!! ah ah ah");
            GameObject.Destroy(vision.gameObject);
        }

    }
    void OnTriggerEnter(Collider vision)
    {
        if (vision.gameObject.tag == "Enemy" && damage_mode && gotuer)
        {
            // Debug.Log("touché!! ah ah ah");
            vision.gameObject.GetComponent<Animator>().CrossFade("Die", 0.1f);
            GameObject.Destroy(vision.gameObject, 1.10f);
        }

    }
}
