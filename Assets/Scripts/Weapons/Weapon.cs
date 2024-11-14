using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds=3f;
    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private readonly bool collectionCheck=false;
    private readonly int defaultCapacity=30;
    private readonly int maxSize=100;
    private float timer;
    public Transform parentTransform;

    private void Awake()
    {
        // Inisialisasi object pool untuk peluru
        objectPool = new ObjectPool<Bullet>(BuatPeluru,AmbilDariPool,KembalikanKePool,HancurkanObjekDariPool,collectionCheck,defaultCapacity,maxSize);
    }

    private void FixedUpdate()
    {
        // Update timer menggunakan Time.deltaTime dan cek apakah interval tembak tercapai
        if ((timer += Time.fixedDeltaTime)>=shootIntervalInSeconds)
        {
            timer = 0f;

            Shoot(); // Panggil fungsi tembak jika interval sudah tercapai
        }
    }

    public void Shoot()
    {
        Bullet bulletInstance=objectPool.Get(); // Ambil peluru dari pool
        
        bulletInstance.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation); // Set posisi dan rotasi peluru
        bulletInstance.Initialize(); // Inisialisasi gerakan peluru
    }

    private Bullet BuatPeluru()
    {
        
        Bullet bulletInstance=Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation, parentTransform);
        
        bulletInstance.SetPool(objectPool); // Set pool objek pada peluru
        return bulletInstance;
    }

    // Aksi saat peluru diambil dari pool
    private void AmbilDariPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true); // Aktifkan peluru yang diambil dari pool
    
    }

    // Aksi saat peluru dikembalikan ke pool
    private void KembalikanKePool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false); // Nonaktifkan peluru yang dikembalikan ke pool
    
    }

    // Aksi saat menghancurkan peluru yang ada dalam pool jika pool sedang menyusut
    private void HancurkanObjekDariPool(Bullet bullet)
    {
        Destroy(bullet.gameObject); // Hancurkan peluru yang ada di pool
    
    }
}
