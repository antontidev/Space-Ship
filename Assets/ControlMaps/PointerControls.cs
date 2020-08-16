// GENERATED AUTOMATICALLY FROM 'Assets/ControlMaps/PointerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace InputSamples.Controls
{
    public class @PointerControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PointerControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PointerControls"",
    ""maps"": [
        {
            ""name"": ""pointer"",
            ""id"": ""3c570214-6b14-44a9-8e61-3e4dc9ac469f"",
            ""actions"": [
                {
                    ""name"": ""point"",
                    ""type"": ""Value"",
                    ""id"": ""4d610105-c5af-439c-8a02-4f1976d8da67"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""b5560351-2a50-40d4-84b9-2f1b5ce1c8ec"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""MouseAndPen"",
                    ""id"": ""6503119b-11d7-4b61-9465-8ab83699a36c"",
                    ""path"": ""PointerInput"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""point"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""contact"",
                    ""id"": ""33cce31d-cbc6-4899-8781-9ab727534e60"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Mouse"",
                    ""action"": ""point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""contact"",
                    ""id"": ""418f64e8-359e-4b05-8869-c8a1165e44d9"",
                    ""path"": ""<Pen>/tip"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Pen"",
                    ""action"": ""point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""position"",
                    ""id"": ""60ae03ce-5f16-4763-9102-400558002a23"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Mouse"",
                    ""action"": ""point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""position"",
                    ""id"": ""4d35537c-6a23-4f4a-bad4-eaeed0a67248"",
                    ""path"": ""<Pen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Pen"",
                    ""action"": ""point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""tilt"",
                    ""id"": ""e14524a1-8951-4672-98aa-49fda32a7548"",
                    ""path"": ""<Pen>/tilt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Pen"",
                    ""action"": ""point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""pressure"",
                    ""id"": ""ce154ce8-174d-4bbf-adb7-4f8634b86a24"",
                    ""path"": ""<Pen>/pressure"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Pen"",
                    ""action"": ""point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""twist"",
                    ""id"": ""8f871c6e-49c6-4e0b-9d0f-65263a53ac84"",
                    ""path"": ""<Pen>/twist"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Pen"",
                    ""action"": ""point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Touch0"",
                    ""id"": ""f5819de4-b7e5-4745-9d93-2b45e9dad897"",
                    ""path"": ""PointerInput"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""point"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""contact"",
                    ""id"": ""a0baebac-8b22-4db8-9cf9-7ba8c4d8aab0"",
                    ""path"": ""<Touchscreen>/touch0/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Touch"",
                    ""action"": ""point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""position"",
                    ""id"": ""79b4615b-9534-4aa7-af27-90a442add4bc"",
                    ""path"": ""<Touchscreen>/touch0/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Touch"",
                    ""action"": ""point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""radius"",
                    ""id"": ""07bde460-f80a-45ae-823d-db51f6bda4bb"",
                    ""path"": ""<Touchscreen>/touch0/radius"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Touch"",
                    ""action"": ""point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""pressure"",
                    ""id"": ""b72c4336-bd65-457f-bacd-cf9933eb2fc7"",
                    ""path"": ""<Touchscreen>/touch0/pressure"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Touch"",
                    ""action"": ""point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""inputId"",
                    ""id"": ""6be63793-ef04-469d-ac12-779245b71ba9"",
                    ""path"": ""<Touchscreen>/touch0/touchId"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Touch"",
                    ""action"": ""point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Touch1"",
                    ""id"": ""448ef65c-b779-4014-bdc4-1cf793b11223"",
                    ""path"": ""PointerInput"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""point"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""contact"",
                    ""id"": ""a7e9f275-8e72-4221-b947-e1d972629254"",
                    ""path"": ""<Touchscreen>/touch1/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Touch"",
                    ""action"": ""point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""position"",
                    ""id"": ""64552938-2fd4-4fd9-aab5-c6873527f9fa"",
                    ""path"": ""<Touchscreen>/touch1/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Touch"",
                    ""action"": ""point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""radius"",
                    ""id"": ""ccf25457-9cab-4137-bfb6-d9a3f07cdf02"",
                    ""path"": ""<Touchscreen>/touch1/radius"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Touch"",
                    ""action"": ""point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""pressure"",
                    ""id"": ""1cd9d53e-4d19-4f1d-a4cf-68df808d026a"",
                    ""path"": ""<Touchscreen>/touch1/pressure"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Touch"",
                    ""action"": ""point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""inputId"",
                    ""id"": ""583bf8d0-0d65-4042-9496-64d4f3b7b7e2"",
                    ""path"": ""<Touchscreen>/touch1/touchId"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Touch"",
                    ""action"": ""point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""081643ad-f669-4d6e-b239-34894e269a24"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": ""Press"",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""Touch;Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Pen"",
            ""bindingGroup"": ""Pen"",
            ""devices"": [
                {
                    ""devicePath"": ""<Pen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // pointer
            m_pointer = asset.FindActionMap("pointer", throwIfNotFound: true);
            m_pointer_point = m_pointer.FindAction("point", throwIfNotFound: true);
            m_pointer_Look = m_pointer.FindAction("Look", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // pointer
        private readonly InputActionMap m_pointer;
        private IPointerActions m_PointerActionsCallbackInterface;
        private readonly InputAction m_pointer_point;
        private readonly InputAction m_pointer_Look;
        public struct PointerActions
        {
            private @PointerControls m_Wrapper;
            public PointerActions(@PointerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @point => m_Wrapper.m_pointer_point;
            public InputAction @Look => m_Wrapper.m_pointer_Look;
            public InputActionMap Get() { return m_Wrapper.m_pointer; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PointerActions set) { return set.Get(); }
            public void SetCallbacks(IPointerActions instance)
            {
                if (m_Wrapper.m_PointerActionsCallbackInterface != null)
                {
                    @point.started -= m_Wrapper.m_PointerActionsCallbackInterface.OnPoint;
                    @point.performed -= m_Wrapper.m_PointerActionsCallbackInterface.OnPoint;
                    @point.canceled -= m_Wrapper.m_PointerActionsCallbackInterface.OnPoint;
                    @Look.started -= m_Wrapper.m_PointerActionsCallbackInterface.OnLook;
                    @Look.performed -= m_Wrapper.m_PointerActionsCallbackInterface.OnLook;
                    @Look.canceled -= m_Wrapper.m_PointerActionsCallbackInterface.OnLook;
                }
                m_Wrapper.m_PointerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @point.started += instance.OnPoint;
                    @point.performed += instance.OnPoint;
                    @point.canceled += instance.OnPoint;
                    @Look.started += instance.OnLook;
                    @Look.performed += instance.OnLook;
                    @Look.canceled += instance.OnLook;
                }
            }
        }
        public PointerActions @pointer => new PointerActions(this);
        private int m_MouseSchemeIndex = -1;
        public InputControlScheme MouseScheme
        {
            get
            {
                if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
                return asset.controlSchemes[m_MouseSchemeIndex];
            }
        }
        private int m_PenSchemeIndex = -1;
        public InputControlScheme PenScheme
        {
            get
            {
                if (m_PenSchemeIndex == -1) m_PenSchemeIndex = asset.FindControlSchemeIndex("Pen");
                return asset.controlSchemes[m_PenSchemeIndex];
            }
        }
        private int m_TouchSchemeIndex = -1;
        public InputControlScheme TouchScheme
        {
            get
            {
                if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
                return asset.controlSchemes[m_TouchSchemeIndex];
            }
        }
        public interface IPointerActions
        {
            void OnPoint(InputAction.CallbackContext context);
            void OnLook(InputAction.CallbackContext context);
        }
    }
}
