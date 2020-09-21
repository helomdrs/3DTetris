using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public AudioClip landPiece, gameOver;
    public AudioSource soundtrack;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayLandPiece()
    {
        audioSource.PlayOneShot(landPiece);
    }

    public void PlayGameOver()
    {
        soundtrack.Pause();
        audioSource.PlayOneShot(gameOver);
    }
}
