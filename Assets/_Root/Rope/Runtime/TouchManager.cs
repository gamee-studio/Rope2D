using Rope2d;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    #region Trail Eff

    public float distanceFromCamera = 5;
    public LayerMask lmTouch;
    public Camera thisCamera;
    [SerializeField] private TrailRenderer slicerPrefab;

    #endregion

    private TrailRenderer _trail;
    private Vector3 _mousePos;

    private void Start()
    {
        if (_trail == null)
        {
            _trail = Instantiate(slicerPrefab);
        }

        _trail.Clear();
        _trail.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _trail.Clear();
            _cutOn = true;
            _currentMouse = _oldMouse = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _trail.Clear();
            _trail.gameObject.SetActive(false);

            _cutOn = false;
        }

        if (_cutOn)
        {
            _currentMouse = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(_oldMouse, _currentMouse - _oldMouse);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, (_currentMouse - _oldMouse).magnitude, lmTouch);
            _oldMouse = _currentMouse;
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Rope"))
                {
                    var ropeNode = hit.collider.gameObject.GetComponent<RopeNode>();
                    if (ropeNode)
                    {
                        ropeNode.CutAt(hit.point);
                    }
                }
            }

            _trail.gameObject.SetActive(true);
            MoveTrailToCursor(Input.mousePosition);
        }
    }

    void MoveTrailToCursor(Vector3 screenPosition)
    {
        _trail.transform.position = thisCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, distanceFromCamera));
    }


    private bool _cutOn;
    private Vector3 _oldMouse;
    private Vector3 _currentMouse;
}