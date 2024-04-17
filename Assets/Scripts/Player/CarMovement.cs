using System.Collections;
using Road;
using Scripts;
using UnityEngine;

namespace Player
{
    public class CarMovement : MonoBehaviour
    {
        [Header("Car")] 
        [SerializeField] private float moveSpeed = 10;
        [SerializeField] private float maxSpeed = 50;
        [SerializeField] private float minSpeed = 10;

        [SerializeField] private GameObject[] wheels;

        private Rigidbody _rb;
        private float _horizontalInput;
        private float _verticalInput;
        private float _currentSpeed = 10;
        private Vector3 _moveDirection;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();

            StartCoroutine(MoveWheels());
        }

        private void Update()
        {
            if (GlobalGameStates.IsGameStopped) return;
            
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _moveDirection = Vector3.right * _horizontalInput;
            _rb.AddForce(_moveDirection.normalized * moveSpeed * 2f, ForceMode.Force);

            SpeedControl();
            RoadEventManager.ChangePlayerSpeed(_currentSpeed);
        }

        private void FixedUpdate()
        {
            if (GlobalGameStates.IsGameStopped) return;
            
            _verticalInput = Input.GetAxisRaw("Vertical");
            _currentSpeed += _verticalInput * (Time.deltaTime * moveSpeed);
            _currentSpeed = Mathf.Clamp(_currentSpeed, minSpeed, maxSpeed);
        }

        private IEnumerator MoveWheels()
        {
            while (true)
            {
                foreach (GameObject wheel in wheels)
                {
                    wheel.transform.Rotate(new Vector3(180 * Time.deltaTime * _currentSpeed, 0, 0));
                }

                yield return null;
            }
        }

        private void SpeedControl()
        {
            Vector3 oldVelocity = _rb.velocity;
            Vector3 flatVel = new Vector3(oldVelocity.x, 0f, oldVelocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
            }

            Quaternion oldRot = _rb.rotation;
            _rb.rotation = new Quaternion(oldRot.x, oldRot.y, 0f, oldRot.w);
        }
    }
}