using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Fields untuk movement settings
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
        // Mengambil komponen Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Menghitung nilai awal untuk moveVelocity, moveFriction, dan stopFriction
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);
    }

    public void Move()
    {
        // Mengambil input dari pemain
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveDirection = input.normalized;

        // Logika pergerakan berdasarkan input
        if (input.magnitude > 0)
        {
            moveVelocity += moveDirection * maxSpeed * Time.fixedDeltaTime;
            moveVelocity = Vector2.ClampMagnitude(moveVelocity, maxSpeed.magnitude);
        }
        else
        {
            moveVelocity = Vector2.MoveTowards(moveVelocity, Vector2.zero, stopFriction.magnitude * Time.fixedDeltaTime);
        }

        // Mengatur kecepatan Rigidbody2D berdasarkan moveVelocity
        rb.velocity = moveVelocity;
    }

    private Vector2 GetFriction()
    {
        // Mengembalikan gaya gesek yang sesuai berdasarkan kondisi pergerakan
        return rb.velocity.magnitude > 0 ? moveFriction : stopFriction;
    }

    // Method untuk mengecek apakah Player sedang bergerak
    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0.1f;
    }
}
