using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Sante))]
public class P2Niveau_PlayerControler : MonoBehaviour
{
    // 0 - CADRICIEL INITIAL
    [Header("Objets génériques")]
    // Déclaration des constantes
    private static readonly Vector3 FlipRotation = new Vector3(0, 180, 0);
    private static readonly Vector3 CameraPosition = new Vector3(8.333332f, 1.666666f, -0.7666666f);
    private static readonly Vector3 InverseCameraPosition = new Vector3(-8.333332f, 1.666666f, 0.7666666f);

    private weaponDamage _Weapon;
    // Déclaration des variables
    public bool Grounded { get; set; }
    bool _Flipped { get; set; }
    bool _isOnIce { get; set; }
    bool _isOnTrampoline { get; set; }
    bool _isOnMud { get; set; }
    Animator _Anim { get; set; }
    Rigidbody _Rb { get; set; }
    Camera _MainCamera { get; set; }

    // Valeurs exposées
    [SerializeField]
    float MoveSpeed = 5.0f;

    [SerializeField]
    float JumpForce = 7.0f;

    [SerializeField]
    LayerMask WhatIsGround;

    // 4 - DIFFERENT SOL -----------------------------------------------
    [SerializeField]
    float trampoForce = 10f;

    [SerializeField]
    float mudEffect = 0.5f;

    // 1 - DASH -----------------------------------------------
    [Header("Dash")]
    int last_orient = 1;
    bool canDash = true;
    bool isDashing;


    [SerializeField]
    float dashingPower = 18f;

    [SerializeField]
    float dashingTime = 0.2f;

    [SerializeField]
    float dashingCooldown = 1f;

    [SerializeField]
    TrailRenderer tr;

    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip clip_dash;

    [SerializeField]
    AudioClip clip_saut;

    // 2 - DIALOGUE + JEU DE DE --------------------------------------------------------------
    [Header("Dialogue et jeu de hasard")]
    public bool Frozen = false; // Freeze le joueur si égal à true
    public static bool Stop = false; // Gèle les déplacements du joueur lors d'un dialogue
    private Vector3 PlayerPos; // Position initiale du joueur si on relance la scène
    private bool PlayerFlip = false;
    static public bool StartOpening = false;

    public GameObject Weapon;
    public float life = 1000;

    // Barre de Vie
    public bool GameOver = false;
    private Sante _sante;

    // Arbre de compétences
    [SerializeField] private Text _Txt;
    public int xp = 0;

    private bool DoubleJump;
    [SerializeField] private Bouton _Bouton;

    [SerializeField] private TextMeshPro Chargement_Dash;




    // Awake se produit avait le Start. Il peut être bien de régler les références dans cette section.
    void Awake()
    {
        _Anim = GetComponent<Animator>();
        _Rb = GetComponent<Rigidbody>();
        _MainCamera = GetComponentInChildren<Camera>();
        _sante = GetComponent<Sante>();
        _Weapon = Weapon.GetComponent<weaponDamage>();
    }

    // Utile pour régler des valeurs aux objets
    void Start()
    {   
        Chargement_Dash.text = "";
        Grounded = false;
        _Flipped = false;
        if (StartOpening)
        {
            StartOpening = false;
            _Rb.position = PlayerPos;
            if (PlayerFlip)
            {
                _Flipped = true;
                transform.Rotate(FlipRotation);
                _MainCamera.transform.Rotate(-FlipRotation);
                _MainCamera.transform.localPosition = InverseCameraPosition;
            }
        }
        DoubleJump = false;
    }

    // Vérifie les entrées de commandes du joueur
    void Update()
    {
        TextChange();
        if (_sante.PV_actuels <= 0)
        {
            GameOver = true;
        }
        if (isDashing)
        {
            return;
        }

        PlayerPos = _Rb.position;
        PlayerPos.y = PlayerPos.y + 0.7f; // On met le joueur un peu plus haut au spawn

        var horizontal = Input.GetAxis("Horizontal") * MoveSpeed;

        if (!GameOver && !Frozen && !Stop)
        { // METTEZ VOS FONCTIONS DANS LE IF : le booléen freeze = true retire le contrôle du personnage
            HorizontalMove(horizontal);
            FlipCharacter(horizontal);
            CheckJump();
            if (Input.GetKeyDown(KeyCode.E) && canDash)
            {
                StartCoroutine(Dash(horizontal));
            }
            if (Input.GetButtonDown("Attack"))
            {
                //_Anim.SetBool("Attack",true);
                _Weapon.damage_mode = true;
                _Anim.CrossFade("Attack", 0.1f);
            }


        }
    }

    // Gère le mouvement horizontal
    void HorizontalMove(float horizontal)
    {
        if (_isOnTrampoline)
        {
            //_Rb.AddForce(new Vector3(0, trampoForce*Mathf.Abs(_Rb.velocity.y),0));
            _Rb.AddForce(new Vector3(0, trampoForce * (Mathf.Abs(_Rb.velocity.y) + 0.25f * Mathf.Abs(_Rb.velocity.z)), 0));
            //_Rb.AddForce(new Vector3(0, trampoForce*(Mathf.Abs(_Rb.velocity.y)),trampoForce*Mathf.Abs(_Rb.velocity.z)));
            _isOnTrampoline = false;
            Debug.Log(trampoForce * Mathf.Abs(_Rb.velocity.z));
        }

        if (_isOnIce)
        {
            _Rb.AddForce(new Vector3(0, 0, horizontal * 0.2f));
        }
        else if (_isOnMud)
        {
            _Rb.velocity = new Vector3(_Rb.velocity.x, _Rb.velocity.y, horizontal * mudEffect);
        }
        else
        {
            _Rb.velocity = new Vector3(_Rb.velocity.x, _Rb.velocity.y, horizontal);
        }
        _Anim.SetFloat("MoveSpeed", Mathf.Abs(horizontal));
    }

    IEnumerator Dash(float horizontal)
    {
        canDash = false;
        isDashing = true;
        _Rb.useGravity = false;
        _Rb.velocity = new Vector3(_Rb.velocity.x, 0f, last_orient * dashingPower);
        tr.emitting = true;
        source.PlayOneShot(clip_dash);
        _Anim.SetBool("Dash", true);
        yield return new WaitForSeconds(dashingTime);
        _Anim.SetBool("Dash", false);
        tr.emitting = false;
        _Rb.useGravity = true;
        isDashing = false;
        if(Grounded)
        {
            _Anim.Play("Land");
        }
        else{
            _Anim.Play("Midair");
        }
        for (int i = 0; i < 6; i++)
        {   
            Chargement_Dash.text = new string('I', 6 - i);
            yield return new WaitForSeconds((dashingCooldown/6) * 1.0f);
        }
        Chargement_Dash.text = "";
        canDash = true;

    }

    // Gère le saut du personnage, ainsi que son animation de saut
    void CheckJump()
    {
        //if (_Grounded)
        //{
        //    if (Input.GetButtonDown("Jump"))
        //    {
        //        source.PlayOneShot(clip_saut);
        //        _Rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
        //        _Grounded = false;
        //        _Anim.SetBool("Grounded", false);
        //        _Anim.SetBool("Jump", true);
        //        DoubleJump=true;
        //    }
        //}
        if (Input.GetButtonDown("Jump"))
        {
            if (Grounded)
            {
                _Rb.velocity = new Vector3(_Rb.velocity.x, _Rb.velocity.y, 0);
                _Rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
                Grounded = false;
                _Anim.SetBool("Grounded", false);
                _Anim.SetBool("Jump", true);
                DoubleJump = true;
            }
            else if (DoubleJump && _Bouton.active_list[0] == 1)
            {
                _Rb.velocity = new Vector3(_Rb.velocity.x, 0, 0);
                _Rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
                DoubleJump = false;

            }
        }
    }

    // Gère l'orientation du joueur et les ajustements de la camera
    void FlipCharacter(float horizontal)
    {
        if (horizontal < 0 && !_Flipped)
        {
            _Flipped = true;
            PlayerFlip = true;
            last_orient = -1;
            transform.Rotate(FlipRotation);
            _MainCamera.transform.Rotate(-FlipRotation);
            Chargement_Dash.alignment = TextAlignmentOptions.Right;
            _MainCamera.transform.localPosition = InverseCameraPosition;
        }
        else if (horizontal > 0 && _Flipped)
        {
            last_orient = 1;
            PlayerFlip = false;
            _Flipped = false;
            transform.Rotate(-FlipRotation);
            _MainCamera.transform.Rotate(FlipRotation);
            Chargement_Dash.gameObject.transform.Rotate(FlipRotation);
            Chargement_Dash.alignment = TextAlignmentOptions.Left;
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
            Grounded = true;
            _Anim.SetBool("Grounded", Grounded);
        }
        if (coll.gameObject.tag == "trampoline")
        {
            _isOnTrampoline = true;
        }
        else
        {
            _isOnTrampoline = false;
        }

        if (coll.gameObject.tag == "mud")
        {
            _isOnMud = true;
        }
        else
        {
            _isOnMud = false;
        }
        if (coll.gameObject.tag == "ice")
        {
            _isOnIce = true;
            Debug.Log("ice floor");
            GetComponent<Collider>().material.dynamicFriction = 0;
            Debug.Log(GetComponent<Collider>().material);
        }
        else
        {
            _isOnIce = false;
        }

    }
    
    void OnCollisionExit(Collision coll)
    {
        // On s'assure de bien être en contact avec le sol
        if ((WhatIsGround & (1 << coll.gameObject.layer)) == 0)
            return;


        if (coll.gameObject.tag == "mud")
        {
            _isOnMud = false;
        }
        
        if (coll.gameObject.tag == "ice")
        {
            _isOnIce = false;
        }

    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "frag")
        {
            //NombreS.GetComponent<nombre_xp>().xp+=100;
            xp += 100;

            coll.gameObject.SetActive(false);
        }
    }
    private void TextChange()
    {
        _Txt.text = xp.ToString();
    }
    void Attack_End()
    {
        _Weapon.damage_mode = false;
    }
    private void ChangeValue(int nb)
    {
        xp = nb;
        if (xp < 0)
        {
            xp = 0;
        }
    }
    public bool EnleverXP(int nb)
    {
        int valeur = xp - nb;
        bool marche = true;
        if (valeur < 0)
        {
            valeur = xp;  // s'il n'y a pas assez d'xp pour la compétence, elle ne s'active pas
            marche = false;
        }
        ChangeValue(valeur);
        return marche;

    }
}
