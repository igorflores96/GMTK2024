using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    [SerializeField] private int _bodySize;
    [SerializeField] private int _SceneIndex;
    [SerializeField] private GameObject _winCanva;
    private bool _winActived;
    private void OnEnable() 
    {
        _winActived = false;
        _winCanva.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colidiu com o player");
            
            List<Transform> childrenToCount = new List<Transform>();

            foreach (Transform child in other.gameObject.transform)
            {
                if(child.gameObject.TryGetComponent(out PlayerBody isBody))
                    childrenToCount.Add(child);
            }

            if(childrenToCount.Count >= _bodySize - 1)
            {
                if(!_winActived)
                {
                    _winActived = true;
                    StartCoroutine(NextStage());
                    StartCoroutine(ActivePanel());
                    if(other.gameObject.TryGetComponent(out PlayerMovement pm))
                    {
                        pm.DisableInputs();
                    }
                }
            }
        }
    }

    IEnumerator NextStage()
    {
        yield return new WaitForSeconds(4);

        SceneManager.LoadScene(_SceneIndex);
    }

    IEnumerator ActivePanel()
    {
        yield return new WaitForSeconds(0.5f);
        
        FindObjectOfType<AudioManager>().Play("LevelCompleted");
        _winCanva.SetActive(true);

    }

}
