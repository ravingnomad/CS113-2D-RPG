﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyHealth : MonoBehaviour {

    public float CurrentHealth;
    public float MaxHealth;
    public bool dead;
    public Collider2D hitBox;

    private Animator anim;
    private EnemyChasePlayer attack;
    private BoxCollider2D collider;

    private SFXManager sfx;

    // Use this for initialization
    void Start () {
        sfx = FindObjectOfType<SFXManager>();
        collider = GetComponent<BoxCollider2D>();
        CurrentHealth = MaxHealth;
        anim = GetComponent<Animator>();
        attack = GetComponent<EnemyChasePlayer>();
	}
	
	// Update is called once per frame
	void Update () {

        if (CurrentHealth <= 0)
        {
            anim.SetBool("Dead", true);            
        }
    }

    public void HurtEnemy(float damage)
    {
        CurrentHealth -= damage;
        attack.wasHit = true;
    }

    public void SetMaxHealth()
    {
        CurrentHealth = MaxHealth;
    }

    public void Death()
    {
        if (gameObject.tag == "Goblin")
        {
            GetComponent<GoblinTakeDamage>().enabled = false;
            sfx.GoblinDeath.Play();
        }

        if (gameObject.tag == "Slime")
        {
            GetComponent<SlimeTakeDamage>().enabled = false;
            sfx.SlimeDeath.Play();
        }
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;
        rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        anim.Stop();
        GetComponent<EnemyAttack>().enabled = false;
        GetComponent<EnemyMovement>().enabled = false;
        attack.enabled = false;
        hitBox.enabled = false;
        collider.enabled = false;
 

        //FindObjectOfType<DestroyManager>().AddToList(this.name);
    }

}
