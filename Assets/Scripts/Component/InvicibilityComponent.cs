using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(HitboxComponent))]
public class InvicibiltyComponent : MonoBehaviour
{
    [SerializeField] private int blinkingCount = 7;  // Number of times the object will blink
    [SerializeField] private float blinkInterval = 0.1f;  // Interval between each blink
    [SerializeField] private Material blinkMaterial;  // Material used when the object is invincible
    private SpriteRenderer spriteRenderer;  // The SpriteRenderer component of the object
    private HitboxComponent hitboxComponent;  // The HitboxComponent of the object
    private Material originalMaterial;  // The original material of the object
    public bool isInvincible = false;  // Flag to check if the object is currently invincible

    // Start is called before the first frame update
    void Awake()
    {
        hitboxComponent = GetComponent<HitboxComponent>();  // Get the HitboxComponent
        spriteRenderer = GetComponent<SpriteRenderer>();  // Get the SpriteRenderer component
        originalMaterial = spriteRenderer.material;  // Save the original material of the object
    }

    // Coroutine to handle the blinking effect when invincible
    private IEnumerator FlashRoutine()
    {
        for (int i = 0; i < blinkingCount; i++)  // Loop to blink the object a set number of times
        {
            spriteRenderer.material = blinkMaterial;  // Set the material to the blinking material
            Debug.Log("Invincible");  // Log that the object is invincible
            yield return new WaitForSeconds(blinkInterval);  // Wait for the blink interval
            spriteRenderer.material = originalMaterial;  // Reset the material back to original
            Debug.Log("Vulnerable");  // Log that the object is now vulnerable
            yield return new WaitForSeconds(blinkInterval);  // Wait for the blink interval again
        }
        isInvincible = false;  // After blinking, set invincibility to false
    }

    // Method to start the invincibility effect
    public void StartInvincibility()
    {
        if (!isInvincible)  // Check if the object is not already invincible
        {
            StartCoroutine(FlashRoutine());  // Start the flashing routine
            Debug.Log("Started invincibility.");  // Log that invincibility started
        }
    }

    void Update()
    {
        // No functionality in Update for now, but can be used for future updates or checks
    }
}
