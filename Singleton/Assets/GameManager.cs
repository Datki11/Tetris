using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

    public TetrisBlock tetrisBlock;
	public BlockController blockController;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {

        Invoke("SpawnTetrisBlocks", 1);
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnTetrisBlocks()
    {
		BlockController controller = Instantiate<BlockController>(blockController);
		controller.transform.position = new Vector3(0, 12, 0);
		TetrisBlock[] block = new TetrisBlock[2];
		for (int i = 0; i < 2; i++)
		{
			block[i] = Instantiate<TetrisBlock>(tetrisBlock);
			block[i].transform.SetParent (controller.transform);
			block[i].transform.localPosition = new Vector3 (-0.5f + i, 0, 0);
			block[i].controller = controller;
		}
		block[0].partner = block[1];
		block[1].partner = block [0];
		controller.blocks = block;
    }
}
