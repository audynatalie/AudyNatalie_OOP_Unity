using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Portal : MonoBehaviour
{
    [SerializeField] private float speed = 5f; 
    [SerializeField] private float rotateSpeed = 50f; 
    private Vector3 newPosition; 

    // Menentukan posisi acak untuk portal saat pertama kali dimulai
    private void Start()
    {
        ChangePosition();
    }

    // Mengatur pergerakan portal menuju posisi baru dan rotasi portal
    private void Update()
    {
        // Memindahkan portal menuju posisi baru dengan kecepatan tertentu
        transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        // Jika portal sudah dekat dengan posisi baru, ubah posisi ke titik acak lainnya
        if (Vector3.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }

        // Memutar portal di sekitar sumbu Z
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

        // Mengaktifkan atau menonaktifkan portal berdasarkan apakah pemain memiliki senjata
        if (IsPlayerArmed())
        {
            SetPortalActive(true);
        }
        else
        {
            SetPortalActive(false);
        }
    }

    // Mengatur interaksi saat bertabrakan dengan pemain
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Jika objek yang bertabrakan memiliki tag "Player"
        if (other.CompareTag("Player"))
        {
            // Mengaktifkan semua objek UI dalam GameManager saat pemain masuk portal
            foreach (Transform child in GameManager.Instance.transform)
            {
                if (child.GetComponent<Canvas>() != null || child.GetComponent<UnityEngine.UI.Image>() != null)
                {
                    child.gameObject.SetActive(true);
                }
            }
            // Memuat scene "Main" melalui LevelManager
            FindObjectOfType<LevelManager>().LoadScene("Main");
        }
    }

    // Mengatur posisi baru untuk portal secara acak
    private void ChangePosition()
    {
        float posX = Random.Range(-10f, 10f);
        float posY = Random.Range(-10f, 10f);

        newPosition = new Vector3(posX, posY, 0f);
    }

    // Memeriksa apakah pemain memiliki senjata atau tidak
    private bool IsPlayerArmed()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            return player.GetComponentInChildren<Weapon>() != null;
        }
        return false;
    }

    // Mengaktifkan atau menonaktifkan komponen portal (sprite dan collider)
    private void SetPortalActive(bool enabled)
    {
        GetComponent<SpriteRenderer>().enabled = enabled;
        GetComponent<Collider2D>().enabled = enabled;
    }
}
