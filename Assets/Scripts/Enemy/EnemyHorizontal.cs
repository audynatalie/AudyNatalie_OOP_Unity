using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHorizontal : Enemy
{
    public float kecepatan=2f;
    private Vector2 arahGerak;
    public GameObject prefabEnemy;
    private void Start()
    {
        // Posisikan musuh secara acak di sisi kiri atau kanan layar
        RespawnDiSisi();
        SpawnMultipleEnemies(prefabEnemy, Random.Range(3, 7));
    
    }
    private void Update()
    {
        // Gerakkan musuh secara horizontal
        transform.Translate(arahGerak * kecepatan * Time.deltaTime);
        // Cek apakah musuh keluar dari layar dan respawn jika perlu
        if (IsOutOfScreen())
        {
            RespawnDiSisi();
        }
    }
    // Cek apakah musuh keluar dari layar
    private bool IsOutOfScreen()
    {
        return transform.position.x<-Screen.width / 80f || transform.position.x>Screen.width/80f;
    }
    // Method untuk memposisikan musuh secara acak di sisi kiri atau kanan layar
    private void RespawnDiSisi()
    {
        // Tentukan sisi spawn secara acak (kiri atau kanan)
        float spawnX=Random.Range(0, 2)==0 ? -Screen.width/110f : Screen.width/120f;
        
        float spawnY=Random.Range(-Screen.height/80f, Screen.height/80f);
        // Set posisi musuh di sisi kiri atau kanan dengan posisi Y acak
        transform.position=new Vector2(spawnX,spawnY);
        // Tentukan arah pergerakan horizontal berdasarkan sisi spawn
        arahGerak=spawnX<0 ? Vector2.right:Vector2.left;
        // Pastikan rotasi tetap pada keadaan awal (menghadap arah horizontal)
        
        transform.rotation=Quaternion.identity;
    
    }
    public static void SpawnMultipleEnemies(GameObject prefabEnemy,int jumlah)
    {
        for(int i=0;i<jumlah;i++)
        {
            // Buat instance baru dari musuh dan spawn di sisi acak
            GameObject musuhBaru=Instantiate(prefabEnemy);
            EnemyHorizontal skripMusuh=musuhBaru.GetComponent<EnemyHorizontal>();
            skripMusuh?.RespawnDiSisi(); // Gunakan null-conditional operator
        }
    }
}