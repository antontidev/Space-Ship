using InputSamples.Drawing;
using MyBox;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InputSamples.Gestures
{

    /// <summary>
    /// Interface for Zenject
    /// </summary>
    public class IController : MonoBehaviour
    {

    }

    /// <summary>
    /// Controller that interprets takes pointer input from <see cref="PointerInputManager"/> and detects
    /// directional swipes and detects taps.
    /// </summary>
    [RequireComponent(typeof(PointerInputManager))]
    public class GestureController : MonoBehaviour
    {
        private PointerInputManager inputManager;

        // For accessing GestureController without setting up a reference inside the Editor
        public static GestureController Instance { get; private set; }

        // Maximum duration of a press before it can no longer be considered a tap.
        [SerializeField]
        public float maxTapDuration = 0.2f;

        // Maximum distance in screen units that a tap can drift from its original position before
        // it is no longer considered a tap.
        [SerializeField]
        private float maxTapDrift = 5.0f;

        // Maximum duration of a swipe before it is no longer considered to be a valid swipe.
        [SerializeField]
        private float maxSwipeDuration = 0.5f;

        // Minimum distance in screen units that a swipe must move before it is considered a swipe.
        // Note that if this is smaller or equal to maxTapDrift, then it is possible for a user action to be
        // returned as both a swipe and a tap.
        [SerializeField]
        private float minSwipeDistance = 10.0f;

        // How much a swipe should consistently be in the same direction before it is considered a swipe.
        [SerializeField]
        private float swipeDirectionSamenessThreshold = 0.6f;

        /// <summary>
        /// Minimym distance between fingers that a scale must move before event is invoked
        /// </summary>
        [SerializeField]
        private float minScaleDistance = 10.0f;

        public enum LabelType
        {
            TextMeshPro,
            Text
        }

        [Tooltip("Used to select of from label Type")]
        public LabelType labelType;

        [Header("Debug"), SerializeField]
        [Tooltip("When you use TextMeshPro instead of unity Text")]
        [ConditionalField("labelType", false, LabelType.TextMeshPro)]
        public TextMeshProUGUI label;

        [Tooltip("When you use unity Text instead of TextMeshPro")]
        [ConditionalField("labelType", false, LabelType.Text)]
        public Text labelText;

        // Mapping of input IDs to their active gesture tracking objects.
        private readonly Dictionary<int, ActiveGesture> activeGestures = new Dictionary<int, ActiveGesture>();

        /// <summary>
        /// Event fired when the user presses on the screen.
        /// </summary>
        public event Action<SwipeInput> Pressed;

        /// <summary>
        /// Event fired when the user move finger on screen
        /// </summary>
        public event Action<SwipeInput> Dragged;

        /// <summary>
        /// Event fired for every motion (possibly multiple times a frame) of a potential swipe gesture.
        /// </summary>
        public event Action<SwipeInput> PotentiallySwiped;

        /// <summary>
        /// Event fired when a user performs a swipe gesture.
        /// </summary>
        public event Action<SwipeInput> Swiped;

        /// <summary>
        /// Event fired when a user performs a tap gesture, on releasing.
        /// </summary>
        public event Action<TapInput> Tapped;

        /// <summary>
        /// Event fired when a user released his finger
        /// </summary>
        public event Action<SwipeInput> SwipeEnded;

        /// <summary>
        /// Dictionary of current fingers on screen
        /// </summary>
        private readonly Dictionary<int, ActiveGesture> activeFingers = new Dictionary<int, ActiveGesture>();

        /// <summary>
        /// Event fired when a user performs a swipe gesture, on pressed
        /// </summary>
        public event Action<ScaleInput> Scaled;

        private void Awake()
        {
            inputManager = GetComponent<PointerInputManager>();

            Instance = this;
        }

        protected virtual void OnEnable()
        {
            inputManager.Pressed += OnPressed;
            inputManager.Dragged += OnDragged;
            inputManager.Released += OnReleased;
        }

        protected virtual void OnDisable()
        {
            inputManager.Pressed += OnPressed;
            inputManager.Dragged += OnDragged;
            inputManager.Released += OnReleased;
        }

        /// <summary>
        /// Checks whether a given active gesture will be a valid swipe.
        /// </summary>
        private bool IsValidSwipe(ref ActiveGesture gesture)
        {
            return gesture.TravelDistance >= minSwipeDistance &&
                (gesture.StartTime - gesture.EndTime) <= maxSwipeDuration &&
                gesture.SwipeDirectionSameness >= swipeDirectionSamenessThreshold;
        }

        /// <summary>
        /// Checks whether a given active gesture will be a valid two-finger scale swipe
        /// </summary>
        /// <returns>Returns true, if dot multipclication of direction vectors was zero</returns>
        private bool IsValidScale()
        {
            List<Vector2> dir = new List<Vector2>();
            foreach (var element in activeFingers)
            {
                dir.Add(element.Value.GetDirection());
            }

            var mult = Vector2.Dot(dir[0], dir[1]);

            UnityEngine.Debug.Log(mult);
            //Using dot product of two vectors from dictionary
            return false;
        }

        /// <summary>
        /// Checks whether a given active gesture will be a valid tap.
        /// </summary>
        private bool IsValidTap(ref ActiveGesture gesture)
        {
            return gesture.TravelDistance <= maxTapDrift &&
                (gesture.StartTime - gesture.EndTime) <= maxTapDuration;
        }

        private void OnPressed(PointerInput input, double time)
        {
            UnityEngine.Debug.Assert(!activeGestures.ContainsKey(input.InputId));

            var newGesture = new ActiveGesture(input.InputId, input.Position, time);
            activeGestures.Add(input.InputId, newGesture);

            if (activeFingers.Count < 2)
            {
                activeFingers.Add(input.InputId, newGesture);
            }

            DebugInfo(newGesture);

            Pressed?.Invoke(new SwipeInput(newGesture));
        }

        private void OnDragged(PointerInput input, double time)
        {
            if (!activeGestures.TryGetValue(input.InputId, out var existingGesture))
            {
                // Probably caught by UI, or the input was otherwise lost
                return;
            }

            existingGesture.SubmitPoint(input.Position, time);

            Dragged?.Invoke(new SwipeInput(existingGesture));

            if (IsValidSwipe(ref existingGesture))
            {
                PotentiallySwiped?.Invoke(new SwipeInput(existingGesture));
            }
            // There is no arguments because we updated position value before
            if (activeFingers.Count > 2 && IsValidScale())
            {
                Scaled?.Invoke(new ScaleInput(activeFingers));
            }

            DebugInfo(existingGesture);
        }

        private void OnReleased(PointerInput input, double time)
        {
            if (!activeGestures.TryGetValue(input.InputId, out var existingGesture))
            {
                // Probably caught by UI, or the input was otherwise lost
                return;
            }

            activeGestures.Remove(input.InputId);
            activeFingers.Remove(input.InputId);
            existingGesture.SubmitPoint(input.Position, time);

            SwipeEnded?.Invoke(new SwipeInput(existingGesture));

            if (IsValidSwipe(ref existingGesture))
            {
                Swiped?.Invoke(new SwipeInput(existingGesture));
            }

            if (IsValidTap(ref existingGesture))
            {
                Tapped?.Invoke(new TapInput(existingGesture));
            }

            DebugInfo(existingGesture);
        }

        private void DebugInfo(ActiveGesture gesture)
        {
            if (label == null && labelText == null) return;

            var builder = new StringBuilder();

            builder.AppendFormat("ID: {0}", gesture.InputId);
            builder.AppendLine();
            builder.AppendFormat("Start Position: {0}", gesture.StartPosition);
            builder.AppendLine();
            builder.AppendFormat("Position: {0}", gesture.EndPosition);
            builder.AppendLine();
            builder.AppendFormat("Duration: {0}", gesture.EndTime - gesture.StartTime);
            builder.AppendLine();
            builder.AppendFormat("Sameness: {0}", gesture.SwipeDirectionSameness);
            builder.AppendLine();
            builder.AppendFormat("Travel distance: {0}", gesture.TravelDistance);
            builder.AppendLine();
            builder.AppendFormat("Samples: {0}", gesture.Samples);
            builder.AppendLine();
            builder.AppendFormat("Realtime since startup: {0}", Time.realtimeSinceStartup);
            builder.AppendLine();
            builder.AppendFormat("Starting Timestamp: {0}", gesture.StartTime);
            builder.AppendLine();
            builder.AppendFormat("Ending Timestamp: {0}", gesture.EndTime);
            builder.AppendLine();

            labelText.text = builder.ToString();

            if (label != null)
            {
                label.text = builder.ToString();
            }
            else if (labelText != null)
            {
                labelText.text = builder.ToString();
            }

            var worldStart = Camera.main.ScreenToWorldPoint(gesture.StartPosition);
            var worldEnd = Camera.main.ScreenToWorldPoint(gesture.EndPosition);

            worldStart.z += 5;
            worldEnd.z += 5;
        }
    }
}
