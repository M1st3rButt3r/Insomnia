using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public int speed;
    public float raycastLength;
    public LayerMask ground;
    public float jumpForce;
    public float koyoteTime;
    public float jumpTime;

    private Rigidbody2D _rb;
    private bool _grounded;
    private float _lastGrounded;
    private bool _isJumping;
    private float _jumpUntil;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        PlayerInput.Instance.JumpStart += JumpStart;
        PlayerInput.Instance.JumpEnd += JumpEnd;
    }

    void FixedUpdate()
    {
        Grounded();
        Move(PlayerInput.Instance.moveInput);
        if(_isJumping) Jumping();
    }

    private void Move(Vector2 input)
    {
        _rb.velocity = new Vector2(input.x * speed * Time.fixedDeltaTime, _rb.velocity.y);
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

    private void JumpStart()
    {
        if(!_grounded && _lastGrounded + koyoteTime <= Time.time) return;
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(Vector2.up * jumpForce);
        _isJumping = true;
        _jumpUntil = Time.time + jumpTime;
    }

    private void Jumping()
    {
        if (_jumpUntil < Time.time)
        {
            _isJumping = false;
            return;
        }

        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce * Time.fixedDeltaTime);
    }

    private void JumpEnd()
    {
        _isJumping = false;
    }
}
