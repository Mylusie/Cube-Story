using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CodeMenu : MonoBehaviour
{
    public Transform target; // Référence à l'objet vers lequel la caméra se déplacera
    public float movementSpeed = 2f; // Vitesse de déplacement de la caméra
    public float DistanceAcceptable = 2f; // Vitesse de déplacement de la caméra
    public string sceneToLoad; // Nom de la scène à charger

    private Coroutine moveCoroutine; // Référence à la coroutine de mouvement de la caméra

    public void MoveCameraAndLoadScene()
    {


        // Arrête l'animation de la caméra
        GameObject mainCameraObject = GameObject.FindWithTag("MainCamera");
        if (mainCameraObject != null)
        {
            Animator cameraAnimator = mainCameraObject.GetComponent<Animator>();
            if (cameraAnimator != null)
            {
                cameraAnimator.enabled = false;
            }
        }

        // Fait disparaître le menu
        gameObject.SetActive(false);

        // Trouve la caméra principale de la scène
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found in the scene!");
            return;
        }

        // Active le GameObject contenant le script
        gameObject.SetActive(true);

        // Démarre la coroutine de déplacement de la caméra
        moveCoroutine = StartCoroutine(MoveCameraCoroutine(mainCamera.transform, target.position));
    }

    IEnumerator MoveCameraCoroutine(Transform cameraTransform, Vector3 targetPosition)
    {
        // Déplacement de la caméra vers l'objet cible
        while (Vector3.Distance(cameraTransform.position, targetPosition) > DistanceAcceptable)
        {
            cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, targetPosition, movementSpeed * Time.deltaTime);
            yield return null;
        }

        // Une fois que la caméra a atteint l'objet cible, charge la nouvelle scène
        SceneManager.LoadScene(sceneToLoad);
    }

    public GameObject mainPart; // Référence au GameObject à cacher (par exemple, le menu principal)
    public GameObject options; // Référence au GameObject à afficher (par exemple, le menu des options)

    public void ShowOptions()
    {
        // Cacher le menu principal
        mainPart.SetActive(false);

        // Afficher le menu des options
        options.SetActive(true);
    }

    
    public void Retour()
    {
        // Cacher le menu principal
        mainPart.SetActive(true);

        // Afficher le menu des options
        options.SetActive(false);
    }
}
