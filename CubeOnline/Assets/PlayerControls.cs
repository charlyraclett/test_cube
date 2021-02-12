// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""game_pad"",
            ""id"": ""b3c4342f-1868-46f7-ac67-c7ed99b67f35"",
            ""actions"": [
                {
                    ""name"": ""left_bump"",
                    ""type"": ""Button"",
                    ""id"": ""f4e80b66-aa3e-4755-ac47-96abab64ace1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""move_cube"",
                    ""type"": ""Value"",
                    ""id"": ""95a580c9-c69f-4e12-bc59-9cdab5f2b814"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""rotate_cube"",
                    ""type"": ""Value"",
                    ""id"": ""3040395e-cc11-43a0-b2b4-41e7312d77c5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""a4990350-b79b-4a60-a088-6a3be0a92ba3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""pause"",
                    ""type"": ""Button"",
                    ""id"": ""848dc41a-0ce1-44ae-8914-7d63e227798d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""WASD"",
                    ""type"": ""Button"",
                    ""id"": ""4b1f4596-ee1a-435f-9cfa-32d8c06f4eae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""right_bump"",
                    ""type"": ""Value"",
                    ""id"": ""d0883fa3-d9e4-4212-be18-ca8ff8f6dfe5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""211ad6f0-5236-4a32-afbb-86a7bd0533fb"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""left_bump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26d45a59-8e81-4e18-ad16-d872c1292148"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""left_bump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""928e0d64-9a86-4332-8bff-d2879b6d25f4"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move_cube"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""870e3d02-3c33-499e-abae-e2462f8aa4ea"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""rotate_cube"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14740479-aec5-46dd-a1a0-d5aff81a667c"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""68daef14-eaf5-437f-81ec-02d2e1f3d995"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""wasd"",
                    ""id"": ""f7f09fa2-23cc-440d-bc8a-5bec54e523a4"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9490b581-b7d9-498b-b65a-a71140248879"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0ab08212-f1d4-4044-ab28-77cb9412c4dc"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cdecc018-4e85-4220-8d4f-07185dfb05e7"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f8c9ba65-e5d3-49a8-b27e-8879b5208d85"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5a481b6d-0207-4fd7-bb0f-a37465cee9fe"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move_cube"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2821efd3-8b38-420d-a50e-259fbc374f69"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move_cube"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fe1a42f8-b935-45fd-a22e-3ff50b6a0407"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move_cube"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6d546f93-c847-4b69-a3ef-d2e2510fec63"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move_cube"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""cefbe91b-d66d-43e7-8442-957b8977f46e"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""right_bump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""keyboard"",
            ""id"": ""1af32109-6616-4873-b15d-76c4e1e7e56d"",
            ""actions"": [
                {
                    ""name"": ""A"",
                    ""type"": ""Button"",
                    ""id"": ""af9a0f78-27dc-4d8f-8e13-5f65618130d5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""New action1"",
                    ""type"": ""Value"",
                    ""id"": ""f8785dd9-1142-4a53-b7a8-0678351d18a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": []
        }
    ],
    ""controlSchemes"": []
}");
        // game_pad
        m_game_pad = asset.FindActionMap("game_pad", throwIfNotFound: true);
        m_game_pad_left_bump = m_game_pad.FindAction("left_bump", throwIfNotFound: true);
        m_game_pad_move_cube = m_game_pad.FindAction("move_cube", throwIfNotFound: true);
        m_game_pad_rotate_cube = m_game_pad.FindAction("rotate_cube", throwIfNotFound: true);
        m_game_pad_Newaction = m_game_pad.FindAction("New action", throwIfNotFound: true);
        m_game_pad_pause = m_game_pad.FindAction("pause", throwIfNotFound: true);
        m_game_pad_WASD = m_game_pad.FindAction("WASD", throwIfNotFound: true);
        m_game_pad_right_bump = m_game_pad.FindAction("right_bump", throwIfNotFound: true);
        // keyboard
        m_keyboard = asset.FindActionMap("keyboard", throwIfNotFound: true);
        m_keyboard_A = m_keyboard.FindAction("A", throwIfNotFound: true);
        m_keyboard_Newaction1 = m_keyboard.FindAction("New action1", throwIfNotFound: true);
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

    // game_pad
    private readonly InputActionMap m_game_pad;
    private IGame_padActions m_Game_padActionsCallbackInterface;
    private readonly InputAction m_game_pad_left_bump;
    private readonly InputAction m_game_pad_move_cube;
    private readonly InputAction m_game_pad_rotate_cube;
    private readonly InputAction m_game_pad_Newaction;
    private readonly InputAction m_game_pad_pause;
    private readonly InputAction m_game_pad_WASD;
    private readonly InputAction m_game_pad_right_bump;
    public struct Game_padActions
    {
        private @PlayerControls m_Wrapper;
        public Game_padActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @left_bump => m_Wrapper.m_game_pad_left_bump;
        public InputAction @move_cube => m_Wrapper.m_game_pad_move_cube;
        public InputAction @rotate_cube => m_Wrapper.m_game_pad_rotate_cube;
        public InputAction @Newaction => m_Wrapper.m_game_pad_Newaction;
        public InputAction @pause => m_Wrapper.m_game_pad_pause;
        public InputAction @WASD => m_Wrapper.m_game_pad_WASD;
        public InputAction @right_bump => m_Wrapper.m_game_pad_right_bump;
        public InputActionMap Get() { return m_Wrapper.m_game_pad; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Game_padActions set) { return set.Get(); }
        public void SetCallbacks(IGame_padActions instance)
        {
            if (m_Wrapper.m_Game_padActionsCallbackInterface != null)
            {
                @left_bump.started -= m_Wrapper.m_Game_padActionsCallbackInterface.OnLeft_bump;
                @left_bump.performed -= m_Wrapper.m_Game_padActionsCallbackInterface.OnLeft_bump;
                @left_bump.canceled -= m_Wrapper.m_Game_padActionsCallbackInterface.OnLeft_bump;
                @move_cube.started -= m_Wrapper.m_Game_padActionsCallbackInterface.OnMove_cube;
                @move_cube.performed -= m_Wrapper.m_Game_padActionsCallbackInterface.OnMove_cube;
                @move_cube.canceled -= m_Wrapper.m_Game_padActionsCallbackInterface.OnMove_cube;
                @rotate_cube.started -= m_Wrapper.m_Game_padActionsCallbackInterface.OnRotate_cube;
                @rotate_cube.performed -= m_Wrapper.m_Game_padActionsCallbackInterface.OnRotate_cube;
                @rotate_cube.canceled -= m_Wrapper.m_Game_padActionsCallbackInterface.OnRotate_cube;
                @Newaction.started -= m_Wrapper.m_Game_padActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_Game_padActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_Game_padActionsCallbackInterface.OnNewaction;
                @pause.started -= m_Wrapper.m_Game_padActionsCallbackInterface.OnPause;
                @pause.performed -= m_Wrapper.m_Game_padActionsCallbackInterface.OnPause;
                @pause.canceled -= m_Wrapper.m_Game_padActionsCallbackInterface.OnPause;
                @WASD.started -= m_Wrapper.m_Game_padActionsCallbackInterface.OnWASD;
                @WASD.performed -= m_Wrapper.m_Game_padActionsCallbackInterface.OnWASD;
                @WASD.canceled -= m_Wrapper.m_Game_padActionsCallbackInterface.OnWASD;
                @right_bump.started -= m_Wrapper.m_Game_padActionsCallbackInterface.OnRight_bump;
                @right_bump.performed -= m_Wrapper.m_Game_padActionsCallbackInterface.OnRight_bump;
                @right_bump.canceled -= m_Wrapper.m_Game_padActionsCallbackInterface.OnRight_bump;
            }
            m_Wrapper.m_Game_padActionsCallbackInterface = instance;
            if (instance != null)
            {
                @left_bump.started += instance.OnLeft_bump;
                @left_bump.performed += instance.OnLeft_bump;
                @left_bump.canceled += instance.OnLeft_bump;
                @move_cube.started += instance.OnMove_cube;
                @move_cube.performed += instance.OnMove_cube;
                @move_cube.canceled += instance.OnMove_cube;
                @rotate_cube.started += instance.OnRotate_cube;
                @rotate_cube.performed += instance.OnRotate_cube;
                @rotate_cube.canceled += instance.OnRotate_cube;
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
                @pause.started += instance.OnPause;
                @pause.performed += instance.OnPause;
                @pause.canceled += instance.OnPause;
                @WASD.started += instance.OnWASD;
                @WASD.performed += instance.OnWASD;
                @WASD.canceled += instance.OnWASD;
                @right_bump.started += instance.OnRight_bump;
                @right_bump.performed += instance.OnRight_bump;
                @right_bump.canceled += instance.OnRight_bump;
            }
        }
    }
    public Game_padActions @game_pad => new Game_padActions(this);

    // keyboard
    private readonly InputActionMap m_keyboard;
    private IKeyboardActions m_KeyboardActionsCallbackInterface;
    private readonly InputAction m_keyboard_A;
    private readonly InputAction m_keyboard_Newaction1;
    public struct KeyboardActions
    {
        private @PlayerControls m_Wrapper;
        public KeyboardActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @A => m_Wrapper.m_keyboard_A;
        public InputAction @Newaction1 => m_Wrapper.m_keyboard_Newaction1;
        public InputActionMap Get() { return m_Wrapper.m_keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardActions instance)
        {
            if (m_Wrapper.m_KeyboardActionsCallbackInterface != null)
            {
                @A.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnA;
                @A.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnA;
                @A.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnA;
                @Newaction1.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnNewaction1;
                @Newaction1.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnNewaction1;
                @Newaction1.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnNewaction1;
            }
            m_Wrapper.m_KeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @A.started += instance.OnA;
                @A.performed += instance.OnA;
                @A.canceled += instance.OnA;
                @Newaction1.started += instance.OnNewaction1;
                @Newaction1.performed += instance.OnNewaction1;
                @Newaction1.canceled += instance.OnNewaction1;
            }
        }
    }
    public KeyboardActions @keyboard => new KeyboardActions(this);
    public interface IGame_padActions
    {
        void OnLeft_bump(InputAction.CallbackContext context);
        void OnMove_cube(InputAction.CallbackContext context);
        void OnRotate_cube(InputAction.CallbackContext context);
        void OnNewaction(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnWASD(InputAction.CallbackContext context);
        void OnRight_bump(InputAction.CallbackContext context);
    }
    public interface IKeyboardActions
    {
        void OnA(InputAction.CallbackContext context);
        void OnNewaction1(InputAction.CallbackContext context);
    }
}
