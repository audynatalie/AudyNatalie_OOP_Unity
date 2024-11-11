using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed;        // Kecepatan maksimum yang dapat dicapai oleh pemain
    [SerializeField] private Vector2 timeToFullSpeed; // Waktu yang diperlukan untuk mencapai kecepatan penuh
    [SerializeField] private Vector2 timeToStop;      // Waktu yang diperlukan untuk berhenti sepenuhnya
    [SerializeField] private Vector2 stopClamp;       // Kecepatan minimum sebelum pemain berhenti

    private Vector2 moveDirection;   // Arah pergerakan
    private Vector2 moveVelocity;    // Kecepatan yang diterapkan ke Rigidbody2D
    private Vector2 moveFriction;    // Gesekan yang diterapkan saat bergerak
    private Vector2 stopFriction;    // Gesekan yang diterapkan saat berhenti
    private Rigidbody2D rb;          // Referensi ke Rigidbody2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Menghitung nilai terkait pergerakan berdasarkan pengaturan input
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);
    }

    private void FixedUpdate()
    {
        Move(); // Memanggil metode untuk menggerakkan pemain
        ConstrainMovementWithinBounds(); // Membatasi pergerakan pemain agar tetap dalam batas layar
    }

    public void Move()
    {
        // Menghitung arah pergerakan berdasarkan input
        float sumbuHorizontal = Input.GetAxis("Horizontal");
        float sumbuVertical = Input.GetAxis("Vertical");
        
        moveDirection = new Vector2(sumbuHorizontal, sumbuVertical).normalized;

        Vector2 friction = GetFriction();
        Vector2 velocity = moveDirection * maxSpeed;
        
        velocity -= friction * Time.fixedDeltaTime;

        // Memperbarui kecepatan Rigidbody berdasarkan nilai yang dihitung dan batas kecepatan maksimum
        rb.velocity = new Vector2(
            Mathf.Clamp(velocity.x, -maxSpeed.x, maxSpeed.x),
            Mathf.Clamp(velocity.y, -maxSpeed.y, maxSpeed.y)
        );

        // Menghentikan pemain jika kecepatannya di bawah nilai stopClamp
        if (Mathf.Abs(rb.velocity.x) < stopClamp.x && Mathf.Abs(rb.velocity.y) < stopClamp.y)
        {
            rb.velocity = Vector2.zero;
        }
    }

    Vector2 GetFriction()
    {
        // Mengembalikan nilai gesekan berdasarkan kondisi pergerakan
        return moveDirection != Vector2.zero ? moveFriction : stopFriction;
    }

    void ConstrainMovementWithinBounds()
    {   
        float boundaryMargin = 0.5f; // Mengatur margin agar pemain tidak menyentuh tepi layar
        float viewportHeight = Camera.main.orthographicSize;
        float viewportWidth = viewportHeight * Camera.main.aspect;

        // Menghitung posisi pemain dengan margin batas
        Vector3 playerPositionNow = transform.position;
        
        playerPositionNow.x = Mathf.Clamp(playerPositionNow.x, -viewportWidth + boundaryMargin, viewportWidth - boundaryMargin);
        playerPositionNow.y = Mathf.Clamp(playerPositionNow.y, -viewportHeight + boundaryMargin, viewportHeight - boundaryMargin);

        // Menerapkan posisi yang sudah dibatasi kembali ke pemain
        transform.position = playerPositionNow;
    }

    public bool IsMoving()
    {
        // Memeriksa apakah pemain sedang bergerak dengan mengevaluasi kecepatan
        return rb.velocity.magnitude > 0.1f;
    }
}
