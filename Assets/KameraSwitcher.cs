using UnityEngine;

public class KameraSwitcher : MonoBehaviour
{
    public Camera cameraMain;   // Kamera utama
    public Camera camera1;      // Kamera alternatif (misal mengarah ke chest)

    void Start()
    {
        // Aktifkan kamera utama saat awal
        ActivateCamera(cameraMain);
    }

    // Fungsi ini dipanggil saat objek ini diklik (pastikan punya Collider)
    void OnMouseDown()
    {
        // Saat objek (misalnya chest) diklik, pindah ke kamera1
        ActivateCamera(camera1);
        Debug.Log("Beralih ke kamera 1 karena objek diklik");
    }

    // Opsional: kembali ke kamera utama saat tekan Y
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ActivateCamera(cameraMain);
            Debug.Log("Kembali ke kamera utama");
        }
    }

    void ActivateCamera(Camera camToActivate)
    {
        cameraMain.enabled = false;
        camera1.enabled = false;

        camToActivate.enabled = true;
    }
}
