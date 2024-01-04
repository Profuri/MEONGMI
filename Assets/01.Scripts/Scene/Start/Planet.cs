using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 8f;

    private float _currentRotate;
    
    private void Update()
    {
        transform.Rotate(Vector3.up * (_rotateSpeed * Time.deltaTime));
    }
}
