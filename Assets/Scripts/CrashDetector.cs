using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrushDetector : MonoBehaviour
{
    [SerializeField] float reloadDelay = 2.0f;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSound;
    SnowTrail snowTrail;

    SurfaceEffector2D surfaceEffector2D;
    // Start is called before the first frame update
    void Start()
    {
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            crashEffect.Play();
            snowTrail.snowTrail.Stop();

            FindObjectOfType<PlayerController>().DisableControls();

            Debug.Log("You hit the ground!");

            MakeCrashSound();

            surfaceEffector2D.enabled = false;

            Invoke(nameof(ReloadScene), reloadDelay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void MakeCrashSound()
    {
        AudioSource audioSource = FindObjectOfType<AudioSource>();
        audioSource.clip = crashSound;
        audioSource.Play();

    }
}
