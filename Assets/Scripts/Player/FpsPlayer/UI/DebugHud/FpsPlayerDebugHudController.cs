using System.Globalization;
using Koi.FpsPlayer.Player.FpsPlayer.Components;
using Koi.FpsPlayer.Player.FpsPlayer.Constants;
using UnityEngine;
using UnityEngine.UIElements;

namespace Koi.FpsPlayer.Player.FpsPlayer.UI.DebugHud
{
    /// <summary>
    /// A component to manage the debug HUD of an FPS player.
    /// </summary>
    public class FpsPlayerDebugHudController : MonoBehaviour
    {
        /// <summary>
        /// The UI document that contains the debug HUD.
        /// </summary>
        [SerializeField]
        private UIDocument uiDocument;

        /// <summary>
        /// The component that manages the FOV of the player.
        /// </summary>
        [SerializeField]
        private FpsPlayerFov fpsPlayerFov;

        /// <summary>
        /// The UI element that contains root container.
        /// </summary>
        private VisualElement _rootContainer;

        /// <summary>
        /// The UI element that displays the FOV value.
        /// </summary>
        private Label _fov;

        private void Start()
        {
            _rootContainer = uiDocument.rootVisualElement.Q<VisualElement>("RootContainer");
            _fov = _rootContainer.Q<VisualElement>("Fov").Q<Label>("FovValue");

            ConfigureFovSlider();

            fpsPlayerFov.FOVChanges.AddListener(UpdateFov);

            UpdateFov();
        }

        /// <summary>
        /// Configure the FOV slider to allow user to change the FOV of the player in game.
        /// </summary>
        private void ConfigureFovSlider()
        {
            var fovSlider = _rootContainer.Q<Slider>("Slider");

            fovSlider.lowValue = FovConstants.MinValue;
            fovSlider.highValue = FovConstants.MaxValue;
            fovSlider.value = fpsPlayerFov.GetFov();

            fovSlider.RegisterValueChangedCallback(OnFovSliderChanges);
        }

        /// <summary>
        /// Update the FOV value displayed in the debug HUD.
        /// </summary>
        private void UpdateFov()
        {
            _fov.text = GetPlayerFov();
        }

        /// <summary>
        /// Method to call when FOV slider changes.
        /// </summary>
        /// <param name="e">The event.</param>
        private void OnFovSliderChanges(ChangeEvent<float> e)
        {
            SetPlayerFov((int) e.newValue);
        }

        /// <summary>
        /// Get the player FOV.
        /// </summary>
        /// <returns>The player FOV.</returns>
        private string GetPlayerFov()
        {
            return fpsPlayerFov.GetFov().ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Set the player FOV.
        /// </summary>
        /// <param name="value">The new player FOV.</param>
        private void SetPlayerFov(int value)
        {
            fpsPlayerFov.SetFov(value);
        }
    }
}