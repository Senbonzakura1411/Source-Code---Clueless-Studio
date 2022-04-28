using UnityEngine;

namespace PlayerScripts
{
    [RequireComponent(typeof(Camera))]
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform focus;

        [SerializeField] private Vector2 orbitAngles = new Vector2(45f, 0f);

        [SerializeField, Range(-89f, 89f)] private float minVerticalAngle = -30f, maxVerticalAngle = 75f;

        [SerializeField, Range(1f, 360f)] private float rotationSpeed = 90f;

        [SerializeField, Range(1f, 20f)] private float offset = 5f;

        [SerializeField, Min(0f)] private float focusRadius = 1f;

        [SerializeField, Range(0f, 1f)] private float focusCentering = 0.5f;
        
        [SerializeField] private LayerMask obstructionMask = -1;
        
        private Vector3 CameraHalfExtends {
            get {
                Vector3 halfExtends;
                halfExtends.y = _camera.nearClipPlane * Mathf.Tan(0.5f * Mathf.Deg2Rad * _camera.fieldOfView);
                halfExtends.x = halfExtends.y * _camera.aspect;
                halfExtends.z = 0f;
                return halfExtends;
            }
        }
        
        private Camera _camera;
        
        private PlayerInput _input;

        private Vector3 _focusPoint;

        private void OnValidate()
        {
            if (maxVerticalAngle < minVerticalAngle)
            {
                maxVerticalAngle = minVerticalAngle;
            }
        }

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _input = focus.GetComponent<PlayerInput>();
            _focusPoint = focus.position;
            transform.localRotation = Quaternion.Euler(orbitAngles);
            OnValidate();
        }

        private void LateUpdate()
        {
            UpdateFocusPoint();
            Quaternion lookRotation;

            if (ManualRotation())
            {
                ConstrainAngles();
                lookRotation = Quaternion.Euler(orbitAngles);
            }
            else
            {
                lookRotation = transform.localRotation;
            }

            Vector3 lookDirection = lookRotation * Vector3.forward;
            Vector3 lookPosition = _focusPoint - lookDirection * offset;
            
            Vector3 rectOffset = lookDirection * _camera.nearClipPlane;
            Vector3 rectPosition = lookPosition + rectOffset;
            Vector3 castFrom = focus.position;
            Vector3 castLine = rectPosition - castFrom;
            float castDistance = castLine.magnitude;
            Vector3 castDirection = castLine / castDistance;


            if (Physics.BoxCast(castFrom, CameraHalfExtends, castDirection, out RaycastHit hit, lookRotation,
                castDistance, obstructionMask))
            {
                rectPosition = castFrom + castDirection * hit.distance;
                lookPosition = rectPosition - rectOffset;
            }
            
            transform.SetPositionAndRotation(lookPosition, lookRotation);
        }

        private void UpdateFocusPoint()
        {
            Vector3 targetPoint = focus.position;

            if (!(focusRadius > 0f)) return;
            float distance = Vector3.Distance(targetPoint, _focusPoint);
            var t = 1f;
            if (distance > 0.01f && focusCentering > 0f)
            {
                t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
            }

            if (distance > focusRadius)
            {
                t = Mathf.Min(t, focusRadius / distance);
            }

            _focusPoint = Vector3.Lerp(targetPoint, _focusPoint, t);
        }

        private bool ManualRotation()
        {
            var input = _input.MouseAxesInputVector;
            
            const float e = 0.001f;

            if (!(input.x < -e) && !(input.x > e) && !(input.y < -e) && !(input.y > e)) return false;
            orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
            return true;
        }

        private void ConstrainAngles()
        {
            orbitAngles.x =
                Mathf.Clamp(orbitAngles.x, minVerticalAngle, maxVerticalAngle);

            if (orbitAngles.y < 0f)
            {
                orbitAngles.y += 360f;
            }
            else if (orbitAngles.y >= 360f)
            {
                orbitAngles.y -= 360f;
            }
        }
    }
}