using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponDamage : MonoBehaviour
{
    public bool damage_mode=false;
    private SkillsManager _skillsManager;
    public bool gotuer;
    // Start is called before the first frame update
    void Start()
    {
        _skillsManager=GameObject.Find("Shared").GetComponent<SkillsManager>();
        gotuer=false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_skillsManager.unlockedSkills[(int)Skill.CAC]);
        gotuer=_skillsManager.unlockedSkills[(int)Skill.CAC];
       // Debug.Log("coucou");
    }

    void OnCollisionEnter(Collision vision){
        Debug.Log("enemy:");
        Debug.Log(vision.gameObject.tag);
        if(vision.gameObject.tag=="Enemy"){
            Debug.Log("touché!! ah ah ah");
            GameObject.Destroy(vision.gameObject);
        }
        
    }
    void OnTriggerEnter(Collider vision){
        if(vision.gameObject.tag=="Enemy" && damage_mode && gotuer){
            Debug.Log("touché!! ah ah ah");
            vision.gameObject.GetComponent<Animator>().CrossFade("Die",0.1f);
            GameObject.Destroy(vision.gameObject,1.10f);
        }
        
    }
}
