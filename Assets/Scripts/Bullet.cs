using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed=20;
    public int damage=10;
    private Rigidbody2D rb;
    public IObjectPool<Bullet>poolObjek;

    void Start()
    {
        // Mengambil komponen Rigidbody2D dan set kecepatan awal peluru
        rb=GetComponent<Rigidbody2D>();

        SetKecepatanPeluru();
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        poolObjek=pool;
    }

    public void Initialize()
    {
        // Set kecepatan peluru jika belum terinisialisasi
        if (rb==null) rb=GetComponent<Rigidbody2D>();
        SetKecepatanPeluru();
    }

    private void SetKecepatanPeluru()
    {
        // Menetapkan kecepatan peluru berdasarkan arah transformasi objek
        rb.velocity=transform.up*bulletSpeed;
    }

    private void KembalikanKePool()
    {
        poolObjek.Release(this);
    }

    void OnTriggerEnter2D(Collider2D tabrakan)
    {
        // Kembalikan ke pool saat peluru bertabrakan dengan objek tertentu
        if (tabrakan.CompareTag("Enemy") || tabrakan.CompareTag("Obstacle"))
        {

            poolObjek.Release(this);
        }
    }
}