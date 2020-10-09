using UnityEngine;
/// <summary>
/// Class which rotates player with LookRotation and Slerp
/// </summary>
public class QuaternionRotator : PlayerMove
{
    public QuaternionRotator(Transform transform,
                             float rotateSpeed) : base(transform,
                                                       rotateSpeed)
    {
#if UNITY_EDITOR
        playerMoveDebug = new QuaternionRotateDebug();
#endif
    }

    public override MoveName Name
    {
        get
        {
            return MoveName.Quaternion;
        }
    }

    // TODO it rotates player in world space not in self space
    /// <summary>
    /// Rotates player with LookRotation
    /// </summary>
    /// <remarks>
    /// Works perfectly but rotates player in world space not in local
    /// </remarks>
    /// <param name="lookRotation">Any delta from any input source</param>
    public override void Move(Vector2 lookRotation)
    {
        var worldSpaceRotateTo = CalculateSpeed(lookRotation);

        var vectorizedRotation = transform.InverseTransformDirection(worldSpaceRotateTo);

        if (vectorizedRotation != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(vectorizedRotation, transform.root.up);

            //var lerpedRotation = Quaternion.Slerp(transform.rotation,
            //                                      rotation,
            //                                      Time.deltaTime * moveSpeed);

            //transform.rotation = lerpedRotation;
        }
    }

#if UNITY_EDITOR_WIN || UNITY_EDITOR
    public override void _Move(Vector3 lookRotation)
    {
        transform.Rotate(lookRotation);
    }

    /// <summary>
    /// Used to debug input
    /// </summary>
    private class QuaternionRotateDebug : PlayerMoveDebug
    {
        public override void DrawGizmos(Transform transform, Vector3 lastForward)
        {
            var forward = GetRay(transform.position, lastForward);
            Gizmos.DrawRay(forward);

            var upwards = GetRay(transform.position, transform.root.up);

            Gizmos.DrawRay(upwards);
        }

        private Ray GetRay(Vector3 origin, Vector3 direction)
        {
            return new Ray(origin, direction);
        }
    }
#endif
}
