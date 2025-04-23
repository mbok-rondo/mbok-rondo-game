using UnityEngine;

public class FourDirectionMovement : MonoBehaviour
{
    public float moveSpeed = 0.5f;

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection = Vector3.forward; // Maju
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection = Vector3.back;    // Mundur
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDirection = Vector3.left;    // Kiri
        }
        else if (Input.GetKey(KeyCode.W))
        {
            moveDirection = Vector3.right;   // Kanan
        }

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}
