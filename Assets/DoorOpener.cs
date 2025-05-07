using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public float interactDistance = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                DoorInteraction door = hit.collider.GetComponentInParent<DoorInteraction>();
                if (door != null)
                {
                    door.ActivateDoor();
                }
            }
        }
    }
}
