using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
// using UnityEngine.InputSystem;
/// <summary>
/// A static class for general helpful methods
/// </summary>

namespace _Scripts
{

    public static class Helpers
    {
        private static Transform _playerTransform;
        public static Transform PlayerTransform
        {
            get
            {
                if (_playerTransform == null) _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                return _playerTransform;
            }
        }


        private static Camera _camera;
        public static Camera Camera
        {
            get
            {
                if (_camera == null) _camera = Camera.main;
                return _camera;
            }
        }

        /// <summary>
        /// Take a Transform component to look at the main camera
        /// </summary>
        public static void LookAtMainCamera(Transform transform)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.transform.position);
        }

        /// <summary>
        /// Get the layer index from the layer name
        /// </summary>
        public static int GetLayerByName(string layerName)
        {
            int enemyLayer = 1 << LayerMask.NameToLayer(layerName);
            return enemyLayer;
        }

        /// <summary>
        /// Get the position of the canvas element
        /// </summary>
        public static Vector2 GetWorldPositionOFCanvasElement(RectTransform element)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera, out var result);
            return result;
        }

        public static Vector3 GetMouseInWorldPosition()
        {
#if ENABLE_INPUT_SYSTEM
            var mousePos = Mouse.current.position.ReadValue();
#else
            var mousePos = Input.mousePosition;
#endif
            Plane plane = new Plane(Vector3.up, 0);
            Vector3 mouseInWorldPosition = Vector3.zero;

            Ray ray = Camera.ScreenPointToRay(mousePos);
            if (plane.Raycast(ray, out var distance))
            {
                mouseInWorldPosition = ray.GetPoint(distance);
            }
            return mouseInWorldPosition;
        }

        private static PointerEventData _eventDataCurrentPosition;
        private static List<RaycastResult> _results;
        public static bool IsOverUi()
        {
#if ENABLE_INPUT_SYSTEM
            PlayerInput input = GameObject.FindObjectOfType<PlayerInput>();
            if (input)
            {
                var inputAction = input.GetDevice<Pointer>();
                _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = inputAction.position.ReadValue() };
            }
#else
                _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
#endif

            _results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_eventDataCurrentPosition, _results);
            return _results.Count > 0;
        }
    }

}