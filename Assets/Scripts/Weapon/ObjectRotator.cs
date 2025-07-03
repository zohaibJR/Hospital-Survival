using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public float rotationSpeed = 50f;

    public enum RotationAxis { X, Y, Z }
    public RotationAxis rotationAxis = RotationAxis.Y;

    void Update()
    {
        Vector3 rotationVector = Vector3.zero;

        switch (rotationAxis)
        {
            case RotationAxis.X:
                rotationVector = Vector3.right;
                break;
            case RotationAxis.Y:
                rotationVector = Vector3.up;
                break;
            case RotationAxis.Z:
                rotationVector = Vector3.forward;
                break;
        }

        transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
    }
}
