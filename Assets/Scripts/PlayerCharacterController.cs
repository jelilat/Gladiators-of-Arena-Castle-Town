using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    public GameObject charA;
    public GameObject charB;

    private GameObject activeChar;
    private Rigidbody2D rb;
    private Animator animator;

    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isGrounded = true; // You might need a way to check this

    private void Start()
    {
        // Set the default active character
        SetActiveCharacter(charA);
    }

    private void Update()
    {
        // Switch active character (example: press 'C' to switch)
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetActiveCharacter(activeChar == charA ? charB : charA);
        }

        // rest
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("AttackUp");
            animator.SetTrigger("AttackDown");
        }

        // Apply actions to the active character
        Move();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            AttackUp();
        }
        // else
        // {
        //     animator.SetBool("AttackUp", false);
        // }

        if (Input.GetKeyDown(KeyCode.X))
        {
            AttackDown();
        }
        // else
        // {
        //     animator.SetBool("AttackDown", false);
        // }
    }

    private void SetActiveCharacter(GameObject character)
    {
        activeChar = character;
        rb = activeChar.GetComponent<Rigidbody2D>();
        animator = activeChar.GetComponent<Animator>();
    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput != 0)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }


    private void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        animator.SetTrigger("jump");
        isGrounded = false; // You will need to reset this to true when character lands
    }

    private void AttackUp()
    {
        animator.SetTrigger("AttackUp");
    }

    private void AttackDown()
    {
        animator.SetTrigger("AttackDown");
    }
}
