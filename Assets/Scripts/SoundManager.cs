using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioClip bulletSound;
    [SerializeField] private AudioClip dieSound;

    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBulletSound()
    {
        audioSource.PlayOneShot(bulletSound);
    }

    public void PlayDieSound()
    {
        audioSource.PlayOneShot(dieSound);
    }
}
