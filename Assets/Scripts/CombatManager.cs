using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners; // Array spawner musuh
    public float timer=0; // Timer untuk jeda antar-gelombang
    [SerializeField] private float waveInterval=5f; // Interval waktu antar gelombang
    public int waveNumber =1; // Nomor gelombang saat ini
    public int totalEnemies = 0; // Total musuh yang telah di-spawn

    private void Start()
    {
        MulaiGelombang(); // Memulai gelombang pertama
    }

    private void Update()
    {
        if (SemuaSpawnerIdle())
        {
            timer += Time.deltaTime;

            // Memeriksa apakah sudah waktunya untuk memulai gelombang berikutnya
            if (timer >= waveInterval)
            {
                LanjutKeGelombangBerikutnya();
                timer = 0;
            }
        }
    }

    private void MulaiGelombang()
    {
        foreach (var spawner in enemySpawners)
        {
            AturSpawnerUntukGelombang(spawner, waveNumber);
        }
    }

    private void LanjutKeGelombangBerikutnya()
    {
        waveNumber++;
        totalEnemies = 0;

        foreach (var spawner in enemySpawners)
        {
            AturSpawnerUntukGelombang(spawner, waveNumber);
        }
    }

    private void AturSpawnerUntukGelombang(EnemySpawner spawner, int gelombangSaatIni)
    {
        if (spawner != null)
        {
            spawner.defaultSpawnCount = gelombangSaatIni; // Menentukan jumlah spawn berdasarkan gelombang
            spawner.multiplierIncreaseCount = gelombangSaatIni; // Meningkatkan kesulitan
            spawner.isSpawning = true;
        }
    }

    private bool SemuaSpawnerIdle()
    {
        foreach (var spawner in enemySpawners)
        {
            if (spawner != null && spawner.isSpawning)
            {
                return false; // Jika salah satu spawner masih aktif
            }
        }
        return true; // Semua spawner sudah tidak aktif
    }

    public void CatatKillMusuh()
    {
        totalEnemies++; // Menambahkan jumlah musuh yang telah dikalahkan
    }
}
