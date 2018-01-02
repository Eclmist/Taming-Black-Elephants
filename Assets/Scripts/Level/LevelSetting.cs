using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetting : MonoBehaviour {

	public static LevelSetting Instance;
    public static MoodMusic.Mood lastKnownMood = MoodMusic.Mood.None;

    public float leftBound;
    public float rightBound;
    public float topBound;
    public float bottomBound;

    public MoodMusic.Mood levelMood;
    public Vector2 debugSpawnPoint;

    // Use this for initialization
    void Awake () {
		if (Instance != null) {
			Destroy(this);
			Debug.LogError("There are multiple levelsettings in this scene!!!!");
		}


		Instance = this;
	}
	
	// Update is called once per frame
	void OnDrawGizmos () {
		Vector3 top = Vector3.up * 100;
		Vector3 bot = Vector3.down * 100;
		Debug.DrawLine(top + leftBound * Vector3.right, bot + leftBound * Vector3.right, Color.blue);
        Debug.DrawLine(top + rightBound * Vector3.right, bot + rightBound * Vector3.right, Color.red);
        Debug.DrawLine(new Vector3(leftBound, topBound, 0), new Vector3(rightBound, topBound, 0), Color.blue);
        Debug.DrawLine(new Vector3(leftBound, bottomBound, 0), new Vector3(rightBound, bottomBound, 0), Color.red);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(debugSpawnPoint, 0.2F);
    }

    private void OnDestroy() {
		Instance = null;
	}
}
