using System.Collections;
using System.Collections.Generic; // Penting: Pastikan ini ada untuk menggunakan List
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public float interactDistance = 3f;

    // --- Bagian baru untuk Inventaris Kunci ---
    private List<string> collectedKeys = new List<string>(); // List untuk menyimpan nama-nama kunci

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                // Coba dapatkan komponen DoorInteraction dari objek yang ditabrak raycast
                // atau dari parent-nya (sesuai cara kamu menggunakan GetComponentInParent)
                DoorInteraction door = hit.collider.GetComponentInParent<DoorInteraction>();
                
                if (door != null)
                {
                    // Panggil metode untuk mengaktifkan pintu dan kirimkan referensi script ini (DoorOpener)
                    // Nantinya, script DoorInteraction akan menggunakan ini untuk mengecek kunci.
                    door.ActivateDoor(this); 
                }
            }
        }
    }

    // --- Metode baru untuk Mengelola Kunci ---

    // Metode untuk menambahkan kunci ke inventaris
    public void CollectKey(string keyName)
    {
        if (!collectedKeys.Contains(keyName)) // Hindari duplikasi kunci
        {
            collectedKeys.Add(keyName);
            Debug.Log("Kunci '" + keyName + "' ditambahkan ke inventaris pemain.");
        }
    }

    // Metode untuk memeriksa apakah pemain memiliki kunci tertentu
    public bool HasKey(string keyName)
    {
        return collectedKeys.Contains(keyName);
    }

    // Opsional: Untuk debug, tampilkan kunci yang dimiliki (akan muncul di layar game)
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 20), "Kunci dimiliki: " + string.Join(", ", collectedKeys));
    }
}