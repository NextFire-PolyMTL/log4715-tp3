﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerControlerTechnoFrag : MonoBehaviour
{
    // Déclaration des constantes
    private static readonly Vector3 FlipRotation = new Vector3(0, 180, 0);
    private static readonly Vector3 CameraPosition = new Vector3(8, 1, 0);
    private static readonly Vector3 InverseCameraPosition = new Vector3(-8, 1, 0);

    // Déclaration des variables
    bool _Grounded { get; set; }
    bool _Flipped { get; set; }
    Animator _Anim { get; set; }
    Rigidbody _Rb { get; set; }
    Camera _MainCamera { get; set; }

    // Valeurs exposées
    [SerializeField]
    float MoveSpeed = 5.0f;

    [SerializeField]
    float JumpForce = 5f;

    [SerializeField]
    LayerMask WhatIsGround;

   // private int xp;
    private GameObject NombreS;
    //public int Nombre_XP;

    public int xp=0;
    public Text txt;
    [SerializeField]
    GameObject image_money;

    private bool DoubleJump;
    private Bouton bouton;

    // Awake se produit avait le Start. Il peut être bien de régler les références dans cette section.
    void Awake()
    {
        _Anim = GetComponent<Animator>();
        _Rb = GetComponent<Rigidbody>();
        _MainCamera = Camera.main;
        NombreS=GameObject.Find("Arbre/Canvas/nombre");
        txt=NombreS.GetComponent<Text>();
        bouton=GameObject.Find("Arbre/Canvas/Boutons/Manager").GetComponent<Bouton>();
    }

    // Utile pour régler des valeurs aux objets
    void Start()
    {
        _Grounded = false;
        _Flipped = false;
        DoubleJump=false;
    //    xp=0;
    }

    // Vérifie les entrées de commandes du joueur
    void Update()
    {
        TextChange();

        var horizontal = Input.GetAxis("Horizontal") * MoveSpeed;
        HorizontalMove(horizontal);
        FlipCharacter(horizontal);
        CheckJump(horizontal);

    }

    // Gère le mouvement horizontal
    void HorizontalMove(float horizontal)
    {
        _Rb.velocity = new Vector3(_Rb.velocity.x, _Rb.velocity.y, horizontal);
        _Anim.SetFloat("MoveSpeed", Mathf.Abs(horizontal));
    }

    // Gère le saut du personnage, ainsi que son animation de saut
    void CheckJump(float horizontal)
    {  
        if (Input.GetButtonDown("Jump"))
            {
            if (_Grounded)
            {   
                _Rb.velocity = new Vector3(_Rb.velocity.x, 0, horizontal);
                _Rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
                _Grounded = false;
                _Anim.SetBool("Grounded", false);
                _Anim.SetBool("Jump", true);
                DoubleJump=true;
            }
            else if (DoubleJump && bouton.active_list[0]==1){   
                _Rb.velocity = new Vector3(_Rb.velocity.x, 0, horizontal);           
                _Rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
                DoubleJump=false;
            
        }  
            }   
    }

    // Gère l'orientation du joueur et les ajustements de la camera
    void FlipCharacter(float horizontal)
    {
        if (horizontal < 0 && !_Flipped)
        {
            _Flipped = true;
            transform.Rotate(FlipRotation);
            _MainCamera.transform.Rotate(-FlipRotation);
            _MainCamera.transform.localPosition = InverseCameraPosition;
        }
        else if (horizontal > 0 && _Flipped)
        {
            _Flipped = false;
            transform.Rotate(-FlipRotation);
            _MainCamera.transform.Rotate(FlipRotation);
            _MainCamera.transform.localPosition = CameraPosition;
        }
    }

    // Collision avec le sol
    void OnCollisionEnter(Collision coll)
    {        
        // On s'assure de bien être en contact avec le sol
        if ((WhatIsGround & (1 << coll.gameObject.layer)) == 0)
            return;

        // Évite une collision avec le plafond
        if (coll.relativeVelocity.y > 0)
        {
            _Grounded = true;
            _Anim.SetBool("Grounded", _Grounded);
        }
    }
    
    private void OnTriggerEnter(Collider coll){
        if (coll.gameObject.tag=="frag"){
            //NombreS.GetComponent<nombre_xp>().xp+=100;
            xp+=50;
            //xp++;
            coll.gameObject.SetActive(false);
        }
    }


    private void TextChange(){
        txt.text=xp.ToString();
    }
    private void ChangeValue(int nb){
        xp=nb;
        if (xp<0){
            xp=0;
        }
    }
    public bool EnleverXP(int nb){
        int valeur=xp-nb;
        bool marche=true;
        if (valeur<0){
            valeur=xp;  // s'il n'y a pas assez d'xp pour la compétence, elle ne s'active pas
            marche=false;
        }
        ChangeValue(valeur);
        return marche;

    }
}