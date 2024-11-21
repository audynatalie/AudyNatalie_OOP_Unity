using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Instance tunggal dari GameManager yang dapat diakses dari script lain
    public static GameManager Instance { get; private set; }

    // Referensi ke LevelManager yang berada di dalam GameManager
    public LevelManager LevelManager { get; private set; }

    void Awake()
    {
        // Menonaktifkan semua objek anak yang memiliki komponen Canvas atau Image
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Canvas>() != null || child.GetComponent<UnityEngine.UI.Image>() != null)
            {
                child.gameObject.SetActive(false);
            }
        }

        // Mengecek apakah sudah ada instance GameManager yang lain
        if (Instance != null && Instance != this)
        {
            // Jika instance lain sudah ada, hancurkan objek ini untuk mencegah duplikasi
            Destroy(gameObject);
            return;
        }

        // Menetapkan instance ini sebagai GameManager tunggal
        Instance = this;

        // Mengambil referensi ke LevelManager yang berada di dalam GameManager
        LevelManager = GetComponentInChildren<LevelManager>();

        if (LevelManager == null)
        {
            Debug.LogError("LevelManager tidak ditemukan sebagai anak dari GameManager. Pastikan LevelManager ada di dalam GameManager.");
        }

        // Menandai objek GameManager agar tidak dihancurkan saat pergantian scene
        DontDestroyOnLoad(gameObject);

        // Pastikan ada objek bernama "Camera"
        GameObject mainCamera = GameObject.Find("Camera");
        if (mainCamera != null)
        {
            DontDestroyOnLoad(mainCamera); // Menandai kamera agar tidak dihancurkan
        }
        else
        {
            Debug.LogWarning("Camera tidak ditemukan di scene. Pastikan ada objek bernama 'Camera'.");
        }
    }
}
