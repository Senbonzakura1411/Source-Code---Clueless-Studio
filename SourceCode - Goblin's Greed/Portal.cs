using UnityEngine;

public class Portal : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 5f;

    private void Update() => transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
}
