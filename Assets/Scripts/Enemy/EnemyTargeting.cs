using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyTargeting : Enemy
{
    private Transform transformPemain; // Posisi Pemain
    private float kecepatan=2.0f;    // Kecepatan gerakan musuh
    public GameObject prefabEnemy;     // Prefab EnemyTargeting
    private void Start()
    {
        // Temukan Pemain dalam scene
        GameObject pemain=GameObject.FindGameObjectWithTag("Player");
        // Pastikan Pemain ditemukan sebelum menetapkan transform-nya
        transformPemain=pemain?.transform;
        if(transformPemain==null)
        {
            Debug.LogWarning("Pemain tidak ditemukan dalam scene. EnemyTargeting tidak akan bergerak.");
        
        }
        // Spawn beberapa musuh secara acak
        SpawnMultipleEnemies(prefabEnemy, Random.Range(1, 6));
    }
    private void Update()
    {
        // Jika Pemain ditemukan, bergerak ke arahnya
        if (transformPemain!=null)
        {
            // Hitung arah gerakan menuju Pemain
            Vector2 arah=(transformPemain.position-transform.position).normalized;
            transform.Translate(arah*kecepatan*Time.deltaTime);
       
        }
    
    }
    private void OnTriggerEnter2D(Collider2D tabrakan)
    {
        // Jika musuh bersentuhan dengan Pemain, maka musuh akan hilang
        if (tabrakan.CompareTag("Player"))
        {
            
            Destroy(gameObject); // Menghancurkan musuh saat menyentuh Pemain
        }
    }
    // Method untuk spawn beberapa musuh secara acak
    public void SpawnMultipleEnemies(GameObject prefabEnemy, int jumlah)
    {
        for(int i=0; i<jumlah;i++)
        {
            // Tentukan posisi spawn secara acak di sisi kiri atau kanan layar
            float spawnX=Random.Range(0, 2) == 0 ? -Screen.width / 110f : Screen.width / 110f;
           
            float spawnY=Random.Range(-Screen.height / 80f, Screen.height / 80f);
            // Buat instance baru dari prefab EnemyTargeting
            GameObject musuhBaru=Instantiate(prefabEnemy, new Vector2(spawnX, spawnY), Quaternion.identity);
           
            EnemyTargeting skripMusuh=musuhBaru.GetComponent<EnemyTargeting>();
            
            skripMusuh?.SetPlayerTransform(transformPemain); // Set target pemain dengan null-conditional operator
       
        }
    }
    // Menggunakan setter untuk set player transform
    private void SetPlayerTransform(Transform pemain)
    {
        transformPemain = pemain;
    
    }
}