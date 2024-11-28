using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy; // Prefab musuh yang akan di-spawn

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3; // Jumlah minimum kill untuk meningkatkan jumlah spawn
    public int totalKill = 0; // Total musuh yang telah dikalahkan
    private int totalKillWave = 0; // Total kill dalam satu gelombang

    [SerializeField] private float spawnInterval = 3f; // Interval waktu antar spawn musuh

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0; // Jumlah musuh yang akan di-spawn dalam gelombang saat ini
    public int defaultSpawnCount = 1; // Jumlah spawn default
    public int spawnCountMultiplier = 1; // Faktor pengali untuk jumlah spawn
    public int multiplierIncreaseCount = 1; // Jumlah peningkatan pengali spawn

    public CombatManager combatManager; // Referensi ke CombatManager

    public bool isSpawning = false; // Status apakah spawner sedang aktif atau tidak

    private void Start()
    {
        BeginSpawning(); // Memulai proses spawn saat game dimulai
    }

    private void BeginSpawning()
    {
        if (spawnInterval > 0)
        {
            StartCoroutine(HandleSpawning()); // Memulai coroutine untuk menangani spawn musuh
        }
        else
        {
            Debug.LogWarning("Spawn interval terlalu kecil, mengatur ke nilai default."); // Peringatan jika spawnInterval tidak valid
            spawnInterval = 3f; // Mengatur spawnInterval ke nilai default
            StartCoroutine(HandleSpawning());
        }
    }

    private IEnumerator HandleSpawning()
    {
        while (isSpawning)
        {
            SpawnMultipleEnemies(); // Spawn beberapa musuh sekaligus

            yield return new WaitForSeconds(spawnInterval); // Menunggu sesuai interval sebelum spawn berikutnya

            AdjustSpawnParameters(); // Menyesuaikan parameter spawn berdasarkan kondisi game
        }
    }

    private void SpawnMultipleEnemies()
    {
        // Menentukan jumlah musuh yang akan di-spawn
        int enemiesToSpawn = spawnCount > 0 ? spawnCount : defaultSpawnCount;
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            InstantiateEnemy(); // Spawn musuh satu per satu
        }

        totalKillWave = 0; // Reset jumlah kill dalam gelombang
        WaitForWaveCompletion(); // Menunggu sampai gelombang selesai
    }

    private void WaitForWaveCompletion()
    {
        while (totalKillWave < spawnCount)
        {
            // Menunggu hingga semua musuh dalam gelombang dikalahkan
        }
    }

    private void AdjustSpawnParameters()
    {
        // Jika jumlah kill mencapai batas untuk meningkatkan jumlah spawn
        if (totalKill >= minimumKillsToIncreaseSpawnCount * multiplierIncreaseCount)
        {
            spawnCountMultiplier++; // Meningkatkan pengali jumlah spawn
            spawnCount = defaultSpawnCount * spawnCountMultiplier; // Menyesuaikan jumlah spawn
            multiplierIncreaseCount++; // Meningkatkan jumlah multiplier
        }
    }

    private void InstantiateEnemy()
    {
        if (spawnedEnemy != null)
        {
            // Membuat instance musuh baru
            Enemy newEnemy = Instantiate(spawnedEnemy, transform.position, Quaternion.identity);
            // StartCoroutine(newEnemy.ActivateAfterDelay(0f)); // Mengaktifkan musuh setelah jeda
        }
        else
        {
            Debug.LogError("Prefab musuh belum diassign!"); // Pesan error jika prefab musuh tidak diatur
        }
    }

    public void StartSpawning()
    {
        isSpawning = true; // Memulai proses spawn
    }

    public void StopSpawning()
    {
        isSpawning = false; // Menghentikan proses spawn
    }
}
