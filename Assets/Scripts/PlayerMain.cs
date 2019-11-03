﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine ;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMain : MonoBehaviour
{
    private CharacterController2D _controller;
    private float _horizontal;
    private bool _crouch = false;
    private bool _jump = false;
    private Transform player;
    [SerializeField] private bool active = true;
    [SerializeField] private float _walkSpeed = 40f;
    public TransitionMain transition;


    void OnHorizontal(InputValue value)
    {
        _horizontal = value.Get<float>();
    }

    void OnJump()
    {
        if (active)
        {
            _jump = true;
        }   
    }
    void OnChange()
    {
        active = !active;
    }
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController2D>();
        player = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10 || transform.position.y > 7)
            Restart();
    }

    void FixedUpdate()
    {
        if (active)
        {
            // Move our character
            _controller.Move(_horizontal * Time.fixedDeltaTime * _walkSpeed, _crouch, _jump);
            _jump = false;
        }
    }

    public static void Restart()
    {
        GameObject.Find("Transition").GetComponent<TransitionMain>().Restart();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            player.transform.parent = collision.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            player.transform.parent = null;
        }

    }
}
