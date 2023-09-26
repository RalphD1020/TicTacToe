using UnityEngine;
using UnityEngine.UI;

public class TicTacToeTile : MonoBehaviour
{
    // Reference to the button
    public Button button;
    public GameObject emptyPrefab;
    public GameObject xPrefab;
    public GameObject oPrefab;
    
    public void Start()
    {
        ActivatePrefab(emptyPrefab);
    }

    // Function that gets called when the button is clicked
    public void OnButtonClick()
    {
        Debug.Log("Current player is: " + TicTacToeManager.Instance.currentPlayer);
        updateButtonDisplay();
        TicTacToeManager.Instance.performMove();
        button.interactable = false;
    }
    
    private void ActivatePrefab(GameObject prefabToActivate)
    {
        // Deactivate all prefabs
        emptyPrefab.SetActive(false);
        xPrefab.SetActive(false);
        oPrefab.SetActive(false);

        // Activate the chosen prefab
        prefabToActivate.SetActive(true);
    }

    // todo: Define Button Display Behavior
    public void updateButtonDisplay()
    {
        if (TicTacToeManager.Instance.currentPlayer == 'X')
        {
            ActivatePrefab(xPrefab);
        }
        else
        {
            ActivatePrefab(oPrefab);
        }
    }
}