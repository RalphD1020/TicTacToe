using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    // Reference to the button
    public Button button;
    public GameObject emptyPrefab;
    public GameObject xPrefab;
    public GameObject oPrefab;
    private char currentPlayer = 'X';
    
    public void Start()
    {
        ActivatePrefab(emptyPrefab);
    }

    // Function that gets called when the button is clicked
    public void OnButtonClick()
    {
        Debug.Log("Current player is: " + currentPlayer);
        updateButtonDisplay();
        // button.interactable = false;
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
        if (currentPlayer == 'X')
        {
            ActivatePrefab(xPrefab);
            currentPlayer = 'O';
        }
        else
        {
            ActivatePrefab(oPrefab);
            currentPlayer = 'X';
        }
    }
}