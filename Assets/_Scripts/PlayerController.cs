﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : SteerableBehaviour, IShooter, IDamageable
{
    Animator animator;
    private int lifes;
    public AudioClip shootSFX;
    public GameObject bullet;
    public Transform arma;
    public float shootDelay = 1.0f;
    private float _lastShootTimestamp = 0.0f;


   private void Start()
   {
       lifes = 10;
       animator = GetComponent<Animator>();
   }

    public void Shoot()
    {
        if (Time.time - _lastShootTimestamp < shootDelay) return;
        AudioManager.PlaySFX(shootSFX);

       _lastShootTimestamp = Time.time;
       Instantiate(bullet, arma.position, Quaternion.identity);
        // throw new System.NotImplementedException();
    }

    public void TakeDamage()
    {
        lifes--;
        if(lifes <= 0) Die(); 
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
{
   if (collision.CompareTag("Inimigos"))
   {
       Destroy(collision.gameObject);
       TakeDamage();
   }
}

    void FixedUpdate()
    {
        float yInput = Input.GetAxis("Vertical");
        float xInput = Input.GetAxis("Horizontal");
        Thrust(xInput, yInput);if (yInput != 0 || xInput != 0)
       {
           animator.SetFloat("Velocity", 1.0f);
       }
       else
       {
           animator.SetFloat("Velocity", 0.0f);
       }
       if(Input.GetAxisRaw("Jump") != 0)
       {
           Shoot();
       }
       if(Input.GetKeyDown(KeyCode.Escape)) 
       {
           SceneManager.LoadScene("SampleScene");
       }
    }    


    
}
