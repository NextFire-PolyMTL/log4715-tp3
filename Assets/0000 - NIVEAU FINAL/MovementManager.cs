using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [Header("Marche")]
    public LayerMask WhatIsGround;
    public float MoveSpeed = 5.0f;
    public float MudEffect = 0.5f;

    [Header("Saut")]
    public float JumpForce = 7.0f;
    public float TrampoForce = 10f;

    [Header("Dash")]
    public float DashingPower = 18f;
    public float DashingTime = 0.2f;
    public float DashingCooldown = 1f;

    [Header("Visuels et sons")]
    public AudioClip ClipDash;
    public AudioClip ClipSaut;
}
