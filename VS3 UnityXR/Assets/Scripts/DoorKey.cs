using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{

    [SerializeField] private GameObject gameOverPanel; 
    [SerializeField] private float delayBeforeStop = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            StartCoroutine(StopGame());
            Destroy(other.transform.gameObject);
        }
    }
    private IEnumerator StopGame()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        yield return new WaitForSeconds(delayBeforeStop);

        Debug.Log("Het spel is gestopt.");
        Application.Quit(); 
    }
}
