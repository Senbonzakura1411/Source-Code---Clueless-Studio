using UnityEngine;

namespace Player
{
    public class PlayerRotation : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();
    

        private void Update()
        {
            var pointToLook = GetComponent<PlayerInput>().WorldXZPositionVector;
            var startRotation = transform.rotation;
            var finalRotation = Quaternion.Lerp(startRotation, Quaternion.LookRotation(pointToLook), 1f);
            _rigidbody.MoveRotation(finalRotation);
        }
    }
}

