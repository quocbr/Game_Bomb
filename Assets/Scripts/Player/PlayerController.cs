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
    [SerializeField]private float moveSpeed = 2.0f;
    [SerializeField] private Vector2 direction;
    private Animator _animator;
    private Rigidbody2D _rb;
    private PlayerInputAction _playerInputAction;

    public float bombFuseTime = 4.5f;
    public int bombAmount = 1;

    private bool _animLoker = false;
    
    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    protected override void Awake()
    {
        _playerInputAction = new PlayerInputAction();
    }

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

        if (_playerInputAction.Player.Fire.triggered && BulletSpawner.Instance.SpawnedCount < bombAmount)
        {
            StartCoroutine(PlaceBomb());
            /*var position = this.transform.position;
            this.direction.x = (int) position.x + (position.x>0.0?0.5f:-0.5f);
            this.direction.y = (int) (position.y + (position.y>0.0?0.5f:-0.5f));

            Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.bulletOne,this.direction, transform.rotation);
            newBullet.gameObject.SetActive(true);*/
        }
    }

    private IEnumerator PlaceBomb()
    {
        Vector2 position = this.transform.position;
        position.x = (int) position.x + (position.x>0.0?0.5f:-0.5f);
        position.y = (int) (position.y + (position.y>0.0?0.5f:-0.5f));
        
        Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.bulletOne,position, Quaternion.identity);
        newBullet.gameObject.SetActive(true);

        yield return new WaitForSeconds(bombFuseTime);
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

    protected override void OnEnable()
    {
        _playerInputAction.Enable();
    }

    protected override void OnDisable()
    {
        _playerInputAction.Disable();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }
}
