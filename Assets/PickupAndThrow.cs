using UnityEngine;

public class PickupAndThrow : MonoBehaviour
{
    public float pickupRange = 3f;
    public Transform holdPoint;
    public float throwForce = 500f;

    private GameObject heldObject;
    private Rigidbody heldRb;

    void Update()
    {
        // Debug draw ray setiap frame
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * pickupRange, Color.red);

        // Tes apakah ada objek throwable di depan
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Throwable"))
            {
                Debug.Log("Benda bisa diambil: " + hit.collider.name);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (heldObject == null)
                TryPickup();
            else
                Drop();
        }

        if (heldObject != null && Input.GetMouseButtonDown(0))
        {
            Throw();
        }

        if (heldObject != null)
        {
            heldObject.transform.localPosition = Vector3.zero;
            heldObject.transform.localRotation = Quaternion.identity;
        }
    }

    void TryPickup()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * pickupRange, Color.red, 1f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Throwable"))
            {
                Debug.Log("Bisa diambil: " + hit.collider.name);

                heldObject = hit.collider.gameObject;
                heldRb = heldObject.GetComponent<Rigidbody>();

                heldRb.useGravity = false;
                heldRb.isKinematic = true;

                heldObject.transform.SetParent(holdPoint);
                heldObject.transform.localPosition = Vector3.zero;
                heldObject.transform.localRotation = Quaternion.identity;
            }
        }
    }


    void Drop()
    {
        heldObject.transform.SetParent(null);
        heldRb.useGravity = true;
        heldRb.isKinematic = false;

        Debug.Log("Menjatuhkan benda: " + heldObject.name);

        heldObject = null;
        heldRb = null;
    }

    void Throw()
    {
        Rigidbody rbToThrow = heldRb;
        Drop();
        rbToThrow.AddForce(Camera.main.transform.forward * throwForce);
        Debug.Log("Melempar benda!");
    }
}
