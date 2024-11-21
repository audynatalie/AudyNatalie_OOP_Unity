using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    public float kecepatan = 2f; // Kecepatan gerakan musuh
    private Vector2 arahGerak; // Arah gerakan musuh
    private SpriteRenderer renderSprite; // Komponen SpriteRenderer untuk mengatur flip gambar

    private void Start()
    {
        // Inisialisasi SpriteRenderer
        renderSprite = GetComponent<SpriteRenderer>();
        if (renderSprite == null)
        {
            Debug.LogError("SpriteRenderer tidak ditemukan! Pastikan komponen SpriteRenderer ada pada GameObject.");
            return; // Hentikan eksekusi jika SpriteRenderer tidak ditemukan
        }

        // Posisikan musuh secara acak di sisi kiri atau kanan layar
        RespawnDiSisi();
    }

    private void Update()
    {
        // Gerakkan musuh secara horizontal
        GerakkanMusuh();

        // Jika musuh keluar dari layar di bagian kiri atau kanan, respawn di sisi berlawanan
        if (IsOutOfScreen())
        {
            RespawnDiSisi();
        }
    }

    // Fungsi untuk menggerakkan musuh
    private void GerakkanMusuh()
    {
        transform.Translate(arahGerak * kecepatan * Time.deltaTime);
    }

    // Fungsi untuk mengecek apakah musuh keluar dari layar
    private bool IsOutOfScreen()
    {
        return transform.position.x < -Screen.width / 80f || transform.position.x > Screen.width / 80f;
    }

    // Method untuk memposisikan musuh secara acak di sisi kiri atau kanan layar
    private void RespawnDiSisi()
    {
        // Pastikan renderSprite sudah diinisialisasi
        if (renderSprite == null)
        {
            Debug.LogError("SpriteRenderer tidak diinisialisasi! Tidak dapat melanjutkan RespawnDiSisi.");
            return;
        }

        // Tentukan sisi spawn secara acak (kiri atau kanan)
        float spawnX = Random.Range(0, 2) == 0 ? Screen.width / 120f : -Screen.width / 110f;

        float spawnY = Random.Range(-Screen.height / 80f, Screen.height / 80f);

        // Set posisi musuh di sisi kiri atau kanan dengan posisi Y acak
        transform.position = new Vector2(spawnX, spawnY);

        // Tentukan arah pergerakan horizontal berdasarkan sisi spawn
        arahGerak = spawnX < 0 ? Vector2.right : Vector2.left;

        // Membalikkan gambar sesuai arah gerakan
        renderSprite.flipX = arahGerak == Vector2.left;

        // Pastikan rotasi tetap pada keadaan awal (menghadap arah horizontal)
        transform.rotation = Quaternion.identity;
    }
}
