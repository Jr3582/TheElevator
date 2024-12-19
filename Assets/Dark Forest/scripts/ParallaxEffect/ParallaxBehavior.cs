using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AEA
{
    public class ParallaxBehavior : MonoBehaviour
    {
        [SerializeField] private Vector2 _parallaxEffectMultiplier;
        [SerializeField] private Vector2 _autoScrollSpeed = new Vector2(-1f, 0f);

        private Transform _cameraTransform;
        private Vector3 _lastCameraPosition;
        private Renderer _renderer;
        private float _backgroundWidth;
        [SerializeField] private GameObject[] _backgroundLayers;

        void Start()
        {
            if (_autoScrollSpeed == Vector2.zero)
            {
                _autoScrollSpeed = new Vector2(-1f, 0f);
            }

            if (_backgroundLayers.Length > 0)
            {
                _renderer = _backgroundLayers[0].GetComponent<Renderer>();
                _backgroundWidth = _renderer.bounds.size.x;
            }

            _cameraTransform = Camera.main.transform;
            _lastCameraPosition = _cameraTransform.position;
        }

        private void LateUpdate()
        {
            foreach (var layer in _backgroundLayers)
            {
                ParallaxEffect(layer);
                AutoScroll(layer);
                LoopBackground(layer);
            }
        }

        private void ParallaxEffect(GameObject layer)
        {
            Vector3 deltaMovement = _cameraTransform.position - _lastCameraPosition;

            layer.transform.position += new Vector3(deltaMovement.x * _parallaxEffectMultiplier.x, deltaMovement.y * _parallaxEffectMultiplier.y);

            _lastCameraPosition = _cameraTransform.position;
        }

        private void AutoScroll(GameObject layer)
        {
            Vector3 scrollMovement = new Vector3(_autoScrollSpeed.x, _autoScrollSpeed.y, 0f) * Time.deltaTime;
            layer.transform.position += scrollMovement;
        }

        private void LoopBackground(GameObject layer)
        {
            if (layer.transform.position.x <= _cameraTransform.position.x - _backgroundWidth)
            {
                layer.transform.position = new Vector3(layer.transform.position.x + _backgroundWidth * 2, layer.transform.position.y, layer.transform.position.z);
            }
        }
    }
}
