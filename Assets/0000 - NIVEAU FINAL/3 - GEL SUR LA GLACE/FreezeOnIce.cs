using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Niveau_PlayerControler))]
public class FreezeOnIce : MonoBehaviour
{
    private Niveau_PlayerControler _pc;
    [SerializeField] private string _WhatIsIceTag;
    [SerializeField] private GameObject _IceBlock;
    [SerializeField] private float _DelayUntilFreeze = 1.0f;
    [SerializeField] private int _IceBreakNbActions = 5;
    private bool _onIce = false;
    private float _lastMove = 0;
    private int _nbActions = 0;

    void Awake()
    {
        _pc = GetComponent<Niveau_PlayerControler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _IceBlock.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool input = Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Jump");

        if (input) _nbActions++;

        if (input || !_pc.Grounded) _lastMove = Time.time;

        if (_onIce && ((Time.time - _lastMove) > _DelayUntilFreeze)) Freeze(true);
        else if (_pc.Frozen && (_nbActions > _IceBreakNbActions)) Freeze(false);
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == _WhatIsIceTag) _onIce = true;
    }

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag == _WhatIsIceTag) _onIce = false;
    }

    void Freeze(bool freeze)
    {
        _pc.Frozen = freeze;
        _IceBlock.SetActive(freeze);
        _nbActions = 0;
    }
}
