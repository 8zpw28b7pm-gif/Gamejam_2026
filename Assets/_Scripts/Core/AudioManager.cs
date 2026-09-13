using UnityEngine;

namespace RF.Core
{
    public class AudioManager : MonoBehaviour
    {

        [SerializeField] private AudioSource popSource;
        [SerializeField] private AudioClip[] popSounds;

        [SerializeField] private AudioClip[] fallOffScreenClips;

        private void Awake()
        {
            GameManager.Instance.AudioManager = this;
        }

        public void PlayPopSound()
        {
            int index = Random.Range(0, popSounds.Length);
            float pitch = Random.Range(1f, 1.2f);

            if (popSounds[index] != null)
            {
                popSource.pitch = pitch;
                popSource.PlayOneShot(popSounds[index]);
            }
        }

        public void PlayAudioClipAtPoint(AudioClip clip, float volume = 1f)
        {
            if (clip == null) return;

            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
        }

        public void PlayRandomAudioClip(AudioClip[] clips, float volume = 1f)
        {
            if (clips.Length < 1) return;

            AudioClip clipToPlay = clips[Random.Range(0, clips.Length)];

            if (clipToPlay != null)
            {
                AudioSource.PlayClipAtPoint(clipToPlay, Camera.main.transform.position, volume);
            }
        }

        public void PlayFallOffScreenSound()
        {
            if (fallOffScreenClips.Length < 1) return;

            AudioClip clipToPlay = fallOffScreenClips[Random.Range(0, fallOffScreenClips.Length)];

            if (clipToPlay != null)
            {
                AudioSource.PlayClipAtPoint(clipToPlay, Camera.main.transform.position, 0.2f);
            }
        }
    }
}