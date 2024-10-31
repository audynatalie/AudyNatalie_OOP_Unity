using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Vector2 maxSpeed;
    [SerializeField] private Vector2 timeToFullSpeed;
    [SerializeField] private Vector2 timeToStop;
    [SerializeField] private Vector2 stopClamp;

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;

    private Rigidbody2D rb;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = new Vector2(moveVelocity.x / timeToStop.x, moveVelocity.y / timeToStop.y);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveDirection = input.normalized;

        Debug.Log("Input: " + input);

        if (input.magnitude > 0)
        {

            Vector2 targetVelocity = moveDirection * maxSpeed;
            rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, moveVelocity.magnitude * Time.fixedDeltaTime);
        }
        else
        {

            rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, stopFriction.magnitude * Time.fixedDeltaTime);

            if (rb.velocity.magnitude < stopClamp.magnitude)
            {
                rb.velocity = Vector2.zero;
            }
        }

        rb.velocity = new Vector2(
            Mathf.Clamp(rb.velocity.x, -maxSpeed.x, maxSpeed.x),
            Mathf.Clamp(rb.velocity.y, -maxSpeed.y, maxSpeed.y)
        );

        Debug.Log("Velocity: " + rb.velocity);
    }

    private Vector2 GetFriction()
    {

        if (rb.velocity.magnitude > stopClamp.magnitude)
        {
            return moveFriction * Time.fixedDeltaTime;
        }
        else
        {
            return stopFriction * Time.fixedDeltaTime;
        }
    }

    private void MoveBound()
    {

    }

    public bool IsMoving()
    {

        return rb.velocity.magnitude > 0.1f;
    }
}
