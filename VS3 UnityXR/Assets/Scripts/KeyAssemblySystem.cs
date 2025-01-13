using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyAssemblySystem : MonoBehaviour
{
    [Header("XR Sockets")]
    [SerializeField] private XRSocketInteractor socket1;
    [SerializeField] private XRSocketInteractor socket2;
    [SerializeField] private XRSocketInteractor socket3;

    [Header("XR Button")]
    [SerializeField] private XRBaseInteractable button;

    [Header("Full Key")]
    [SerializeField] private GameObject fullKeyPrefab; 
    [SerializeField] private Transform spawnPoint; 

    private bool keyAssembled = false;

    private void Start()
    {
        // Luister naar de knopinteractie
        button.selectEntered.AddListener(OnButtonPressed);
    }

    private void OnDestroy()
    {
        // Verwijder de listener om geheugenlekken te voorkomen
        button.selectEntered.RemoveListener(OnButtonPressed);
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        // Controleer of alle onderdelen in de sockets zijn geplaatst
        if (AreAllPartsInPlace() && !keyAssembled)
        {
            AssembleKey();
        }
    }

    private bool AreAllPartsInPlace()
    {
        return socket1.hasSelection && socket2.hasSelection && socket3.hasSelection;
    }

    private void AssembleKey()
    {
        keyAssembled = true;

        // Spawn het volledige sleutelobject
        if (fullKeyPrefab != null && spawnPoint != null)
        {
            Instantiate(fullKeyPrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("De sleutel is geassembleerd!");
        }
        else
        {
            Debug.LogWarning("Volledige sleutel prefab of spawnpunt niet ingesteld.");
        }

        RemoveObjectFromSocket(socket1);
        RemoveObjectFromSocket(socket2);
        RemoveObjectFromSocket(socket3);
    }

    private void RemoveObjectFromSocket(XRSocketInteractor socket)
    {
        if (socket.hasSelection)
        {
            var interactable = socket.GetOldestInteractableSelected();
            if (interactable != null)
            {
                Destroy(interactable.transform.gameObject); 
                Debug.Log($"Object uit {socket.name} verwijderd.");
            }
        }
    }
}
