using UnityEngine;
using System.Collections;


public class SmoothFollow : MonoBehaviour
{
    public Transform target
    {
        get { return (Player.Instance == null) ? transform : Player.Instance.transform; }
    }

	public float smoothDampTime = 0.2f;
	[HideInInspector]
	public new Transform transform;
	public Vector3 cameraOffset;
    public static Vector3 offsetZone;
	public bool useFixedUpdate = false;
    //public bool setOffsetOnStart = true;      // Disabled to auto set player to center for game jam use case

    //public Vector2 boundsMin;
    //public Vector2 boundsMax;
    

	private Vector3 _smoothDampVelocity;
	private Vector3 cameraWorldRect;
	private Vector3 cameraBotLeft;
	private Vector3 cameraTopRight;


	void Awake()
	{
		transform = gameObject.transform;
    }
    void UpdateTarget() {
    }

    void Start() {
		cameraTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 10));
		cameraBotLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10));

		cameraWorldRect = cameraTopRight - cameraBotLeft;

		UpdateTarget();
        //if(setOffsetOnStart) {
        //    cameraOffset = target.position - transform.position;
        cameraOffset = new Vector3(0, 0, -transform.position.z);
        //}

    }

    void LateUpdate()
	{
		if( !useFixedUpdate ) {
            UpdateTarget();
            updateCameraPosition();
        }
	}


	private void OnDrawGizmos() {
		Vector3 cameraBotLeft;
		Vector3 cameraTopRight;

		cameraTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 10));
		cameraBotLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10));
		Vector3 cameraTopLeft = new Vector3(cameraBotLeft.x, cameraTopRight.y, 0);
		Vector3 cameraBotRight = new Vector3(cameraTopRight.x, cameraBotLeft.y, 0);

		cameraWorldRect = cameraTopRight - cameraBotLeft;

		Debug.DrawLine(cameraTopRight, cameraTopLeft, Color.magenta);
		Debug.DrawLine(cameraTopRight, cameraBotRight, Color.magenta);
		Debug.DrawLine(cameraBotLeft, cameraTopLeft, Color.magenta);
		Debug.DrawLine(cameraBotLeft, cameraBotRight, Color.magenta);

	}

	void FixedUpdate()
	{
		if( useFixedUpdate ) {
            UpdateTarget();
            updateCameraPosition();
        }
	}
    

    void updateCameraPosition()
    {
        Vector3 combinedOffset = offsetZone + cameraOffset;

        //GLHelper.DrawLine(Vector3.left * 100 + startingPlayerHeight * Vector3.up, Vector3.right * 100 + startingPlayerHeight * Vector3.up);
        if (target == null) { return; }

        Vector3 cameraTarget = target.position;
        //if (target.position.y < cameraTopRight.y) {
        //	cameraTarget.y = startingPlayerHeight;
        //}

        transform.position = Vector3.SmoothDamp(transform.position, cameraTarget - combinedOffset, ref _smoothDampVelocity, smoothDampTime);

        if (transform.position.x + cameraWorldRect.x / 2 > LevelSetting.Instance.rightBound)
        {
            Vector3 position = transform.position;
            position.x = LevelSetting.Instance.rightBound - cameraWorldRect.x / 2;
            transform.position = position;
        }
        if (transform.position.x - cameraWorldRect.x / 2 < LevelSetting.Instance.leftBound)
        {
            Vector3 position = transform.position;
            position.x = LevelSetting.Instance.leftBound + cameraWorldRect.x / 2;
            transform.position = position;
        }

        if (transform.position.y + cameraWorldRect.y / 2 > LevelSetting.Instance.topBound)
        {
            Vector3 position = transform.position;
            position.y = LevelSetting.Instance.topBound - cameraWorldRect.y / 2;
            transform.position = position;
        }
        if (transform.position.y - cameraWorldRect.y / 2 < LevelSetting.Instance.bottomBound)
        {
            Vector3 position = transform.position;
            position.y = LevelSetting.Instance.bottomBound + cameraWorldRect.y / 2;
            transform.position = position;
        }

    }
}
