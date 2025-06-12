using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    public Camera cameraMain;
    public Camera cameraPuzzle;
    public PlayerLogic player; // Referensi ke skrip movement player
    public PadlockController padlockController;

    private void OnMouseDown()
    {
        if (!PadlockController.isPuzzleActive) // Pastikan belum dibuka
        {
            EnterPuzzleMode();
        }
    }

   void EnterPuzzleMode()
{
    if (cameraMain != null && cameraPuzzle != null)
    {
        cameraMain.enabled = false;
        cameraPuzzle.enabled = true;
    }

    if (player != null)
    {
        player.canMove = false;
    }

    if (padlockController != null)
    {
        PadlockController.isPuzzleActive = true;

        // Auto-select ruller pertama
        if (padlockController.rullers.Length > 0)
        {
            RullerController.allRullers = padlockController.rullers;
            RullerController.selectedIndex = 0;

            // Panggil fungsi untuk memilih ruller
            typeof(RullerController)
                .GetMethod("SelectRullerByIndex", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.Invoke(null, new object[] { 0 });
        }
    }
RullerController.allRullers = padlockController.rullers;
RullerController.SelectFirstRuller();

    Debug.Log("Masuk mode puzzle: " + gameObject.name);
}

}
