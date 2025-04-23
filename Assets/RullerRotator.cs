using UnityEngine;

public class RullerRotator : MonoBehaviour
{
    public float rotationSpeed = 50f; // Kecepatan putar
    private bool isRotating = false;

    void Update()
    {
        // Tombol input (ganti sesuai preferensi: klik, drag, UI button)
        if (isRotating)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    // Event ketika mouse klik ke objek
    private void OnMouseDown()
    {
        isRotating = true;
    }

    private void OnMouseUp()
    {
        isRotating = false;
    }
}
