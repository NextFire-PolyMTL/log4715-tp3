using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Skill
{
    DoubleSaut,
    Skill1,
    Skill2,
    Skill3,
    Skill4,
    Skill5,
    Skill6,
    Skill7,
    Skill8,
    Skill9,
}

public class SkillsManager : MonoBehaviour
{
    public static int XP = 0;
    public bool[] unlockedSkills = new bool[Enum.GetNames(typeof(Skill)).Length];

    [SerializeField] private Text _txt;

    [SerializeField] private TMP_Text _txt2;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _txt.text = XP.ToString();
        _txt2.text = XP.ToString();
    }


    public bool EnleverXP(int nb)
    {
        if (XP < nb) return false;
        XP -= nb;
        return true;
    }
}
