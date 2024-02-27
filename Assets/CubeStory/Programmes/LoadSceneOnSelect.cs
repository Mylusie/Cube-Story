using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnSelect : MonoBehaviour
{
    public GameObject objectToInteractWith; // Objet à interagir
    public string sceneToLoad; // Nom de la scène à charger
    public string sceneToUnload; // Nom de la scène à décharger
    public KeyCode selectButton = KeyCode.Space; // Touche pour sélectionner
    public Material highlightMaterial; // Matériau de surbrillance

    private void Update()
    {
        // Vérifie si l'objet à interagir est en surbrillance et si la touche de sélection est enfoncée
        if (IsObjectHighlighted(objectToInteractWith) && Input.GetKeyDown(selectButton))
        {
            // Charge la nouvelle scène et décharge l'ancienne
            SceneManager.UnloadSceneAsync(sceneToUnload);
            SceneManager.LoadScene(sceneToLoad);
            
        }
    }

    // Vérifie si l'objet est en surbrillance en vérifiant son matériau
    private bool IsObjectHighlighted(GameObject obj)
    {
        if (obj == null) return false;

        Renderer renderer = obj.GetComponent<Renderer>();
        return renderer != null && renderer.sharedMaterial == highlightMaterial;
    }
}
