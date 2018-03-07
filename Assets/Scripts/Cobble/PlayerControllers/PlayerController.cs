using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cobble.Core;
using Cobble.Lib;

namespace Cobble.PlayerControllers {
    public class PlayerController : MonoBehaviour {
        [Header("Controlls")] [Tooltip("The speed at which the player moves per second.")]
        public float MovementSpeed = 10.0f;

        [Tooltip("The movement speed multiplier applied when sprinting.")]
        public float SprintingMultiplier = 2.0f;

        [Tooltip("The speed at which the camera will rotate.")]
        public float RotationSpeed = 5.0f;

        [Tooltip("The velocity that the player jumps with.")]
        public float JumpVelocity = 5.0f;

        [Tooltip("The LayerMask used to determine if the player is on the ground.")]
        public LayerMask TerrainMask = -1;

        private Vector3 _movementNormal;

        [SerializeField] private bool _isOnGround;

        private bool _isJumping;

        private float _maxY;

        private Rigidbody _rigidbody;

        [Header("Other")] [Tooltip("The camera attached to the player")] [SerializeField]
        private Camera _playerCamera;

        [SerializeField] private GuiManager _guiManager;

        private float _cameraVertRot;

        private void Start() {
            _movementNormal = transform.up;
            _rigidbody = GetComponent<Rigidbody>();
            if (!_playerCamera)
                _playerCamera = GetComponentInChildren<Camera>();
            if (!_guiManager)
                _guiManager = FindObjectOfType<GuiManager>();
        }


        // Update is called once per frame
        private void Update() {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, 1.25f, TerrainMask))
                _movementNormal = hit.normal;
            else
                _movementNormal = transform.up;

            Debug.DrawRay(transform.position, _movementNormal * 5f, Color.red);
            if (!Input.GetButtonDown("Pause")) return;
            if (_guiManager.GetCurrentGuiScreen() == GuiScreen.None)
                _guiManager.Open(GuiScreen.PauseUi);
            else
                _guiManager.GoBack();
        }

        private void FixedUpdate() {
            if (!_isOnGround || GameManager.IsPaused || GuiManager.IsMouseFree()) return;

            GetInput();
        }


        private void GetInput() {
            var sprint = Input.GetAxis("Sprint");
            var speed = Mathf.Lerp(MovementSpeed, MovementSpeed * SprintingMultiplier, sprint);
            var x = Input.GetAxis("Horizontal") * speed;
            var baseJump = (Input.GetButtonDown("Jump") && _isOnGround ? 1.0f : 0.0f) * JumpVelocity;
            var y = Mathf.Lerp(baseJump, baseJump * SprintingMultiplier, sprint);
            var z = Input.GetAxis("Vertical") * speed;

            var vel = transform.right * x + transform.forward * z;

            _rigidbody.velocity = Vector3.ProjectOnPlane(vel, _movementNormal);
            if (y > 0)
                _rigidbody.AddRelativeForce(0, y, 0, ForceMode.VelocityChange);

//        Debug.Log(x + " | " + y + " | " + z + " | " + _rigidbody.velocity + " | " + _rigidbody.velocity.magnitude);

            var horizontal = Input.GetAxis("Mouse X") * RotationSpeed;
            if (Math.Abs(horizontal) < 0.1f)
                horizontal = Input.GetAxis("Left Joystick X") * RotationSpeed;
            var vertical = Input.GetAxis("Mouse Y") * RotationSpeed;
            if (Math.Abs(vertical) < 0.1f)
                vertical = -Input.GetAxis("Left Joystick Y") * RotationSpeed;

            transform.Rotate(0, horizontal, 0);

            _cameraVertRot += -vertical;
            _cameraVertRot = Mathf.Clamp(_cameraVertRot, -90, 90);
            var localAngles = _playerCamera.transform.localEulerAngles;
            localAngles.x = _cameraVertRot;
            _playerCamera.transform.localEulerAngles = localAngles;
            _rigidbody.rotation = transform.rotation;
        }

        private void OnCollisionEnter(Collision other) {
            if ((TerrainMask.value & 1 << other.gameObject.layer) != 0) {
                _isOnGround = true;
            }
        }

        private void OnCollisionStay(Collision other) {
//        if ((TerrainMask.value & 1 << other.gameObject.layer) != 0) {
//            _movementNormal = FindBestContactNormal(other.contacts);
//        }
        }

        private void OnCollisionExit(Collision other) {
            if ((TerrainMask.value & 1 << other.gameObject.layer) != 0)
                _isOnGround = false;
        }

        private Vector3 FindBestContactNormal(ContactPoint[] contactPoints) {
            var bestContact = -transform.up.normalized;
            foreach (var contact in contactPoints) {
                bestContact = contact.normal;
                var oldDot = Vector3.Dot(transform.up.normalized, bestContact.normalized);
                var newDot = Vector3.Dot(transform.up.normalized, contact.normal);
                if (newDot > oldDot)
                    bestContact = contact.normal;
            }

            return bestContact == -transform.up.normalized ? transform.up.normalized : bestContact;
        }
    }
}