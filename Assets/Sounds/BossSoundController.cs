using UnityEngine;

public class BossSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip runSound;
    public AudioClip attackSound;

    public void PlayRunSound()
    {
        audioSource.clip = runSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopRunSound()
    {
        if (audioSource.isPlaying && audioSource.clip == runSound)
        {
            audioSource.Stop();
        }
    }

    public void PlayAttackSound()
    {
        audioSource.PlayOneShot(attackSound);
    }
}
