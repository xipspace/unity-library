using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockController : MonoBehaviour
{
	public void RestartScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	private int blockCounter()
	{
		return GameObject.FindGameObjectsWithTag("Block").Length;
	}

	void OnCollisionEnter2D()
	{


		if (blockCounter() == 1)
		{
			RestartScene();
		}
		Destroy(gameObject);
	}

	void Start()
	{

		// Debug.Log(blockCounter());

	}

}
