using UnityEngine;

public class Player : MonoBehaviour
{
    // Singleton Instance
    public static Player Instance;

    // Reference untuk PlayerMovement dan Animator
    private PlayerMovement playerMovement;
    private Animator animator;

    // Awake: Implementasi Singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Mengambil komponen PlayerMovement dan Animator dari EngineEffect
        playerMovement = GetComponent<PlayerMovement>();
        animator = GameObject.Find("EngineEffect").GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Memanggil method Move pada PlayerMovement
        playerMovement.Move();
    }

    private void LateUpdate()
    {
        // Mengatur parameter IsMoving pada Animator sesuai kondisi PlayerMovement
        animator.SetBool("IsMoving", playerMovement.IsMoving());
    }
}
