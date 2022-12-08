using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bouton : MonoBehaviour
{
    [SerializeField] private SkillsManager _skillManager;
    private List<Button> bouton_list;
    private List<GameObject> fleche_list;
    private List<GameObject> couts;
    private int[] couts_list;
    [SerializeField] bool niv2 = false;

    // FIXME: Hacky fix
    private bool _free = false;

    // Start is called before the first frame update
    void Awake()
    {
        bouton_list = new List<Button>();
        bouton_list.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp1").GetComponent<Button>());
        bouton_list.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp2").GetComponent<Button>());
        bouton_list.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp3").GetComponent<Button>());
        bouton_list.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp10").GetComponent<Button>());
        bouton_list.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp4").GetComponent<Button>());
        bouton_list.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp6").GetComponent<Button>());
        bouton_list.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp5").GetComponent<Button>());
        bouton_list.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp8").GetComponent<Button>());
        bouton_list.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp7").GetComponent<Button>());
        bouton_list.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp9").GetComponent<Button>());

        fleche_list = new List<GameObject>();
        fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche1/noir1"));
        fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche1/noir2"));
        fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche1/noir3"));

        fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche2/noir1"));
        fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche2/noir2"));

        fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche3/noir1"));
        fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche3/noir2"));

        fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche4/noir1"));

        fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche5/noir1"));
        fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche5/noir2"));

        fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche6/noir1"));
        fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche6/noir2"));

        fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche7/noir1"));

        fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche8/noir1"));

        fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche9/noir1"));

        couts = new List<GameObject>();
        couts.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp1/niv1"));
        couts.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp2/niv2"));
        couts.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp3/niv3"));
        couts.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp10/niv4"));
        couts.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp4/niv5"));
        couts.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp6/niv6"));
        couts.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp5/niv7"));
        couts.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp8/niv8"));
        couts.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp7/niv9"));

        couts.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp9/niv10"));

        couts_list = new int[] { 100, 250, 250, 300, 340, 340, 400, 470, 470, 620 };
    }

    void Start()
    {
        for (int i = 1; i < 10; i++)
        {
            bouton_list[i].interactable = false;
            ColorBlock couleur = bouton_list[i].colors;
            couleur.disabledColor = Color.black;
            bouton_list[i].colors = couleur;
        }

        // FIXME: Hacky fix
        Action[] funcs = {
            DebloqueSkill1,
            DebloqueSkill2,
            DebloqueSkill3,
            DebloqueSkill4,
            DebloqueSkill5,
            DebloqueSkill6,
            DebloqueSkill7,
            DebloqueSkill8,
            DebloqueSkill9,
            DebloqueSkill10,
        };
        _free = true;
        for (int i = 0; i < SkillsManager.unlockedSkills.Length; i++)
        {
            if (SkillsManager.unlockedSkills[i]) funcs[i]();
        }
        _free = false;
    }

    // Update is called once per frame
    void Update()
    {
        int nb = SkillsManager.XP;
        for (int i = 0; i < 10; i++)
        {
            if (bouton_list[i].interactable)
            {
                if (nb - couts_list[i] > 0)
                {
                    couts[i].GetComponent<TMPro.TextMeshProUGUI>().color = Color.green;
                }
                else
                {
                    couts[i].GetComponent<TMPro.TextMeshProUGUI>().color = Color.red;
                }
                // BoutonsActifs.Add(bouton_list[i]);
            }
        }
    }

    public void DebloqueSkill1()
    {

        //bool c1=NombreS.GetComponent<nombre_xp>().EnleverXP(130);
        bool c1 = _free || _skillManager.EnleverXP(100);
        if (c1)
        {
            bouton_list[1].interactable = true;
            ColorBlock couleur = bouton_list[1].colors;
            couleur.disabledColor = Color.white;
            bouton_list[1].colors = couleur;

            bouton_list[2].interactable = true;
            ColorBlock couleur2 = bouton_list[2].colors;
            couleur2.disabledColor = Color.white;
            bouton_list[2].colors = couleur2;

            bouton_list[0].interactable = false;
            SkillsManager.unlockedSkills[0] = true;
            ColorBlock couleur3 = bouton_list[0].colors;
            couleur3.disabledColor = Color.green;
            bouton_list[0].colors = couleur3;

            fleche_list[0].SetActive(false);
            fleche_list[1].SetActive(false);
            fleche_list[2].SetActive(false);
            couts[0].SetActive(false);
            // coûts.RemoveAt(0);
        }


    }
    public void DebloqueSkill2()
    {
        //bool c2=NombreS.GetComponent<nombre_xp>().EnleverXP(200);
        bool c2 = _free || _skillManager.EnleverXP(250);
        if (c2)
        {
            bouton_list[1].interactable = false;
            SkillsManager.unlockedSkills[1] = true;

            ColorBlock couleur = bouton_list[1].colors;
            couleur.disabledColor = Color.green;
            bouton_list[1].colors = couleur;


            //   bouton_list[1].disabledColor=Color.green;
            if (SkillsManager.unlockedSkills[2] && niv2)
            {
                bouton_list[3].interactable = true;
            }
            if (niv2)
            {
                bouton_list[4].interactable = true;
                fleche_list[3].SetActive(false);
                fleche_list[4].SetActive(false);
            }

            couts[1].SetActive(false);
            //coûts.RemoveAt(1);
        }

    }

    public void DebloqueSkill3()
    {
        //bool c3=NombreS.GetComponent<nombre_xp>().EnleverXP(200);
        bool c3 = _free || _skillManager.EnleverXP(250);
        if (c3)
        {
            bouton_list[2].interactable = false;
            SkillsManager.unlockedSkills[2] = true;


            ColorBlock couleur = bouton_list[2].colors;
            couleur.disabledColor = Color.green;
            bouton_list[2].colors = couleur;
            //   bouton_list[2].disabledColor=Color.green;
            if (SkillsManager.unlockedSkills[1] && niv2)
            {
                bouton_list[3].interactable = true;
            }

            if (niv2)
            {
                bouton_list[5].interactable = true;
                fleche_list[5].SetActive(false);
                fleche_list[6].SetActive(false);
            }

            couts[2].SetActive(false);
            //  coûts.RemoveAt(2);
        }



    }
    public void DebloqueSkill4()
    {
        //bool c4=NombreS.GetComponent<nombre_xp>().EnleverXP(250);
        bool c4 = _free || _skillManager.EnleverXP(300);
        if (c4)
        {
            bouton_list[3].interactable = false;
            SkillsManager.unlockedSkills[3] = true;


            ColorBlock couleur = bouton_list[3].colors;
            couleur.disabledColor = Color.green;
            bouton_list[3].colors = couleur;
            //     bouton_list[3].disabledColor=Color.green;
            if (SkillsManager.unlockedSkills[4] && SkillsManager.unlockedSkills[5])
            {
                bouton_list[6].interactable = true;
            }
            fleche_list[7].SetActive(false);
            couts[3].SetActive(false);
            //    coûts.RemoveAt(3);
        }

    }
    public void DebloqueSkill5()
    {
        //bool c5=NombreS.GetComponent<nombre_xp>().EnleverXP(340);
        //Debug.Log(c5);
        bool c5 = _free || _skillManager.EnleverXP(340);

        ColorBlock couleur = bouton_list[4].colors;
        if (c5)
        {
            bouton_list[4].interactable = false;
            SkillsManager.unlockedSkills[4] = true;
            couleur.disabledColor = Color.green;
            bouton_list[4].colors = couleur;
            //     bouton_list[4].disabledColor=Color.green;
            if (SkillsManager.unlockedSkills[3] && SkillsManager.unlockedSkills[5])
            {
                bouton_list[6].interactable = true;
            }
            bouton_list[7].interactable = true;
            fleche_list[8].SetActive(false);
            fleche_list[9].SetActive(false);
            couts[4].SetActive(false);
            //   coûts.RemoveAt(4);
        }

        //if (c5==false){

        // }
    }
    public void DebloqueSkill6()
    {
        //bool c6=NombreS.GetComponent<nombre_xp>().EnleverXP(340);
        bool c6 = _free || _skillManager.EnleverXP(340);
        if (c6)
        {
            bouton_list[5].interactable = false;
            SkillsManager.unlockedSkills[5] = true;

            ColorBlock couleur = bouton_list[5].colors;
            couleur.disabledColor = Color.green;
            bouton_list[5].colors = couleur;
            //     bouton_list[5].disabledColor=Color.green;
            if (SkillsManager.unlockedSkills[3] && SkillsManager.unlockedSkills[4])
            {
                bouton_list[6].interactable = true;
            }
            bouton_list[8].interactable = true;
            fleche_list[10].SetActive(false);
            fleche_list[11].SetActive(false);
            couts[5].SetActive(false);
            //   coûts.RemoveAt(5);
        }



    }
    public void DebloqueSkill7()
    {
        //bool c7=NombreS.GetComponent<nombre_xp>().EnleverXP(400);
        bool c7 = _free || _skillManager.EnleverXP(400);
        if (c7)
        {
            SkillsManager.unlockedSkills[6] = true;
            bouton_list[6].interactable = false;

            ColorBlock couleur = bouton_list[6].colors;
            couleur.disabledColor = Color.green;
            bouton_list[6].colors = couleur;
            //     bouton_list[6].disabledColor=Color.green;
            if (SkillsManager.unlockedSkills[7] && SkillsManager.unlockedSkills[8])
            {
                bouton_list[9].interactable = true;
            }
            fleche_list[13].SetActive(false);
            couts[6].SetActive(false);
            //   coûts.RemoveAt(6);
        }


    }
    public void DebloqueSkill8()
    {
        //bool c8=NombreS.GetComponent<nombre_xp>().EnleverXP(470);
        bool c8 = _free || _skillManager.EnleverXP(470);
        if (c8)
        {
            SkillsManager.unlockedSkills[7] = true;
            bouton_list[7].interactable = false;

            ColorBlock couleur = bouton_list[7].colors;
            couleur.disabledColor = Color.green;
            bouton_list[7].colors = couleur;
            //   bouton_list[7].disabledColor=Color.green;
            if (SkillsManager.unlockedSkills[6] && SkillsManager.unlockedSkills[8])
            {
                bouton_list[9].interactable = true;
            }
            fleche_list[12].SetActive(false);
            couts[7].SetActive(false);
            //  coûts.RemoveAt(7);
        }


    }
    public void DebloqueSkill9()
    {
        //bool c9=NombreS.GetComponent<nombre_xp>().EnleverXP(470);
        bool c9 = _free || _skillManager.EnleverXP(470);
        if (c9)
        {
            SkillsManager.unlockedSkills[8] = true;
            bouton_list[8].interactable = false;

            ColorBlock couleur = bouton_list[8].colors;
            couleur.disabledColor = Color.green;
            bouton_list[8].colors = couleur;
            //   bouton_list[8].disabledColor=Color.green;
            if (SkillsManager.unlockedSkills[7] && SkillsManager.unlockedSkills[6])
            {
                bouton_list[9].interactable = true;
            }
            fleche_list[14].SetActive(false);
            couts[8].SetActive(false);
            //  coûts.RemoveAt(8);
        }


    }
    public void DebloqueSkill10()
    {
        //bool c10=NombreS.GetComponent<nombre_xp>().EnleverXP(620);
        bool c10 = _free || _skillManager.EnleverXP(620);
        if (c10)
        {
            SkillsManager.unlockedSkills[9] = true;
            bouton_list[9].interactable = false;

            ColorBlock couleur = bouton_list[9].colors;
            couleur.disabledColor = Color.green;
            bouton_list[9].colors = couleur;
            //    bouton_list[9].disabledColor=Color.green;
            couts[9].SetActive(false);
            //   coûts.RemoveAt(9);
        }

    }
}
