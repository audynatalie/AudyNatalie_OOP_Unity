using UnityEngine;
using UnityEngine.UIElements;

public class GameStats : MonoBehaviour
{
    public VisualElement rootVisualElement;
    private Label healthLabel;
    private Label enemiesLeftLabel;
    private Label waveLabel;
    private Label pointsLabel;
    private HealthComponent playerHealthComponent;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        rootVisualElement = uiDocument.rootVisualElement;

        // Menggunakan metode Find untuk mendapatkan elemen UI
        healthLabel = FindLabel("health");
        enemiesLeftLabel = FindLabel("enemiesleft");
        waveLabel = FindLabel("wave");
        pointsLabel = FindLabel("point");

        // Mendapatkan referensi ke HealthComponent dari objek dengan tag "Player"
        playerHealthComponent = FindPlayerHealthComponent();
    }

    // Fungsi untuk mencari elemen Label berdasarkan nama
    private Label FindLabel(string name)
    {
        return rootVisualElement.Q<Label>(name);
    }

    // Fungsi untuk mencari HealthComponent pada objek Player
    private HealthComponent FindPlayerHealthComponent()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        return playerObject != null ? playerObject.GetComponent<HealthComponent>() : null;
    }

    // Fungsi untuk memperbarui tampilan kesehatan pemain
    public void UpdateHealth(float health)
    {
        healthLabel.text = $"Health: {health}";
    }

    // Fungsi untuk memperbarui tampilan jumlah musuh yang tersisa
    public void UpdateEnemiesLeft(int enemiesLeft)
    {
        enemiesLeftLabel.text = $"Enemies Left: {enemiesLeft}";
    }

    // Fungsi untuk memperbarui tampilan gelombang
    public void UpdateWave(int wave)
    {
        waveLabel.text = $"Wave: {wave}";
    }

    // Fungsi untuk memperbarui tampilan poin
    public void UpdatePoints(int points)
    {
        pointsLabel.text = $"Points: {points}";
    }

    private void Update()
    {
        // Memperbarui kesehatan jika HealthComponent tersedia
        if (playerHealthComponent != null)
        {
            UpdateHealth(playerHealthComponent.Health);
        }
    }
}
