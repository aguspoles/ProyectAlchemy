using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityEffect : MonoBehaviour {

	private Movement movement;
	private float duration;
	private float timer;
	private float num;
	private Rigidbody m_rigidBody;
	private Quaternion m_originalRotation;
	// Use this for initialization
	void Start() {
		timer = 0;
		num = 0;
		movement = gameObject.GetComponent<Movement>();
		m_rigidBody = GetComponent<Rigidbody> ();
		m_originalRotation = transform.rotation;
		transform.position = new Vector3 (transform.position.x, transform.position.y + 5, transform.position.z);
		m_rigidBody.useGravity = false;
	}

	// Update is called once per frame
	void Update() {
		num += 0.2f;
		timer += Time.deltaTime;
		GravMadness ();
		if (timer >= duration)
		{
			Destroy(this);
		}
	}

	public void SetDuration(float dur)
	{
		duration = dur;
	}

	private void GravMadness()
	{
		transform.Rotate (new Vector3 (0, num, 0));
	}

	private void OnDisable()
	{
		m_rigidBody.useGravity = true;
		transform.rotation = m_originalRotation;
	}

	private void OnDestroy()
	{
		m_rigidBody.useGravity = true;
		transform.rotation = m_originalRotation;
	}
}
