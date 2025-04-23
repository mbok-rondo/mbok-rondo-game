using UnityEngine;

public class ChestClick : MonoBehaviour
{
    public GameObject chestOpened; // Drag objek chest yang terbuka di Inspector

    void OnMouseDown()
    {
        chestOpened.SetActive(true);   // Munculkan chest terbuka
        gameObject.SetActive(false);   // Sembunyikan chest tertutup
    }
}
