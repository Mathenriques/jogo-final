using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private bool _grounded;
    
    [SerializeField]
    private float speed;

    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int Grounded = Animator.StringToHash("grounded");
    private static readonly int Jump1 = Animator.StringToHash("jump");

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _rigidbody2D.velocity = new Vector2(horizontalInput * speed, _rigidbody2D.velocity.y);

        // Flip
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        } else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        // Jump
        if (Input.GetKey(KeyCode.Space) && _grounded)
        {
            Jump();
        }
        
        // Walk Animation
        _animator.SetBool(Run, horizontalInput != 0);
        _animator.SetBool(Grounded, _grounded);
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, speed);
        _animator.SetTrigger(Jump1);
        _grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Ground"))
        {
            _grounded = true;
        }
    }
}
