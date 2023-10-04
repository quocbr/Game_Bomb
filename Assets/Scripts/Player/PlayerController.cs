using System;
using System.Collections;
using DefaultNamespace;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviourPunCallbacks
{
    public PlayerAnimationStrings animationStrings = new PlayerAnimationStrings();

    [SerializeField] private Vector2 moveInput = Vector2.zero;
    public float moveSpeed = 2.0f;
    private Animator _animator;
    private Rigidbody2D _rb;
    
    
    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        
        if (photonView.IsMine)
        {
            if (moveInput != Vector2.zero)
            {
                this._animator.SetFloat(animationStrings.moveX, this.moveInput.x);
                this._animator.SetFloat(animationStrings.moveY, this.moveInput.y);
                this.transform.Translate(moveInput * moveSpeed * Time.fixedDeltaTime);
            }
            UpdateAnimation();
            //photonView.RPC("UpdateAnimation",RpcTarget.AllBuffered);
        }
    }
    [PunRPC]
    private void UpdateAnimation()
    {
        if (moveInput != Vector2.zero)
        {
            _animator.SetBool(animationStrings.isRun,true);
            //_animator.Play(animationStrings.walk);
        }
        else
        {
            _animator.SetBool(animationStrings.isRun,false);
            //_animator.Play(animationStrings.idle);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
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