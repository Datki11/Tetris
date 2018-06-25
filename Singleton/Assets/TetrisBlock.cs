using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour {

    Color color;
	Renderer render;
	public Rigidbody rb;
	public BlockController controller;
	public TetrisBlock partner;

	public float speed = 4f;
	public float speedQuick = 8f; //How fast the block goes down when the down arrow key is pressed
	public float speedFalling = 10f; //How fast the blocks fall when going down at the initial speed

	bool landed = false;

	// Use this for initialization
	void Start () {

		render = GetComponent<Renderer>();
		rb = GetComponent<Rigidbody> ();

        //Picks a random color to assign to its color variable.
        int a = (int)Random.Range(0, 5);
        switch (a)
        {
            case 0: color = Color.blue; break;
            case 1: color = Color.red; break;
            case 2: color = Color.yellow; break;
            case 3: color = Color.green; break;
            case 4: color = Color.magenta; break;
            default: break;
        }

        //Changes the material color of this object to its color variable.
		render.material.color = color;

		rb.velocity = new Vector3 (0, -speed, 0);


		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!landed)
		{
			if (transform.position.y < 1)
			{
				transform.position = new Vector3 (transform.position.x, 1, 0);
				Landed ();

			}

		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (!landed)
		{
			GameObject other = collision.gameObject;
			transform.position = other.transform.position + new Vector3 (0, 1, 0);
			Landed ();
		}
	}

	public void Landed()
	{
		rb.velocity = Vector3.zero;
		landed = true;
		rb.constraints = RigidbodyConstraints.FreezeAll;
		if (controller != null)
			controller.DestroyCustom ();
		if (partner.rb.velocity.y == -speed)
			partner.rb.velocity = new Vector3 (0f, -speedFalling, 0f);
		if (partner.landed)
		{
			GameManager.instance.Invoke ("SpawnTetrisBlocks", 1);
		}
	}

	public void Move(Direction.Directions direction)
	{
		if (direction == Direction.Directions.Left)
		{
			Collider[] collider = Physics.OverlapBox (transform.position + Vector3.left, Vector3.one / 4, Quaternion.identity);
			if (collider.Length == 0)
				controller.transform.Translate (Vector3.left);
		}
		if (direction == Direction.Directions.Right)
		{
			Collider[] collider = Physics.OverlapBox (transform.position + Vector3.right, Vector3.one / 4, Quaternion.identity);
			if (collider.Length == 0)
				controller.transform.Translate (Vector3.right);
		}
	}
}
