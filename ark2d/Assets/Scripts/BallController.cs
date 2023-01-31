using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
	[SerializeField]
	private float ballSpeed = 150;

	private Rigidbody2D ball;

	private Rigidbody2D GetRigidbody2D()
	{
		return GetComponent<Rigidbody2D>();
	}

	private Vector2 positionUpdate()
	{
		return Vector2.up * ballSpeed;
	}
	
	public void RestartScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	
	// ball and racket interaction

	float hitFactor(Vector2 ballPosition,
		Vector2 racketPosition,
		float racketWidth)
	{
		return (ballPosition.x - racketPosition.x) / racketWidth;
	}	
	
	void OnCollisionEnter2D(Collision2D racket)
	{

		if (racket.gameObject.name == "Racket")
		{
			// Calculate hit Factor
			float x = hitFactor(transform.position,
							  racket.transform.position,
							  racket.collider.bounds.size.x);

			// Calculate direction, set length to 1
			Vector2 direction = new Vector2(x, 1).normalized;

			// Set Velocity with dir * speed
			ball.velocity = direction * ballSpeed;
		}

	}

	// Start is called before the first frame update
	void Start()
	{
		// Debug.Log(SceneManager.sceneCount);
		ball = GetRigidbody2D();
		ball.velocity = positionUpdate();
		// Debug.Log(blockCounter());
	}

	void FixedUpdate()
	{
		// game over
		if (ball.transform.position.y < -100)
		{
			RestartScene();
		}
	}
}
