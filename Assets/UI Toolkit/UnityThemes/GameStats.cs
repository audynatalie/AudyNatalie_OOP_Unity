using UnityEngine;
using UnityEngine.UIElements;

public class GameStats : MonoBehaviour
{
    public VisualElement rootVisualElement;

    private Label health;
    private Label enemiesleft;
    private Label wave;
    private Label point;

    private HealthComponent playerHealthComponent;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        rootVisualElement = uiDocument.rootVisualElement;

        health = rootVisualElement.Q<Label>("health");
        enemiesleft = rootVisualElement.Q<Label>("enemiesleft");
        wave = rootVisualElement.Q<Label>("wave");
        point = rootVisualElement.Q<Label>("point");

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerHealthComponent = playerObject.GetComponent<HealthComponent>();
        }
    }

    public void SetHealth(float healthValue)
    {
        health.text = $"Health: {healthValue}";
    }

    public void SetEnemiesLeft(int enemiesLeftCount)
    {
        enemiesleft.text = $"Enemies Left: {enemiesLeftCount}";
    }

    public void SetWave(int waveValue)
    {
        wave.text = $"Wave: {waveValue}";
    }

    public void SetPoints(int pointsValue)
    {
        point.text = $"Points: {pointsValue}";
    }

    private void Update()
    {
        if (playerHealthComponent != null)
        {
            SetHealth(playerHealthComponent.Health);
        }
    }
}
