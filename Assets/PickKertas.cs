using UnityEngine;
using UnityEngine.UI;

public class PickKertas : MonoBehaviour
{
    public float jarakAmbil = 3f;
    public LayerMask layerKertas;

    public Text textKode1;
    public Text textKode2;
    public Text textKode3;
    public Text textKode4;
    private int jumlahKertasDiambil = 0;

    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.LogError("Main Camera tidak ditemukan!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, jarakAmbil, layerKertas))
            {
                if (hit.collider.CompareTag("Kertas"))
                {
                    AmbilKertas(hit.collider.gameObject);
                }
            }
        }
    }

    void AmbilKertas(GameObject kertas)
    {
        KertasInfo info = kertas.GetComponent<KertasInfo>();
        if (info != null && info.sumberKode != null)
        {
            string kode = info.sumberKode.GeneratedCode;
            jumlahKertasDiambil++;

            // Tampilkan ke UI sesuai urutan
          switch (jumlahKertasDiambil)
{
    case 1:
        if (textKode1 != null)
        {
            textKode1.gameObject.SetActive(true);
            textKode1.text = "Kode Kertas 1: " + kode;
        }
        break;
    case 2:
        if (textKode2 != null)
        {
            textKode2.gameObject.SetActive(true);
            textKode2.text = "Kode Kertas 2: " + kode;
        }
        break;
    case 3:
        if (textKode3 != null)
        {
            textKode3.gameObject.SetActive(true);
            textKode3.text = "Kode Kertas 3: " + kode;
        }
        break;
    case 4:
        if (textKode4 != null)
        {
            textKode4.gameObject.SetActive(true);
            textKode4.text = "Kode Kertas 4: " + kode;
        }
        break;
    default:
        Debug.LogWarning("Lebih dari 4 kertas diambil");
        break;
}
        }
        else
        {
            Debug.LogWarning("Tidak menemukan referensi GantiTextMesh pada kertas " + kertas.name);
        }

        Destroy(kertas); // Hilangkan kertas dari scene
    }
}
