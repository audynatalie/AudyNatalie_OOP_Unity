using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyForward : Enemy
{
    public float kecepatanGerak=2f;
    public GameObject prefabEnemy;
    private void Start()
    {
        // Memulai musuh pada posisi acak di atas layar dan spawn musuh baru
        SpawnEnemiesAtTop(Random.Range(3,7));
   
    }
    private void Update()
    {
        // Gerakkan musuh ke bawah dengan kecepatan tertentu
        MoveEnemyDown();
        // Respawn musuh jika keluar dari layar di bagian bawah
        RespawnIfOutOfScreen();
    
    }
    private void MoveEnemyDown()
    {
        // Menggerakkan musuh ke bawah sesuai kecepatan
        transform.Translate(Vector2.down * kecepatanGerak * Time.deltaTime);
   
    }
    private void RespawnIfOutOfScreen()
    {
        // Cek apakah musuh sudah keluar dari layar dan spawn ulang jika perlu
        if (transform.position.y < -Screen.height / 85f)
        {
            RespawnAtTop();
        }
    }
    private void RespawnAtTop()
    {
        // Memposisikan musuh secara acak di bagian atas layar
        float posisiAcakX = Random.Range(-Screen.width / 105f, Screen.width / 105f);
        
        transform.position = new Vector2(posisiAcakX, Screen.height / 85f);
        // Rotasi musuh tetap pada orientasi awal
        transform.rotation = Quaternion.identity;
    }
    public void SpawnEnemiesAtTop(int jumlahMusuh)
    {
        // Menambahkan musuh baru ke layar
        for (int i=0; i<jumlahMusuh; i++)
        {
            SpawnSingleEnemy();
        }
    }
    private void SpawnSingleEnemy()
    {
        // Membuat dan menginisialisasi musuh baru
        GameObject newEnemy=Instantiate(prefabEnemy);
        EnemyForward enemyScript=newEnemy.GetComponent<EnemyForward>();
        if (enemyScript!=null)
        {
            enemyScript.RespawnAtTop(); // Tentukan posisi spawn di bagian atas layar
        }
    }
}