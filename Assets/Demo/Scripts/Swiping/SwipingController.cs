using System;
using System.Collections.Generic;
using InputSamples.Gestures;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace InputSamples.Demo.Swiping
{
    public class SwipingController : MonoBehaviour
    {
        private LayerMask tapLayer;
        /// <summary>
        /// Reference to gesture input manager.
        /// </summary>
        [SerializeField]
        private GestureController gestureController;

        /// <summary>
        /// Minimum cosine of angle between swipe and bin in order to target the given bin.
        /// </summary>
        [SerializeField]
        private float minimumCosineForBinTargetting = 0.75f;

        [SerializeField]
        public FreeLookAddOn addOn;

        /// <summary>
        /// Mapping of input IDs to 'grabbed' trash.
        /// </summary>
        private readonly Dictionary<int, Trash> swipeMapping = new Dictionary<int, Trash>();

        [SerializeField]
        public Camera cachedCamera;
        private int castLayerMask;
        private float screenUnitsToWorldUnits;

        private Dictionary<string, ShipPart> cachedShipParts;

        protected virtual void Awake()
        {
            castLayerMask = LayerMask.GetMask("Default");

            tapLayer = LayerMask.GetMask("Modules");

            cachedShipParts = new Dictionary<string, ShipPart>();

            // Calculate transformation factor from screen units to world units
            if (!cachedCamera.orthographic)
            {
                Debug.LogError("Swiping controller only supports orthographic camera");
            }
            else
            {
                screenUnitsToWorldUnits = (cachedCamera.orthographicSize * 2) / Screen.height;
            }
        }

        private void OnEnable()
        {
            gestureController.PotentiallySwiped += OnDragged;
            gestureController.Swiped += OnSwiped;
            gestureController.Pressed += OnPressed;
            gestureController.Tapped += OnTapped;
            TouchSimulation.Enable();
        }

        protected virtual void OnDisable()
        {
            gestureController.PotentiallySwiped -= OnDragged;
            gestureController.Swiped -= OnSwiped;
            gestureController.Pressed -= OnPressed;
            gestureController.Tapped -= OnTapped;
        }

        private void OnTapped(TapInput input)
        {
            var ray = cachedCamera.ScreenPointToRay(input.PressPosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, tapLayer.value))
            {
                var gameObj = hit.transform.gameObject;

                ShipPart shipPart;
                if (cachedShipParts.ContainsKey(gameObj.name))
                {
                    shipPart = cachedShipParts[gameObj.name];
                }
                else {
                    shipPart = hit.transform.gameObject.GetComponent<ShipPart>();

                    cachedShipParts[gameObj.name] = shipPart;
                }

                shipPart.onClick?.Invoke(gameObj);
            }
        }

        private void OnSwiped(SwipeInput input)
        {
            // Try find a grabbed trash for this swipe
            Trash swipedTrash;

            if (!swipeMapping.TryGetValue(input.InputId, out swipedTrash))
            {
                return;
            }

            swipeMapping.Remove(input.InputId);

            // Launch trash at target if it hasn't been launched yet
            if (swipedTrash == null ||
                swipedTrash.HasFlung)
            {
                return;
            }

            // Find bin in swipe direction

        }

        private void OnPressed(SwipeInput input)
        {
            // Make sure that the swipe mapping doesn't contain this swipe
            swipeMapping.Remove(input.InputId);

            // Try also find grabbed trash on first press
            Vector2 worldCurrent = cachedCamera.ScreenToWorldPoint(input.EndPosition);

            // Try find trash to grab for this touch
            Collider2D collider = Physics2D.OverlapPoint(worldCurrent, castLayerMask);
            if (collider != null)
            {
                // Try find a Trash object on this hit component
                var trash = collider.GetComponent<Trash>();
                if (trash != null &&
                    !trash.HasFlung)
                {
                    // Remember that this swipe went over this object
                    swipeMapping[input.InputId] = trash;
                }
            }
        }

        private void OnDragged(SwipeInput input)
        {
            // If this input's already picked up some trash, ignore this
            if (swipeMapping.ContainsKey(input.InputId))
            {
                return;
            }

            Vector2 worldPrevious = cachedCamera.ScreenToWorldPoint(input.PreviousPosition);
            Vector2 worldCurrent = cachedCamera.ScreenToWorldPoint(input.EndPosition);

            //Try find trash to grab for this swipe
            RaycastHit2D hit = Physics2D.Linecast(worldPrevious, worldCurrent, castLayerMask);
            if (hit.collider != null)
            {
                // Try find a Trash object on this hit component
                var trash = hit.collider.GetComponent<Trash>();
                if (trash != null &&
                    !trash.HasFlung)
                {
                    // Remember that this swipe went over this object
                    swipeMapping[input.InputId] = trash;
                }
            }
        }
    }
}
