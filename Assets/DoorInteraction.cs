using System.Collections;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public bool isOpen = false;

    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    private Coroutine _currentCoroutine;
    private GameManager gameManager;
    // --- Bagian untuk Fungsionalitas Kunci ---
    public bool requiresKey = false;
    
    public bool isLocked = true; // Tetap true secara default di sini, logikanya ada di Start()

    // --- Bagian untuk Interaksi Musuh ---
    public bool canBeOpenedByEnemy = false; 
    public float enemyDetectionRadius = 3f; 

    private Collider[] _nearbyColliders = new Collider[5]; 

    void Start()
    {
        _closedRotation = transform.rotation;
        _openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));

        // --- MODIFIKASI PENTING DI SINI ---
        // Jika pintu tidak memerlukan kunci ATAU bisa dibuka oleh musuh,
        // maka pintu ini secara default tidak terkunci saat Start.
        if (!requiresKey || canBeOpenedByEnemy) 
        {
            isLocked = false; 
        }
        gameManager = GameManager.instance;
        // Jika requiresKey dicentang dan canBeOpenedByEnemy tidak dicentang,
        // maka isLocked akan tetap true (default) dan hanya bisa dibuka pemain dengan kunci.
    }

    void Update()
    {
        // Logika Interaksi Musuh
        // Penting: Musuh hanya bisa membuka pintu yang tidak terkunci (isLocked = false)
        // dan tidak memerlukan kunci (requiresKey = false).
        // Kita juga tambahkan !requiresKey ke kondisi ini.
        if (canBeOpenedByEnemy && !isLocked && !requiresKey && !isOpen) 
        {
            int numColliders = Physics.OverlapSphereNonAlloc(transform.position, enemyDetectionRadius, _nearbyColliders, LayerMask.GetMask("Enemy"));
            
            for (int i = 0; i < numColliders; i++)
            {
                if (_nearbyColliders[i].CompareTag("Enemy"))
                {
                    Debug.Log("Musuh terdeteksi, membuka pintu!");
                    if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
                    _currentCoroutine = StartCoroutine(ToggleDoor());
                    break; 
                }
            }
        }
    }

    public void ActivateDoor(DoorOpener interactingPlayer)
{
    if (isLocked)
    {
        if (requiresKey)
        {
            if (GameManager.instance != null && GameManager.instance.GetKeyCount() >= 4)
            {
                Debug.Log("Pintu terbuka! Sudah mengumpulkan 4 kunci.");
                isLocked = false;

                if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
                _currentCoroutine = StartCoroutine(ToggleDoor());
            }
            else
            {
                Debug.Log("Pintu terkunci. Kumpulkan 4 kunci terlebih dahulu.");
            }
        }
        else
        {
            Debug.Log("Pintu terkunci. Tidak memerlukan kunci, tapi terkunci oleh sistem lain.");
        }
    }
    else
    {
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(ToggleDoor());
    }
}

    private IEnumerator ToggleDoor()
    {
        Quaternion targetRotation = isOpen ? _closedRotation : _openRotation;
        isOpen = !isOpen;

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
            yield return null;
        }

        transform.rotation = targetRotation;
    }

    void OnDrawGizmosSelected()
    {
        if (canBeOpenedByEnemy)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, enemyDetectionRadius);
        }
    }
}