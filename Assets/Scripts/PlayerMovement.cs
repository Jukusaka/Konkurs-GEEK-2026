using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;

    [SerializeField] private Animator animator;

    float horizontal;
    float vertical;

    public float runSpeed = 20.0f;
    [SerializeField] private GameObject _uiDeath;

    [SerializeField] private List<GameObject> _enemyList;
    [SerializeField] private GameObject _textbox;

    private int _artifactCouter = -1;
    [SerializeField] private int _artifactsToWin = 0;
    [SerializeField] private TextMeshProUGUI _artifactsUI;

    [SerializeField] private AudioSource _footstep;

    [SerializeField] private GameObject _winUI;

    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        UpdateArtifactCounter();
    }

    void Update ()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical"); 
        HandleWalkAnimation();
    }

    private void FixedUpdate()
    {  
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    public void Die()
    {
        _uiDeath.SetActive(true);
        _textbox.SetActive(false);
        foreach (GameObject enemy in _enemyList)
        {
            enemy.SetActive(false);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Die();
            //Destroy(gameObject);
        }
    }

    private void HandleWalkAnimation()
    {
        animator.SetBool("isWalkingLeft", false);
        animator.SetBool("isWalkingRight", false);
        animator.SetBool("isWalkingUp", false);
        animator.SetBool("isWalkingDown", false);

        if (vertical < 0)
        {
            animator.SetBool("isWalkingDown", true);
            _footstep.mute = false;
        }
        else if (vertical > 0)
        {
            animator.SetBool("isWalkingUp", true);
            _footstep.mute = false;
        }
        else if (horizontal < 0)
        {
            animator.SetBool("isWalkingLeft", true);
            _footstep.mute = false;
        }
        else if (horizontal > 0)
        {
            animator.SetBool("isWalkingRight", true);
            _footstep.mute = false;
        }
        else
        {
            _footstep.mute = true;
        }
    }

    public void UpdateArtifactCounter()
    {
        _artifactCouter++;
        if (_artifactCouter == _artifactsToWin)
        {
            Win();
        }

        _artifactsUI.text = $"{_artifactCouter}/{_artifactsToWin} artefaktów";
    }

    private void Win()
    {
        _winUI.SetActive(true);
    }
    
    public void JebaćDisa()
    {
        
    }
}
