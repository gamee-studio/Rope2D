namespace Rope2d
{
    using UnityEngine;

    public class FingerSlicer : MonoBehaviour
    {
        public TrailRenderer trail;
        public LayerMask ropeLayer;
        public float fingerRadius = 0.1f;

        public bool isActivated { get; private set; }
        public bool isSlicing { get; private set; }
        private static RaycastHit2D[] cachedHits = new RaycastHit2D[16];

        public void Activate() { isActivated = true; }

        public void Deactivate() { isActivated = false; }


        public void StartSlicing()
        {
            isSlicing = true;
            trail.enabled = false;
            trail.enabled = true;
            trail.emitting = true;
        }

        public void StopSlicing()
        {
            isSlicing = false;
            if (trail) trail.emitting = false;
        }

        public void MoveTo(Vector3 position, bool? overrideSlicing = null)
        {
            bool slice = overrideSlicing ?? isSlicing;
            if (slice)
            {
                SliceTo(position);
            }
            else
            {
                transform.position = position;
            }
        }

        public void SliceTo(Vector3 position)
        {
            var currentPosition = transform.position;
            if (position == currentPosition) return;

            if (isActivated)
            {
                var dir = position - currentPosition;
                var dist = dir.magnitude;
                dir /= dist;

                var hitCount = Physics2D.CircleCastNonAlloc(currentPosition,
                    fingerRadius,
                    dir,
                    cachedHits,
                    dist,
                    ropeLayer.value);
                if (hitCount > 0)
                {
                    for (int i = 0; i < hitCount; i++)
                    {
                        var hit = cachedHits[i];
                        var ropeNode = hit.collider.GetComponentInParent<RopeNode>();
                        if (ropeNode)
                        {
                            ropeNode.CutAt(hit.point);
                            break;
                        }
                    }
                }
            }

            transform.position = position;
        }
    }
}