﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingLake : MonoBehaviour {

    public Dialogue dialogue;
    public bool in_range = false;
    //public PlayerMove player;
    public PlayerHealth health;
    public bool entered;

    private SFXManager sfx;

    void Start()
    {
        sfx = FindObjectOfType<SFXManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            health = FindObjectOfType<PlayerHealth>();
            health.CurrentHealth = health.MaxHealth;
            //player = FindObjectOfType<PlayerMove>();
            in_range = true;
            entered = true;
            sfx.healing.Play();
        }
    }
    private void Update()
    {

        if (in_range == true && entered == true)
        {
            Animator animator = FindObjectOfType<DialogueManager>().animator;
            if (animator.GetBool("IsOpen") == false)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if (Input.inputString == "e" && animator.GetBool("IsOpen") == true)
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
                if (animator.GetBool("Is Open") == false)
                {
                    entered = false;
                }
            }
            else if (Input.inputString == "q" && animator.GetBool("IsOpen") == true)
            {
                FindObjectOfType<DialogueManager>().EndDialogue();
                entered = false;
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
            in_range = false;
        }
    }
}

