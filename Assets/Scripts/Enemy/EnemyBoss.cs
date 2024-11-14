using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    public float kecepatan=2f;
    private Vector2 arahGerak;
    private SpriteRenderer renderSprite;

    private void Start()
    {
        // Posisikan musuh secara acak di sisi kiri atau kanan layar
        RespawnDiSisi();
        
        renderSprite = GetComponent<SpriteRenderer>();
    
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
        transform.Translate(arahGerak*kecepatan*Time.deltaTime);
    }

    // Fungsi untuk mengecek apakah musuh keluar dari layar
    private bool IsOutOfScreen()
    {
        return transform.position.x<-Screen.width/80f || transform.position.x>Screen.width/80f;
    }

    // Method untuk memposisikan musuh secara acak di sisi kiri atau kanan layar
    private void RespawnDiSisi()
    {
        // Tentukan sisi spawn secara acak (kiri atau kanan)
        
        float spawnX=Random.Range(0, 2)==0?Screen.width/120f : -Screen.width/110f; // Ubah posisi spawn, posisi kiri menjadi kanan
       
        float spawnY=Random.Range(-Screen.height/80f, Screen.height/80f);

        // Set posisi musuh di sisi kiri atau kanan dengan posisi Y acak
        transform.position = new Vector2(spawnX, spawnY);

        // Tentukan arah pergerakan horizontal berdasarkan sisi spawn
        arahGerak=spawnX<0? Vector2.right:Vector2.left; // Membalikkan arah pergerakan (kiri ke kanan)

        // Membalikkan gambar sesuai arah gerakan
        renderSprite.flipX=arahGerak==Vector2.left;
        // Pastikan rotasi tetap pada keadaan awal (menghadap arah horizontal)
        transform.rotation=Quaternion.identity;
    }
    
}

