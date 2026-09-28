using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float followSpeed = 15.0f;
    [SerializeField] private Vector3 followPosition;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10.0f);

    private void Start()
    {
        followPosition = transform.position;
    }

    private void LateUpdate()
    {
        targetPosition = target.position + offset;

        followPosition = Vector3.Lerp(transform.position, targetPosition,
            followSpeed * Time.deltaTime);

        transform.position = followPosition;
    }
}
