using UnityEngine;
using UnityEngine.UI;

public enum TileState {Empty, X, O}
public class Tile : MonoBehaviour
{
    // Reference to the button
    public Button button;
    public GameObject emptyPrefab;
    public GameObject xPrefab;
    public GameObject oPrefab;
    public TileState State { get; set; }

    public void Start()
    {
        ActivatePrefab(emptyPrefab);
    }

    // Function that gets called when the button is clicked
    public void OnButtonClick()
    {
        Debug.Log("Current player is: " + TicTacToeManager.Instance.currentPlayer);
        UpdateButtonDisplay();
        TicTacToeManager.Instance.PerformMove(int.Parse(button.name));
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
    private void UpdateButtonDisplay()
    {
        if (TicTacToeManager.Instance.currentPlayer == TileState.X)
        {
            ActivatePrefab(xPrefab);
        }
        else
        {
            ActivatePrefab(oPrefab);
        }
    }
}