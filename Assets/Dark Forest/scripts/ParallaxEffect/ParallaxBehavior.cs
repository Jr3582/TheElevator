using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AEA
{
    public class ParallaxBehavior : MonoBehaviour
    {
        [SerializeField] private Vector2 _parallaxEffectMultiplier;
        [SerializeField] private Vector2 _autoScrollSpeed = new Vector2(-1f, 0f); // Default to scroll left

        private Transform _cameraTransform;
        private Vector3 _lastCameraPosition;
        private Renderer _renderer;
        private float _backgroundWidth;
        [SerializeField] private GameObject[] _backgroundLayers;

        void Start()
        {
            // Ensure the default auto-scroll speed is set if the field is still at (0, 0)
            if (_autoScrollSpeed == Vector2.zero)
            {
                _autoScrollSpeed = new Vector2(-1f, 0f); // Default to -1 in X direction
            }

            // Get the background width for looping
            if (_backgroundLayers.Length > 0)
            {
                _renderer = _backgroundLayers[0].GetComponent<Renderer>();
                _backgroundWidth = _renderer.bounds.size.x;
            }

            // Cache camera transform and initial position
            _cameraTransform = Camera.main.transform;
            _lastCameraPosition = _cameraTransform.position;
        }

        private void LateUpdate()
        {
            // Process each background layer for parallax effect and auto-scroll
            foreach (var layer in _backgroundLayers)
            {
                ParallaxEffect(layer);
                AutoScroll(layer); // Apply auto-scroll effect
                LoopBackground(layer); // Loop the background layer when it goes off-screen
            }
        }

        private void ParallaxEffect(GameObject layer)
        {
            // Calculate delta movement based on camera movement
            Vector3 deltaMovement = _cameraTransform.position - _lastCameraPosition;

            // Apply the parallax effect by adjusting the position based on multiplier
            layer.transform.position += new Vector3(deltaMovement.x * _parallaxEffectMultiplier.x, deltaMovement.y * _parallaxEffectMultiplier.y);

            // Update the last camera position for next frame
            _lastCameraPosition = _cameraTransform.position;
        }

        private void AutoScroll(GameObject layer)
        {
            // Add auto-scroll to the background
            Vector3 scrollMovement = new Vector3(_autoScrollSpeed.x, _autoScrollSpeed.y, 0f) * Time.deltaTime;
            layer.transform.position += scrollMovement;
        }

        private void LoopBackground(GameObject layer)
        {
            // If the background is moving to the left, we check when it has moved off-screen
            if (layer.transform.position.x <= _cameraTransform.position.x - _backgroundWidth)
            {
                // Reset the background's position to just past the camera view to create a seamless loop
                layer.transform.position = new Vector3(layer.transform.position.x + _backgroundWidth * 2, layer.transform.position.y, layer.transform.position.z);
            }
        }
    }
}
