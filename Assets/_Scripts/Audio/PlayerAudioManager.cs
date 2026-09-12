using RF.Control;
using UnityEngine;

namespace RF.Audio
{
    public class PlayerAudioManager : MonoBehaviour
    {
        [Header("BOUNCE")]
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioClip[] bounceClips;
        [SerializeField] private float pitchMax = 1.5f;
        [SerializeField] private float pitchMin = 1f;

        PlayerController trampolineController;

        private void Awake()
        {
            trampolineController = GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            trampolineController.onBounce += PlayBounceClip;
        }

        private void OnDisable()
        {
            trampolineController.onBounce -= PlayBounceClip;
        }

        public void PlayBounceClip()
        {
            // if (sfxSource.isPlaying) return;

            int index = Random.Range(0, bounceClips.Length);
            float pitchDeviation = Random.Range(pitchMin, pitchMax);

            sfxSource.pitch = pitchDeviation;
            sfxSource.PlayOneShot(bounceClips[index]);
        }
    }
}
