using System.Linq;
using UnityEngine;

namespace FreeBasicFirstPersonController
{
    [AddComponentMenu("Free Basic First Person Controller/First Person Audio")]
    public class FirstPersonAudio : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Target movement component.")]
        public FirstPersonMovement character;
        [Tooltip("Ground check component for detecting ground contact.")]
        public GroundCheck groundCheck;

        [Header("Step & Movement Audio")]
        [Tooltip("AudioSource for walking step sound.")]
        public AudioSource stepAudio;
        [Tooltip("AudioSource for running step sound.")]
        public AudioSource runningAudio;
        [Tooltip("Minimum velocity magnitude for moving audio to play.")]
        public float velocityThreshold = 0.1f;

        [Header("Landing Audio")]
        [Tooltip("AudioSource for landing sound.")]
        public AudioSource landingAudio;
        [Tooltip("Array of landing sound clips.")]
        public AudioClip[] landingSFX;

        [Header("Jump Audio")]
        [Tooltip("Jump component reference.")]
        public Jump jump;
        [Tooltip("AudioSource for jump sound.")]
        public AudioSource jumpAudio;
        [Tooltip("Array of jump sound clips.")]
        public AudioClip[] jumpSFX;

        [Header("Crouch Audio")]
        [Tooltip("Crouch component reference.")]
        public Crouch crouch;
        [Tooltip("AudioSource played when starting crouch.")]
        public AudioSource crouchStartAudio;
        [Tooltip("AudioSource played continuously while crouch walking.")]
        public AudioSource crouchedAudio;
        [Tooltip("AudioSource played when ending crouch.")]
        public AudioSource crouchEndAudio;
        [Tooltip("Array of crouch start sound clips.")]
        public AudioClip[] crouchStartSFX;
        [Tooltip("Array of crouch end sound clips.")]
        public AudioClip[] crouchEndSFX;

        private AudioSource[] MovingAudios => new AudioSource[] { stepAudio, runningAudio, crouchedAudio };
        private Rigidbody characterRb;

        private void Reset()
        {
            character = GetComponentInParent<FirstPersonMovement>();
            groundCheck = (transform.parent != null ? transform.parent : transform).GetComponentInChildren<GroundCheck>();

            stepAudio = GetOrCreateAudioSource("Step Audio", loop: true);
            runningAudio = GetOrCreateAudioSource("Running Audio", loop: true);
            landingAudio = GetOrCreateAudioSource("Landing Audio", loop: false);

            jump = GetComponentInParent<Jump>();
            if (jump != null)
            {
                jumpAudio = GetOrCreateAudioSource("Jump Audio", loop: false);
            }

            crouch = GetComponentInParent<Crouch>();
            if (crouch != null)
            {
                crouchStartAudio = GetOrCreateAudioSource("Crouch Start Audio", loop: false);
                crouchedAudio = GetOrCreateAudioSource("Crouched Audio", loop: true);
                crouchEndAudio = GetOrCreateAudioSource("Crouch End Audio", loop: false);
            }
        }

        private void Awake()
        {
            if (character != null)
            {
                characterRb = character.GetComponent<Rigidbody>();
            }
        }

        private void OnEnable() => SubscribeToEvents();
        private void OnDisable() => UnsubscribeToEvents();

        private void FixedUpdate()
        {
            if (character == null) return;

            if (characterRb == null) characterRb = character.GetComponent<Rigidbody>();
            Vector3 currentVelocity = GetCharacterVelocity();
            Vector3 horizontalVel = new Vector3(currentVelocity.x, 0, currentVelocity.z);
            float currentSpeed = horizontalVel.magnitude;

            if (currentSpeed >= velocityThreshold && (groundCheck == null || groundCheck.isGrounded))
            {
                if (crouch != null && crouch.IsCrouched)
                {
                    SetPlayingMovingAudio(crouchedAudio);
                }
                else if (character.IsRunning)
                {
                    SetPlayingMovingAudio(runningAudio);
                }
                else
                {
                    SetPlayingMovingAudio(stepAudio);
                }
            }
            else
            {
                SetPlayingMovingAudio(null);
            }
        }

        private Vector3 GetCharacterVelocity()
        {
            if (characterRb == null) return Vector3.zero;
#if UNITY_6000_0_OR_NEWER
            return characterRb.linearVelocity;
#else
            return characterRb.velocity;
#endif
        }

        private void SetPlayingMovingAudio(AudioSource audioToPlay)
        {
            foreach (var audio in MovingAudios.Where(a => a != audioToPlay && a != null))
            {
                if (audio.isPlaying)
                {
                    audio.Pause();
                }
            }

            if (audioToPlay != null && audioToPlay.clip != null)
            {
                audioToPlay.loop = true;
                if (!audioToPlay.isPlaying)
                {
                    audioToPlay.UnPause();
                    if (!audioToPlay.isPlaying)
                    {
                        audioToPlay.Play();
                    }
                }
            }
        }

        #region Event Audio Callbacks
        private void PlayLandingAudio() => PlayRandomClip(landingAudio, landingSFX);
        private void PlayJumpAudio() => PlayRandomClip(jumpAudio, jumpSFX);
        private void PlayCrouchStartAudio() => PlayRandomClip(crouchStartAudio, crouchStartSFX);
        private void PlayCrouchEndAudio() => PlayRandomClip(crouchEndAudio, crouchEndSFX);
        #endregion

        #region Event Subscriptions
        private void SubscribeToEvents()
        {
            if (groundCheck != null) groundCheck.Grounded += PlayLandingAudio;
            if (jump != null) jump.Jumped += PlayJumpAudio;
            if (crouch != null)
            {
                crouch.CrouchStart += PlayCrouchStartAudio;
                crouch.CrouchEnd += PlayCrouchEndAudio;
            }
        }

        private void UnsubscribeToEvents()
        {
            if (groundCheck != null) groundCheck.Grounded -= PlayLandingAudio;
            if (jump != null) jump.Jumped -= PlayJumpAudio;
            if (crouch != null)
            {
                crouch.CrouchStart -= PlayCrouchStartAudio;
                crouch.CrouchEnd -= PlayCrouchEndAudio;
            }
        }
        #endregion

        #region Helpers
        private AudioSource GetOrCreateAudioSource(string sourceName, bool loop = false)
        {
            AudioSource result = System.Array.Find(GetComponentsInChildren<AudioSource>(), a => a.name == sourceName);
            if (result != null)
            {
                result.loop = loop;
                return result;
            }

            GameObject audioGo = new GameObject(sourceName);
            audioGo.transform.SetParent(transform, false);
            result = audioGo.AddComponent<AudioSource>();
            result.spatialBlend = 1f;
            result.playOnAwake = false;
            result.loop = loop;
            return result;
        }

        private static void PlayRandomClip(AudioSource audioSource, AudioClip[] clips)
        {
            if (audioSource == null || clips == null || clips.Length == 0) return;

            AudioClip clip = clips[Random.Range(0, clips.Length)];
            if (clips.Length > 1 && clips.Any(c => c != audioSource.clip))
            {
                while (clip == audioSource.clip)
                {
                    clip = clips[Random.Range(0, clips.Length)];
                }
            }

            audioSource.clip = clip;
            audioSource.Play();
        }
        #endregion
    }
}
