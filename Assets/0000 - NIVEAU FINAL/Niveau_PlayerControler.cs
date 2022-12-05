using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Niveau_PlayerControler : MonoBehaviour
{
    // Public Static
    [HideInInspector] public static bool DialogueStop = false; // Gèle les déplacements du joueur lors d'un dialogue
    [HideInInspector] public static bool StartOpening = false;

    // Public
    [HideInInspector] public bool Grounded = false;
    [HideInInspector] public bool Frozen = false;

    // Private Static
    private static readonly Vector3 s_flipRotation = new Vector3(0, 180, 0);
    private static readonly Vector3 s_cameraPosition = new Vector3(8.333332f, 1.666666f, 0f);
    private static readonly Vector3 s_inverseCameraPosition = new Vector3(-8.333332f, 1.666666f, -0f);

    // Position initiale du joueur si on relance la scène, string = nom de l'objet root
    private static Dictionary<string, Vector3> s_playerPos = new Dictionary<string, Vector3>();
    // Orientation initiale du joueur si on relance la scène, string = nom de l'objet root
    private static Dictionary<string, bool> s_playerFlip = new Dictionary<string, bool>();

    public static bool s_gameOver = false;

    // Serialized
    [Header("Shared")]
    [SerializeField] private MovementManager _movementManager;
    [SerializeField] private SkillsManager _skillsManager;
    [SerializeField] private Sante _sante;

    [Header("Dash")]
    [SerializeField] private TextMeshPro _chargementDash;
    [SerializeField] private TrailRenderer _tr;
    [SerializeField] private AudioSource _source;

    // ----------------------------------------------------------------------------

    // GameObjects
    private Rigidbody _rb;
    private Animator _anim;
    private Camera _mainCamera;
    private weaponDamage _weapon;

    // Déplacements
    private bool _flipped = false;
    private bool _isOnIce = false;
    private bool _isOnTrampoline = false;
    private bool _isOnMud = false;
    private bool _canDoubleJump = false;

    // Dash
    private int _lastOrient = 1;
    private bool _canDash = true;
    private bool _isDashing = false;

    // ----------------------------------------------------------------------------

    // Awake se produit avait le Start. Il peut être bien de régler les références dans cette section.
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _mainCamera = GetComponentInChildren<Camera>();
        _weapon = GetComponentInChildren<weaponDamage>();
    }

    // Utile pour régler des valeurs aux objets
    void Start()
    {
        _chargementDash.text = "";

        if (Niveau_PlayerControler.StartOpening)
        {
            _rb.position = s_playerPos[transform.root.name];
            if (s_playerFlip[transform.root.name])
            {
                _flipped = true;
                transform.Rotate(s_flipRotation);
                _mainCamera.transform.Rotate(-s_flipRotation);
                _mainCamera.transform.localPosition = s_inverseCameraPosition;
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Niveau_PlayerControler.StartOpening = false;
    }

    // Vérifie les entrées de commandes du joueur
    void Update()
    {

        if (Sante.PV_actuels <= 0)
        {   
            s_gameOver = true;
            _rb.velocity = new Vector3(0, 0, 0);
            _anim.SetFloat("MoveSpeed", 0f);
        }

        if (_isDashing) return;

        var newPlayerPos = _rb.position;
        newPlayerPos.y += 0.7f; // On met le joueur un peu plus haut au spawn
        s_playerPos[transform.root.name] = newPlayerPos;

        var horizontal = Input.GetAxis("Horizontal") * _movementManager.MoveSpeed;

        if (!s_gameOver && !Frozen && !Niveau_PlayerControler.DialogueStop)
        {
            HorizontalMove(horizontal);
            FlipCharacter(horizontal);
            CheckJump();

            if (Input.GetKeyDown(KeyCode.E) && _canDash)
            {
                StartCoroutine(Dash(horizontal));
            }

            if (Input.GetButtonDown("Attack"))
            {
                _weapon.damage_mode = true;
                _anim.CrossFade("Attack", 0.1f);
            }
        }
    }

    // Gère le mouvement horizontal
    void HorizontalMove(float horizontal)
    {
        if (_isOnTrampoline)
        {
            var yForce = _movementManager.TrampoForce * (Mathf.Abs(_rb.velocity.y) + 0.25f * Mathf.Abs(_rb.velocity.z));
            _rb.AddForce(new Vector3(0, yForce, 0));
            _isOnTrampoline = false; // FIXME: ???
        }
        else if (_isOnIce)
        {
            _rb.AddForce(new Vector3(0, 0, horizontal * 0.2f));
        }
        else if (_isOnMud)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, horizontal * _movementManager.MudEffect);
        }
        else
        {
            _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, horizontal);
        }
        _anim.SetFloat("MoveSpeed", Mathf.Abs(horizontal));
    }

    IEnumerator Dash(float horizontal)
    {
        _canDash = false;
        _isDashing = true;
        _rb.useGravity = false;
        _rb.velocity = new Vector3(_rb.velocity.x, 0f, _lastOrient * _movementManager.DashingPower);
        _tr.emitting = true;
        _source.PlayOneShot(_movementManager.ClipDash);
        _anim.SetBool("Dash", true);
        yield return new WaitForSeconds(_movementManager.DashingTime);
        _anim.SetBool("Dash", false);
        _tr.emitting = false;
        _rb.useGravity = true;
        _isDashing = false;

        if (Grounded)
        {
            _anim.Play("Land");
        }
        else
        {
            _anim.Play("Midair");
        }

        for (int i = 0; i < 6; i++)
        {
            _chargementDash.text = new string('I', 6 - i);
            yield return new WaitForSeconds((_movementManager.DashingCooldown / 6) * 1.0f);
        }

        _chargementDash.text = "";
        _canDash = true;
    }

    // Gère le saut du personnage, ainsi que son animation de saut
    void CheckJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (Grounded)
            {
                _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, 0);
                _rb.AddForce(new Vector3(0, _movementManager.JumpForce, 0), ForceMode.Impulse);
                Grounded = false;
                _anim.SetBool("Grounded", false);
                _anim.SetBool("Jump", true);
                _canDoubleJump = true;
            }
            else if (_canDoubleJump && _skillsManager.unlockedSkills[(int)Skill.DoubleSaut])
            {
                _rb.velocity = new Vector3(_rb.velocity.x, 0, 0);
                _rb.AddForce(new Vector3(0, _movementManager.JumpForce, 0), ForceMode.Impulse);
                _canDoubleJump = false;

            }
        }
    }

    // Gère l'orientation du joueur et les ajustements de la camera
    void FlipCharacter(float horizontal)
    {
        if (horizontal < 0 && !_flipped)
        {
            _flipped = true;
            s_playerFlip[transform.root.name] = true;
            _lastOrient = -1;
            transform.Rotate(s_flipRotation);
            _mainCamera.transform.Rotate(-s_flipRotation);
            _chargementDash.alignment = TextAlignmentOptions.Right;
            _mainCamera.transform.localPosition = s_inverseCameraPosition;
        }
        else if (horizontal > 0 && _flipped)
        {
            _lastOrient = 1;
            s_playerFlip[transform.root.name] = false;
            _flipped = false;
            transform.Rotate(-s_flipRotation);
            _mainCamera.transform.Rotate(s_flipRotation);
            _chargementDash.gameObject.transform.Rotate(s_flipRotation);
            _chargementDash.alignment = TextAlignmentOptions.Left;
            _mainCamera.transform.localPosition = s_cameraPosition;
        }
    }

    // Collision avec le sol
    void OnCollisionEnter(Collision coll)
    {
        // On s'assure de bien être en contact avec le sol
        if ((_movementManager.WhatIsGround & (1 << coll.gameObject.layer)) == 0) return;

        // Évite une collision avec le plafond
        if (coll.relativeVelocity.y > 0)
        {
            Grounded = true;
            _anim.SetBool("Grounded", Grounded);
        }

        _isOnTrampoline = (coll.gameObject.tag == "trampoline");
        _isOnMud = (coll.gameObject.tag == "mud");
        _isOnIce = (coll.gameObject.tag == "ice");

        if (_isOnIce) GetComponent<Collider>().material.dynamicFriction = 0;
    }

    void OnCollisionExit(Collision coll)
    {
        // On s'assure de bien être en contact avec le sol
        if ((_movementManager.WhatIsGround & (1 << coll.gameObject.layer)) == 0) return;

        if (coll.gameObject.tag == "trampoline") _isOnTrampoline = false;
        if (coll.gameObject.tag == "mud") _isOnMud = false;
        if (coll.gameObject.tag == "ice") _isOnIce = false;
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "frag")
        {
            SkillsManager.XP += 100;
            coll.gameObject.SetActive(false);
        }

        // TODO: bouger ça ailleurs
        if (coll.gameObject.layer == 9) _sante._isOnLava = true;
        if (coll.gameObject.layer == 10) _sante._piegeActive = true;
        if (coll.gameObject.tag == "projectile") _sante.Degats(_sante.Degats_Projectiles);
    }

    void OnTriggerExit(Collider coll)
    {
        // TODO: bouger ça ailleurs
        if (coll.gameObject.layer == 9) _sante._isOnLava = false;
    }

    public void Attack_End()
    {
        _weapon.damage_mode = false;
    }
}
