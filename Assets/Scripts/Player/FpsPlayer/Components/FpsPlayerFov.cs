using Koi.FpsPlayer.Player.FpsPlayer.Constants;
using UnityEngine;
using UnityEngine.Events;

namespace Koi.FpsPlayer.Player.FpsPlayer.Components
{
    /// <summary>
    /// A component to manage the field of view of an FPS player.
    /// </summary>
    public class FpsPlayerFov : MonoBehaviour
    {
        /// <summary>
        /// The default FOV.
        /// </summary>
        [SerializeField]
        [Range(FovConstants.MinValue, FovConstants.MaxValue)]
        private int defaultFov = FovConstants.DefaultValue;

        /// <summary>
        /// The camera used as player eyes.
        /// </summary>
        [SerializeField]
        private Camera eyes;

        /// <summary>
        /// The event emitted when the FOV changes.
        /// </summary>
        public readonly UnityEvent FOVChanges = new();

        private void Awake()
        {
            SetFov(defaultFov);
        }

        /// <summary>
        /// Get the current FOV of the player.
        /// </summary>
        /// <returns>The current FOV of the player</returns>
        public float GetFov()
        {
            return eyes.fieldOfView;
        }

        /// <summary>
        /// Set the FOV of the player.
        /// </summary>
        /// <param name="fov">The new FOV of the player.</param>
        /// <returns>Current instance (fluent pattern).</returns>
        public void SetFov(int fov)
        {
            var oldValue = (int) eyes.fieldOfView;

            eyes.fieldOfView = Mathf.Clamp(fov, FovConstants.MinValue, FovConstants.MaxValue);

            var newValue = (int) eyes.fieldOfView;

            if (newValue != oldValue)
            {
                FOVChanges.Invoke();
            }
        }
    }
}