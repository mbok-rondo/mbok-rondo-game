using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHold : MonoBehaviour
{
    public GameObject Object;
    public Transform PlayerTransform;
    public float range = 3f;
    public float throwForce = 10f;
    public Camera Camera;

    private bool isHolding = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isHolding)
            {
                TryPickUp();
            }
            else
            {
                Drop();
            }
        }
    }

    void TryPickUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                Object = hit.transform.gameObject;
                PickUp();
            }
        }
    }

    void PickUp()
    {
        if (Object == null) return;

        Rigidbody rb = Object.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;  // Untuk mencegah fisika aktif saat dipegang
        }

        Object.transform.SetParent(PlayerTransform); // Menempelkan objek ke player
        Object.transform.localPosition = Vector3.zero; // Set posisi objek ke pusat player
        isHolding = true;
    }

    void Drop()
    {
        if (Object == null) return;

        Object.transform.SetParent(null);
        Rigidbody rb = Object.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(Camera.transform.forward * throwForce, ForceMode.Impulse);
        }

        // ⬇️ Panggil fungsi untuk menghancurkan objek setelah 3 detik
        Target target = Object.GetComponent<Target>();
        if (target != null)
        {
            target.StartDespawnTimer();
        }

        isHolding = false;
        Object = null;
    }
}
