using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : QuocBehaviour
{
    private static PlayerController instance;
    public static PlayerController Instance => instance;
    public PlayerAnimationStrings animationStrings = new PlayerAnimationStrings();

    [SerializeField]private Vector2 moveInput = Vector2.zero;
    public float moveSpeed = 2.0f;
    private Animator _animator;
    private Rigidbody2D _rb;
    
    private bool _animLoker = false;

    protected override void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!this._animLoker && this.moveInput != Vector2.zero)
        {
            
            this._animator.SetFloat(animationStrings.moveX,this.moveInput.x);
            this._animator.SetFloat(animationStrings.moveY,this.moveInput.y);
        }
        if (moveInput != Vector2.zero)
        {
            this.transform.Translate(moveInput * moveSpeed * Time.fixedDeltaTime);
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (moveInput != Vector2.zero)
        {
            _animator.Play(animationStrings.walk);
        }
        else
        {
            _animator.Play(animationStrings.idle);
        }
    }

    void OnMove(InputValue value)
    {
        this.moveInput = value.Get<Vector2>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion")) {
            DeathSequence();
        }
    }

    private void DeathSequence()
    {
        enabled = false;
        GetComponent<BoomController>().enabled = false;

        _animator.Play(animationStrings.death);

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }

    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().CheckWinState();
    }
    
    
}
