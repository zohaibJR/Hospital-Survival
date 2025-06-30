using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class LockTransform : MonoBehaviour
{
    public Vector3 lockedPosition;
    public Vector3 lockedRotation;

    private bool valuesInitialized = false;

    void Awake()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            // Capture position/rotation from Editor
            lockedPosition = transform.position;
            lockedRotation = transform.eulerAngles;
            valuesInitialized = true;
        }
#endif
    }

    void Start()
    {
#if UNITY_EDITOR
        if (Application.isPlaying && !valuesInitialized)
        {
            // In case values weren't set in edit mode
            lockedPosition = transform.position;
            lockedRotation = transform.eulerAngles;
        }
#endif
    }

    void LateUpdate()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            transform.position = lockedPosition;
            transform.eulerAngles = lockedRotation;
        }
#endif
    }
}
