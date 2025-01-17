using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float reloadDelay = 2.0f;
    [SerializeField] ParticleSystem finishEffect;
    [SerializeField] AudioClip finishSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("You finished!");

            finishEffect.Play();

            MakeFinishSound();

            Invoke(nameof(LoadNextScene), reloadDelay);     
        }
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex<SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more scenes to load!");
        }
    }

    void MakeFinishSound()
    {
        AudioSource audioSource = FindObjectOfType<AudioSource>();
        audioSource.clip = finishSound;
        audioSource.Play();
    }
}
