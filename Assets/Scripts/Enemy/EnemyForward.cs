using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForward : Enemy
{
    public float kecepatanGerak = 2f; // Kecepatan gerakan musuh
    public GameObject prefabEnemy; // Prefab musuh yang akan di-spawn

    private void Start()
    {
        // Pastikan prefabEnemy sudah diassign di Inspector
        if (prefabEnemy == null)
        {
            Debug.LogError("Prefab musuh belum diassign di Inspector!"); // Peringatan jika prefabEnemy null
            return; // Hentikan eksekusi jika prefabEnemy belum diatur
        }

        // Memulai musuh pada posisi acak di atas layar dan spawn musuh baru
        SpawnEnemiesAtTop(Random.Range(3, 7));
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
        if (prefabEnemy == null)
        {
            Debug.LogError("Prefab musuh belum diassign di Inspector!"); // Peringatan jika prefabEnemy null
            return; // Hentikan eksekusi jika prefabEnemy belum diatur
        }

        for (int i = 0; i < jumlahMusuh; i++)
        {
            SpawnSingleEnemy(); // Spawn satu musuh baru
        }
    }

    private void SpawnSingleEnemy()
    {
        // Membuat dan menginisialisasi musuh baru
        if (prefabEnemy == null)
        {
            Debug.LogError("Prefab musuh belum diassign di Inspector!"); // Peringatan jika prefabEnemy null
            return; // Hentikan eksekusi jika prefabEnemy null
        }

        GameObject newEnemy = Instantiate(prefabEnemy); // Instansiasi prefab musuh
        EnemyForward enemyScript = newEnemy.GetComponent<EnemyForward>();

        if (enemyScript != null)
        {
            enemyScript.RespawnAtTop(); // Tentukan posisi spawn di bagian atas layar
        }
    }
}
