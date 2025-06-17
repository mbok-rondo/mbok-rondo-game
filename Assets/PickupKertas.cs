// using UnityEngine;

// public class PickupKertas : MonoBehaviour
// {
//     public TextMesh kodeText;                 // Referensi ke TextMesh pada kertas
//     public PadlockController padlockTarget;   // Padlock yang akan diberi kode

//     private void OnMouseDown()
//     {
//         if (kodeText != null && padlockTarget != null)
//         {
//             // Ambil kode dari kertas
//             string kode = kodeText.text;
//             padlockTarget.correctCode = kode;

//             Debug.Log("Kertas diambil. Kode = " + kode);

//             // Nonaktifkan atau hancurkan kertas setelah diambil
//             gameObject.SetActive(false);
//             // Atau: Destroy(gameObject);
//         }
//     }
// }
