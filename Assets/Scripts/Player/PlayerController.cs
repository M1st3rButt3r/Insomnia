using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public int speed;
    public float raycastLength;
    public LayerMask ground;
    public float jumpForce;
    public float koyoteTime;
    public float jumpTime;
    public bool canMove = true;
    
    [HideInInspector]
    public Rigidbody2D rb;

    public static PlayerController playerController;

    public AudioClip walk;
    private bool _iswalking;
    public AudioClip jump;
    public AudioClip levitate;
    

    private bool _grounded;
    private float _lastGrounded;
    private bool _isJumping;
    private float _jumpUntil;
    private Vector2 _oldmove;

    private void Awake()
    {
        playerController = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        PlayerInput.Instance.JumpStart += () =>
        {
            if(canMove) JumpStart();
        };
        PlayerInput.Instance.JumpEnd += JumpEnd;
    }

    void FixedUpdate()
    {
        Grounded();
        if(_isJumping) Jumping();
        if(canMove) Move(PlayerInput.Instance.moveInput);
    }

    public void Move(Vector2 input)
    {
        rb.velocity = new Vector2(input.x * speed * Time.fixedDeltaTime, rb.velocity.y);
        if(_oldmove != input && !_iswalking && _grounded)
        {
            SoundManager.Instance.PlayBGM(walk, SoundManager.SFX);
            StartCoroutine("walkingsound");
            _iswalking = true;            
        }
        _oldmove = input;
        
    }
    IEnumerator walkingsound(){
        yield return new WaitForSeconds(walk.length);
        _iswalking = false;
    }
    private void Grounded()
    {
        Vector3 position = transform.position;
        Vector3 scale = transform.localScale;
        Vector2 origin1 = position - new Vector3(0, scale.y/2);
        Vector2 origin2 = position - new Vector3(-scale.x, scale.y)/2;
        Vector2 origin3 = position - scale /2;

        bool origin1Bool = Physics2D.Raycast(origin1, Vector2.down, raycastLength, ground);
        bool origin2Bool = Physics2D.Raycast(origin2, Vector2.down, raycastLength, ground);
        bool origin3Bool = Physics2D.Raycast(origin3, Vector2.down, raycastLength, ground);
        _grounded =  origin1Bool || origin2Bool || origin3Bool;
        if (_grounded) _lastGrounded = Time.time;
    }

    public void JumpStart()
    {
        if(!_grounded && _lastGrounded + koyoteTime <= Time.time) return;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce);
        _isJumping = true;
        _jumpUntil = Time.time + jumpTime;
        SoundManager.Instance.PlayBGM(jump, SoundManager.SFX);
    }

    private void Jumping()
    {
        if (_jumpUntil < Time.time)
        {
            _isJumping = false;
            return;
        }

        rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime);
    }

    public void JumpEnd()
    {
        _isJumping = false;
        
    }

    public void DeactivateInput()
    {
        canMove = false;
    }
    
    public void ActivateInput()
    {
        canMove = true;
    }
}
