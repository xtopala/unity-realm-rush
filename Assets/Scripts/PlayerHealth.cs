using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] int healthDecrease = 1;
    [SerializeField] Text healthText;
    [SerializeField] AudioClip playerDmgSFX;

    private void Start()
    {
        healthText.text = health.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().PlayOneShot(playerDmgSFX);

        health -= healthDecrease;
        if (health <= 0)
        {
            KillPlayer();
        }

        healthText.text = health.ToString();
    }

    private void KillPlayer()
    {
        SceneManager.LoadScene(0);
    }
}
