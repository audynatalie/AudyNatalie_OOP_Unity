using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder; // Referensi senjata yang akan di-*pickup*
    private Weapon weapon; // Instance dari weaponHolder yang akan di-*pickup*
    private static Weapon weaponNow;
    private void Awake()
    {
        // Inisialisasi weapon dengan instansiasi dari weaponHolder
            weapon = Instantiate(weaponHolder);
            weapon.gameObject.SetActive(false); // Menonaktifkan tampilan senjata saat awal
        
    }

    private void Start()
    {
        // Pastikan visual senjata dinonaktifkan saat awal permainan
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Memastikan objek yang masuk ke collider memiliki tag "Player"
        if (other.CompareTag("Player"))
        {
            // Mengecek apakah pemain sudah memiliki senjata
            Weapon weaponNow = other.GetComponentInChildren<Weapon>();

            if (weaponNow != null){
                weaponNow.transform.SetParent(transform,false);
                weaponNow.transform.localPosition = Vector3.zero;
                TurnVisual(false, weaponNow);        
            }


                weaponNow = weapon;
                // Mengatur parent dari weapon menjadi objek player tanpa memindahkannya dari WeaponPickup
                weapon.transform.SetParent(Player.Instance.transform);
                weapon.transform.localPosition = Vector3.zero;

                // Mengaktifkan visual senjata agar tampak oleh pemain
                TurnVisual(true);
    
            
                Debug.Log("Pemain sudah memiliki senjata yang ter-*equip*.");
            
        }
        else
        {
            Debug.Log("Bukan objek Player yang memasuki Trigger");
        }
    }

    // Metode untuk mengaktifkan atau menonaktifkan visual senjata
    private void TurnVisual(bool on)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }

    // Overload metode TurnVisual dengan parameter senjata tertentu untuk polymorphism
    private void TurnVisual(bool on, Weapon weapon)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }
}


