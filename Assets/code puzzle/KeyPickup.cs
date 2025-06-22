using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public float pickupRange = 3f;
    public LayerMask keyLayer;

    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.LogError("Main Camera tidak ditemukan!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Klik kiri
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, pickupRange, keyLayer))
            {
                if (hit.collider.CompareTag("Key"))
                {
                    AmbilKunci(hit.collider.gameObject);
                }
            }
        }
    }

    void AmbilKunci(GameObject keyObject)
    {
        Debug.Log("Kunci diambil: " + keyObject.name);

        // Bisa tambahkan animasi, suara, dll di sini
        // Contoh: Tambahkan kunci ke inventory atau aktifkan chest
        // Panggil fungsi untuk menambah kunci di GameManager
            GameManager.instance.CollectKey();
        Destroy(keyObject); // Hilangkan kunci dari scene
    }
}
