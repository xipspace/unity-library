using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketController : MonoBehaviour
{
	[SerializeField]
	private float racketSpeed = 150;

	private Rigidbody2D racket;

	private Rigidbody2D GetRigidbody2D()
	{
		return GetComponent<Rigidbody2D>();
	}

	private float mapControl()
	{
		return Input.GetAxisRaw("Horizontal");
	}

	private Vector2 racketControl()
	{
		return Vector2.right * racketSpeed;
	}


	// Start is called before the first frame update
	void Start()
	{
		racket = GetRigidbody2D();
	}


	void FixedUpdate()
	{

		// Get Horizontal Input
		// float h = Input.GetAxisRaw("Horizontal");
		// Racket Control
		racket.velocity = racketControl() * mapControl();

	}

}
