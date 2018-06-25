using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

	public TetrisBlock[] blocks;
	public int rotation = 0;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if (transform.position.x > -2)
			{
				blocks [0].Move (Direction.Directions.Left);
			}
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			if (transform.position.x < 3)
			{
				blocks [1].Move (Direction.Directions.Right);

			}
		}
		if (Input.GetKeyDown (KeyCode.DownArrow))
		{
			for (int i = 0; i < 2; i++)
				blocks [i].rb.velocity = new Vector3 (0f, -blocks [i].speedQuick, 0f);
			DestroyCustom ();
		}
		if (Input.GetKeyDown (KeyCode.Z))
		{
			rotation = Rotate(rotation);
		}
			
	}

	public void DestroyCustom()
	{
		transform.DetachChildren ();
		Destroy (this.gameObject);
	}

	public int Rotate(int rotation)
	{
		Collider[] collider;
		switch (rotation) {
		case 0:
			blocks [0].transform.localPosition = new Vector3 (0, 1, 0);
			blocks [1].transform.localPosition = new Vector3 (0, 0, 0);
			rotation = 90;
			return rotation;
		case 90:
			collider = Physics.OverlapBox (blocks [0].transform.position + Vector3.right, Vector3.one / 4, Quaternion.identity);
			if (collider.Length == 0) {
				blocks [0].transform.localPosition = new Vector3 (1, 1, 0);
				blocks [1].transform.localPosition = new Vector3 (0, 1, 0);
				rotation = 180;
			}
			return rotation;
		case 180:
			collider = Physics.OverlapBox (blocks [0].transform.position + Vector3.down, Vector3.one / 4, Quaternion.identity);
			if (collider.Length == 0) {
				blocks [0].transform.localPosition = new Vector3 (1, 0, 0);
				blocks [1].transform.localPosition = new Vector3 (1, 1, 0);
				rotation = 180;
			}
			return rotation;
		case 270:
			collider = Physics.OverlapBox (blocks [0].transform.position + Vector3.left, Vector3.one / 4, Quaternion.identity);
			if (collider.Length == 0) {
				blocks [0].transform.localPosition = new Vector3 (0, 0, 0);
				blocks [1].transform.localPosition = new Vector3 (1, 0, 0);
				rotation = 180;
			}
			return rotation;
		default:
			return rotation;
		}
	}
}
