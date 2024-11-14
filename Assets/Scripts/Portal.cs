using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Portal : MonoBehaviour
{
    [SerializeField] private float speed = 5f; 
    [SerializeField] private float rotateSpeed = 50f; 
    private Vector3 newPosition; 

    // Mengatur posisi portal (acak) saat start
    private void Start()
    {
        ChangePosition();
    
    }

    // Mengatur pergerakan portal dan rotasi portal
    private void Update()
    {
        MovePortal();
        RotatePortal();
        if (PemainPunyaSenjata())
        {
            AktifkanPortal(true);
        }
        else
        {
            AktifkanPortal(false);
        }
    }

    // Mengatur tabrakan dengan player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateCanvasItems();
            FindObjectOfType<LevelManager>().LoadScene("Main");
        }
    }

    private void ChangePosition()
    {
        newPosition=new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f);
    }

    private void MovePortal()
    {
        // Menggerakkan portal ke posisi baru jika belum mencapai
        if(Vector3.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }

        transform.position=Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
    }

    private void RotatePortal()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }

    private void ActivateCanvasItems()
    {
        foreach(Transform child in GameManager.Instance.transform)
        {
            if(child.GetComponent<Canvas>()!=null||child.GetComponent<UnityEngine.UI.Image>()!=null)
            {
                child.gameObject.SetActive(true);

            }

        }

    }

    private bool PemainPunyaSenjata()
    {
        
        GameObject player = GameObject.FindWithTag("Player");
        
        return player != null && player.GetComponentInChildren<Weapon>() != null;
    }

    private void AktifkanPortal(bool enabled)
    {
        GetComponent<SpriteRenderer>().enabled = enabled;
        
        GetComponent<Collider2D>().enabled = enabled;

    }

}
