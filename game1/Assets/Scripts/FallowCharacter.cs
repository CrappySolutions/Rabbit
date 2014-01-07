using UnityEngine;
using System.Collections;

public class FallowCharacter : MonoBehaviour {
	public Camera mainCamera;
	public GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		mainCamera.transform.position = new Vector3(player.transform.position.x, mainCamera.transform.position.y,transform.position.z);
	}
}
