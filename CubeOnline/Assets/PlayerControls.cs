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
                    ""name"": ""right_bump"",
                    ""type"": ""Value"",
                    ""id"": ""d0883fa3-d9e4-4212-be18-ca8ff8f6dfe5"",
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
                    ""expectedControlType"": ""Vector2"",
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
                    ""name"": ""actionA"",
                    ""type"": ""Button"",
                    ""id"": ""a3a5ac12-3584-4a85-824b-7f72a10fb4af"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""buttonB"",
                    ""type"": ""Button"",
                    ""id"": ""be7df2b4-d0b2-4a03-9d05-4c267e170a92"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""buttonY"",
                    ""type"": ""Button"",
                    ""id"": ""180d1618-0b16-4394-aeb7-446d4ba44299"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""cross_up"",
                    ""type"": ""Button"",
                    ""id"": ""2c39bfc1-9434-40c6-a1e2-e7808998557f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""cross_down"",
                    ""type"": ""Button"",
                    ""id"": ""7368fb43-9b35-4bf1-b2e1-7b75ad653496"",
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
                    ""id"": ""ada8d87f-0981-4949-87e6-24ae1ca229c9"",
                    ""path"": ""<Mouse>/leftButton"",
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
                    ""name"": ""2D Vector"",
                    ""id"": ""31a3f5ae-7f73-4bee-a50f-782ecd7e8fd9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""rotate_cube"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e2a30ca9-8440-4eb6-922c-4393a90b2853"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""rotate_cube"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""fa403f93-c7dc-48aa-9ee6-3535cfbf0d92"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""rotate_cube"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f6a74a8a-d557-4afd-95c6-8e93590d44f4"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(max=-1)"",
                    ""groups"": """",
                    ""action"": ""rotate_cube"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""aeba5f9d-36a8-4184-8ca6-04eb71ec4a79"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(max=1)"",
                    ""groups"": """",
                    ""action"": ""rotate_cube"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
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
                    ""name"": """",
                    ""id"": ""9b531a20-c39c-482a-9a04-2ab68cdf576c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""actionA"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1104903a-b143-4787-9274-e559573a3ae9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""actionA"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b13b79f0-db9a-49fa-b9e9-acf7303c7a93"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""buttonB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11f7de56-8bf1-4891-bfb5-a7b3125a884f"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""buttonB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""arrow [Keyboard]"",
                    ""id"": ""f7f09fa2-23cc-440d-bc8a-5bec54e523a4"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move_cube"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9490b581-b7d9-498b-b65a-a71140248879"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""move_cube"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0ab08212-f1d4-4044-ab28-77cb9412c4dc"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""move_cube"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cdecc018-4e85-4220-8d4f-07185dfb05e7"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""move_cube"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f8c9ba65-e5d3-49a8-b27e-8879b5208d85"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""04ebec83-e181-477e-bb6c-68d221939b96"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""buttonY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3887261e-c3b8-4a37-a8de-50f15755205c"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""buttonY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a3bfb8b-fb86-4838-930d-ac38cb37ae76"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""buttonY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9234de14-bf0e-401c-bea7-fe2ee3fe3e56"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""cross_up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""929a0a3b-ed04-44b3-b8d2-c8f258bf50f9"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""cross_down"",
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
        m_game_pad_right_bump = m_game_pad.FindAction("right_bump", throwIfNotFound: true);
        m_game_pad_move_cube = m_game_pad.FindAction("move_cube", throwIfNotFound: true);
        m_game_pad_rotate_cube = m_game_pad.FindAction("rotate_cube", throwIfNotFound: true);
        m_game_pad_pause = m_game_pad.FindAction("pause", throwIfNotFound: true);
        m_game_pad_actionA = m_game_pad.FindAction("actionA", throwIfNotFound: true);
        m_game_pad_buttonB = m_game_pad.FindAction("buttonB", throwIfNotFound: true);
        m_game_pad_buttonY = m_game_pad.FindAction("buttonY", throwIfNotFound: true);
        m_game_pad_cross_up = m_game_pad.FindAction("cross_up", throwIfNotFound: true);
        m_game_pad_cross_down = m_game_pad.FindAction("cross_down", throwIfNotFound: true);
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
    private readonly InputAction m_game_pad_right_bump;
    private readonly InputAction m_game_pad_move_cube;
    private readonly InputAction m_game_pad_rotate_cube;
    private readonly InputAction m_game_pad_pause;
    private readonly InputAction m_game_pad_actionA;
    private readonly InputAction m_game_pad_buttonB;
    private readonly InputAction m_game_pad_buttonY;
    private readonly InputAction m_game_pad_cross_up;
    private readonly InputAction m_game_pad_cross_down;
    public struct Game_padActions
    {
        private @PlayerControls m_Wrapper;
        public Game_padActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @left_bump => m_Wrapper.m_game_pad_left_bump;
        public InputAction @right_bump => m_Wrapper.m_game_pad_right_bump;
        public InputAction @move_cube => m_Wrapper.m_game_pad_move_cube;
        public InputAction @rotate_cube => m_Wrapper.m_game_pad_rotate_cube;
        public InputAction @pause => m_Wrapper.m_game_pad_pause;
        public InputAction @actionA => m_Wrapper.m_game_pad_actionA;
        public InputAction @buttonB => m_Wrapper.m_game_pad_buttonB;
        public InputAction @buttonY => m_Wrapper.m_game_pad_buttonY;
        public InputAction @cross_up => m_Wrapper.m_game_pad_cross_up;
        public InputAction @cross_down => m_Wrapper.m_game_pad_cross_down;
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
                @right_bump.started -= m_Wrapper.m_Game_padActionsCallbackInterface.OnRight_bump;
                @right_bump.performed -= m_Wrapper.m_Game_padActionsCallbackInterface.OnRight_bump;
                @right_bump.canceled -= m_Wrapper.m_Game_padActionsCallbackInterface.OnRight_bump;
                @move_cube.started -= m_Wrapper.m_Game_padActionsCallbackInterface.OnMove_cube;
                @move_cube.performed -= m_Wrapper.m_Game_padActionsCallbackInterface.OnMove_cube;
                @move_cube.canceled -= m_Wrapper.m_Game_padActionsCallbackInterface.OnMove_cube;
                @rotate_cube.started -= m_Wrapper.m_Game_padActionsCallbackInterface.OnRotate_cube;
                @rotate_cube.performed -= m_Wrapper.m_Game_padActionsCallbackInterface.OnRotate_cube;
                @rotate_cube.canceled -= m_Wrapper.m_Game_padActionsCallbackInterface.OnRotate_cube;
                @pause.started -= m_Wrapper.m_Game_padActionsCallbackInterface.OnPause;
                @pause.performed -= m_Wrapper.m_Game_padActionsCallbackInterface.OnPause;
                @pause.canceled -= m_Wrapper.m_Game_padActionsCallbackInterface.OnPause;
                @actionA.started -= m_Wrapper.m_Game_padActionsCallbackInterface.OnActionA;
                @actionA.performed -= m_Wrapper.m_Game_padActionsCallbackInterface.OnActionA;
                @actionA.canceled -= m_Wrapper.m_Game_padActionsCallbackInterface.OnActionA;
                @buttonB.started -= m_Wrapper.m_Game_padActionsCallbackInterface.OnButtonB;
                @buttonB.performed -= m_Wrapper.m_Game_padActionsCallbackInterface.OnButtonB;
                @buttonB.canceled -= m_Wrapper.m_Game_padActionsCallbackInterface.OnButtonB;
                @buttonY.started -= m_Wrapper.m_Game_padActionsCallbackInterface.OnButtonY;
                @buttonY.performed -= m_Wrapper.m_Game_padActionsCallbackInterface.OnButtonY;
                @buttonY.canceled -= m_Wrapper.m_Game_padActionsCallbackInterface.OnButtonY;
                @cross_up.started -= m_Wrapper.m_Game_padActionsCallbackInterface.OnCross_up;
                @cross_up.performed -= m_Wrapper.m_Game_padActionsCallbackInterface.OnCross_up;
                @cross_up.canceled -= m_Wrapper.m_Game_padActionsCallbackInterface.OnCross_up;
                @cross_down.started -= m_Wrapper.m_Game_padActionsCallbackInterface.OnCross_down;
                @cross_down.performed -= m_Wrapper.m_Game_padActionsCallbackInterface.OnCross_down;
                @cross_down.canceled -= m_Wrapper.m_Game_padActionsCallbackInterface.OnCross_down;
            }
            m_Wrapper.m_Game_padActionsCallbackInterface = instance;
            if (instance != null)
            {
                @left_bump.started += instance.OnLeft_bump;
                @left_bump.performed += instance.OnLeft_bump;
                @left_bump.canceled += instance.OnLeft_bump;
                @right_bump.started += instance.OnRight_bump;
                @right_bump.performed += instance.OnRight_bump;
                @right_bump.canceled += instance.OnRight_bump;
                @move_cube.started += instance.OnMove_cube;
                @move_cube.performed += instance.OnMove_cube;
                @move_cube.canceled += instance.OnMove_cube;
                @rotate_cube.started += instance.OnRotate_cube;
                @rotate_cube.performed += instance.OnRotate_cube;
                @rotate_cube.canceled += instance.OnRotate_cube;
                @pause.started += instance.OnPause;
                @pause.performed += instance.OnPause;
                @pause.canceled += instance.OnPause;
                @actionA.started += instance.OnActionA;
                @actionA.performed += instance.OnActionA;
                @actionA.canceled += instance.OnActionA;
                @buttonB.started += instance.OnButtonB;
                @buttonB.performed += instance.OnButtonB;
                @buttonB.canceled += instance.OnButtonB;
                @buttonY.started += instance.OnButtonY;
                @buttonY.performed += instance.OnButtonY;
                @buttonY.canceled += instance.OnButtonY;
                @cross_up.started += instance.OnCross_up;
                @cross_up.performed += instance.OnCross_up;
                @cross_up.canceled += instance.OnCross_up;
                @cross_down.started += instance.OnCross_down;
                @cross_down.performed += instance.OnCross_down;
                @cross_down.canceled += instance.OnCross_down;
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
        void OnRight_bump(InputAction.CallbackContext context);
        void OnMove_cube(InputAction.CallbackContext context);
        void OnRotate_cube(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnActionA(InputAction.CallbackContext context);
        void OnButtonB(InputAction.CallbackContext context);
        void OnButtonY(InputAction.CallbackContext context);
        void OnCross_up(InputAction.CallbackContext context);
        void OnCross_down(InputAction.CallbackContext context);
    }
    public interface IKeyboardActions
    {
        void OnA(InputAction.CallbackContext context);
        void OnNewaction1(InputAction.CallbackContext context);
    }
}
