using UnityEngine;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    public Button[] buttons;
    private int selectedButtonIndex = 0;
    public Material selectedMaterial; // Référence au matériau pour la couleur rouge

    void Start()
    {
        SelectButton(selectedButtonIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveSelectionUp();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveSelectionDown();
        }
        else if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.Return))
        {
            ActivateSelectedButton();
        }
    }

    private void MoveSelectionUp()
    {
        selectedButtonIndex = (selectedButtonIndex == 0) ? buttons.Length - 1 : selectedButtonIndex - 1;
        SelectButton(selectedButtonIndex);
    }

    private void MoveSelectionDown()
    {
        selectedButtonIndex = (selectedButtonIndex == buttons.Length - 1) ? 0 : selectedButtonIndex + 1;
        SelectButton(selectedButtonIndex);
    }

    private void SelectButton(int index)
    {
        // Restaurez la couleur d'origine de tous les boutons
        foreach (Button button in buttons)
        {
            button.GetComponent<Image>().material = null;
        }

        // Mettez en rouge le bouton sélectionné
        buttons[index].GetComponent<Image>().material = selectedMaterial;
        buttons[index].Select();
    }

    private void ActivateSelectedButton()
    {
        buttons[selectedButtonIndex].onClick.Invoke();
        // Réinitialiser la couleur de tous les boutons après avoir activé un bouton
        foreach (Button button in buttons)
        {
            button.GetComponent<Image>().material = null;
        }
    }
}
