using UnityEngine;

public class KeyPickupOnClick : MonoBehaviour
{
    void OnMouseDown()
    {
        PickupKey();
    }

    void PickupKey()
    {
        Debug.Log("Key picked up by click!");

        // Contoh: tambahkan logika lain seperti buka pintu, aktifkan objek, dsb.
        // Misalnya aktifkan peti terbuka:
        // chestOpen.SetActive(true);

        // Nonaktifkan kunci
        gameObject.SetActive(false);
    }
}
