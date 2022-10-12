using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _smoothTime = 0.25f;
    [SerializeField] Vector3 _offset;
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        FollowSmoothed(_target, _offset, _smoothTime);
    }

    // Smoothly follows the target. The smoothTime default (0) = no smoothing
    private void FollowSmoothed(Transform target, Vector3 offset, float smoothTime = 0)
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.position = smoothedPosition;
    }
}
