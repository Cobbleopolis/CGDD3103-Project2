using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cobble.Core;
using Cobble.Lib;

namespace Cobble.PlayerControllers {
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class FpsController : MonoBehaviour {
        [Header("Movement Settings")] [SerializeField] [Tooltip("The speed at which the player moves per second.")]
        private float _movementSpeed = 10.0f;

        [Tooltip("The speed at which the camera will rotate.")] [SerializeField]
        private float _rotationSpeed = 5.0f;

        [Tooltip("The movement speed multiplier applied when sprinting.")] [SerializeField]
        private float _runMultiplier = 2.0f;

        [Tooltip("The velocity that the player jumps with.")] [SerializeField]
        private float _jumpForce = 50.0f;

        [SerializeField] private AnimationCurve _slopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f),
            new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));

        [HideInInspector] public float CurrentTargetSpeed = 10.0f;


        [SerializeField]
        [Header("Advanced Settings")]
        [Tooltip("Distance for checking if the controller is grounded (0.01f seems to work best for this).")]
        private float _groundCheckDistance = 0.01f;

        [SerializeField] [Tooltip("Distance to check to help the player stick to the ground.")]
        private float _stickToGroundHelperDistance = 0.5f; // 

        [SerializeField] [Tooltip("Rate at which the controller comes to a stop when there is no input.")]
        private float _slowDownRate = 10.0f;

        [SerializeField] [Tooltip("Can the user control the direction that is being moved in the air.")]
        private bool _airControl;

        [SerializeField]
        [Tooltip("Reduce the radius by that ratio to avoid getting stuck in wall (a value of 0.1f is nice).")]
        private float _shellOffset = 0.1f;

        [SerializeField] [Header("Other Settings")] [Tooltip("The camera attached to the player.")]
        private Camera _playerCamera;

        private Rigidbody _rigidBody;
        private CapsuleCollider _capsuleCollider;
        private float _yRotation;
        private Vector3 _groundContactNormal;
        private bool _jump, _previouslyGrounded, _jumping, _isGrounded, _running;
        private GuiManager _guiManager;


        public Vector3 Velocity {
            get { return _rigidBody.velocity; }
        }

        public bool Grounded {
            get { return _isGrounded; }
        }

        public bool Jumping {
            get { return _jumping; }
        }

        public bool Running {
            get { return _running; }
        }

        private void Start() {
            _rigidBody = GetComponent<Rigidbody>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _guiManager = FindObjectOfType<GuiManager>();
            if (!_playerCamera)
                _playerCamera = GetComponentInChildren<Camera>();
        }

        private void Update() {
            RotateView();

            if (Input.GetButtonDown("Jump") && !_jump)
                _jump = true;

            if (!Input.GetButtonDown("Pause")) return;
            if (_guiManager.GetCurrentGuiScreen() == GuiScreen.None)
                _guiManager.Open(GuiScreen.PauseUi);
            else
                _guiManager.GoBack();
        }

        private void FixedUpdate() {
            if (GameManager.IsPaused) return;
            GroundCheck();
            var input = GetInput();

            if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) &&
                (_airControl || _isGrounded)) {
                var desiredMove = transform.forward * input.y + transform.right * input.x;
                desiredMove = Vector3.ProjectOnPlane(desiredMove, _groundContactNormal).normalized *
                              CurrentTargetSpeed * SlopeMultiplier();
                if (_rigidBody.velocity.sqrMagnitude <
                    CurrentTargetSpeed * CurrentTargetSpeed)
                    _rigidBody.AddForce(desiredMove, ForceMode.Impulse);
            }

            if (_isGrounded) {
                _rigidBody.drag = _slowDownRate;

                if (_jump) {
                    _rigidBody.drag = 0f;
                    _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, 0f, _rigidBody.velocity.z);
                    _rigidBody.AddForce(new Vector3(0f, _jumpForce, 0f), ForceMode.Impulse);
                    _jumping = true;
                }

                if (!_jumping && Mathf.Abs(input.x) < float.Epsilon && Mathf.Abs(input.y) < float.Epsilon &&
                    _rigidBody.velocity.magnitude < 1f) {
                    _rigidBody.Sleep();
                }
            } else {
                _rigidBody.drag = 0f;
                if (_previouslyGrounded && !_jumping) {
                    StickToGroundHelper();
                }
            }

            _jump = false;
        }

        private void RotateView() {
            //avoids the mouse looking if the game is effectively paused
            if (GameManager.IsPaused) return;

            // get the rotation before it's changed
            var oldYRotation = transform.eulerAngles.y;

            var horizontal = Input.GetAxis("Mouse X") * _rotationSpeed;
            if (Math.Abs(horizontal) < float.Epsilon)
                horizontal = Input.GetAxis("Right Joystick X") * _rotationSpeed;
            var vertical = Input.GetAxis("Mouse Y") * _rotationSpeed;
            if (Math.Abs(vertical) < float.Epsilon)
                vertical = -Input.GetAxis("Right Joystick Y") * _rotationSpeed;

            transform.Rotate(0, horizontal, 0);

            _yRotation += -vertical;
            _yRotation = Mathf.Clamp(_yRotation, -90, 90);
            var localAngles = _playerCamera.transform.localEulerAngles;
            localAngles.x = _yRotation;
            _playerCamera.transform.localEulerAngles = localAngles;
            _rigidBody.rotation = transform.rotation;

            if (!Grounded && !_airControl) return;
            // Rotate the rigidbody velocity to match the new direction that the character is looking
            var velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
            _rigidBody.velocity = velRotation * _rigidBody.velocity;
        }

        private Vector2 GetInput() {
            var input = new Vector2 {
                x = Input.GetAxis("Horizontal"),
                y = Input.GetAxis("Vertical")
            };
            UpdateDesiredTargetSpeed(input);
            return input;
        }

        private void UpdateDesiredTargetSpeed(Vector2 input) {
            if (input == Vector2.zero) return;

            var sprint = Input.GetAxis("Sprint");
            CurrentTargetSpeed = Mathf.Lerp(_movementSpeed, _movementSpeed * _runMultiplier, sprint);

            _running = sprint > float.Epsilon;
        }

        private float SlopeMultiplier() {
            var angle = Vector3.Angle(_groundContactNormal, Vector3.up);
            return _slopeCurveModifier.Evaluate(angle);
        }

        private void StickToGroundHelper() {
            RaycastHit hitInfo;
            if (!Physics.SphereCast(transform.position, _capsuleCollider.radius * (1.0f - _shellOffset),
                Vector3.down, out hitInfo,
                _capsuleCollider.height / 2f - _capsuleCollider.radius +
                _stickToGroundHelperDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore)) return;
            if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < 85f) {
                _rigidBody.velocity = Vector3.ProjectOnPlane(_rigidBody.velocity, hitInfo.normal);
            }
        }

        private void GroundCheck() {
            _previouslyGrounded = _isGrounded;
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, _capsuleCollider.radius * (1.0f - _shellOffset),
                Vector3.down, out hitInfo,
                (_capsuleCollider.height - _capsuleCollider.radius) / 2f + _groundCheckDistance, Physics.AllLayers,
                QueryTriggerInteraction.Ignore)) {
                _isGrounded = true;
                _groundContactNormal = hitInfo.normal;
            } else {
                _isGrounded = false;
                _groundContactNormal = Vector3.up;
            }

            if (!_previouslyGrounded && _isGrounded && _jumping) {
                _jumping = false;
            }
        }
    }
}