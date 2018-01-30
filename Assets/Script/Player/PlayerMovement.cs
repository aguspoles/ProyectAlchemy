using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement {

    [SerializeField]
    float m_dashSpeed;
    [SerializeField]
    float m_coolDownTime;
    [SerializeField]
    float m_turnSpeed;
    [SerializeField]
    float ghostWalkDuration;
    [SerializeField]
    float ghostWalkSpeed;

    [SerializeField]
    SkinnedMeshRenderer playerMesh;

    Rigidbody m_playerRigidBody;
    Vector3 m_movement;
    bool m_coolDown;
    float m_time;
    float m_horizontal;
    float m_vertical;

    bool m_IsAttacking;
    bool m_ghostWalkActivated;

    float m_actualSpeed;

    Animator anim;
    Transform m_MousePos;
	GameObject DashEffect;

    Shader texShader;
    Shader holoShader;

    // Use this for initialization
    void Awake () {
		m_playerRigidBody = GetComponent<Rigidbody> ();
        anim = GetComponentInChildren<Animator>();
		m_coolDown = false;
        m_IsAttacking = false;
        m_ghostWalkActivated = false;
        DashEffect = transform.Find ("DashEffect").gameObject;
    }

    override protected void Start()
    {
        m_MousePos = GameObject.Find("MousePoint").transform;
        m_actualSpeed = speed;

        texShader = Shader.Find("Unlit/Texture");
        holoShader = Shader.Find("Unlit/SpecialFX/Cool Hologram");
    }

    private void FixedUpdate()
    {
        if (!LevelStartingPoint.GetInstance().IsFinished() || LevelEndingPoint.GetInstance().isFinishing())
            return;

        Move();
    }
    public override void SpecificMovement () {
        if(m_IsAttacking)
            transform.LookAt(new Vector3(m_MousePos.position.x,transform.position.y, m_MousePos.position.z)); 

		m_horizontal = Input.GetAxis ("Horizontal");
		m_vertical = Input.GetAxis ("Vertical");
        anim.SetFloat("Horizontal", m_horizontal);
        anim.SetFloat("Vertical", m_vertical);

        if(!m_IsAttacking)
            Move(m_horizontal, m_vertical);
		
		CheckDash (m_horizontal, m_vertical);
	}

	void Move(float h, float v)
	{
		m_movement.Set (h, 0f, v);
		m_movement = m_movement.normalized * m_actualSpeed * Time.fixedDeltaTime;
		m_playerRigidBody.MovePosition (transform.position + m_movement);
	
        Vector3 dir = m_movement.normalized;

        if (dir.magnitude != 0)
            this.transform.forward = Vector3.Lerp(this.transform.forward, dir, Time.fixedDeltaTime * m_turnSpeed);
	}

	void CheckDash(float horizontal, float vertical)
	{
		if (Input.GetKey (KeyCode.LeftShift) && m_coolDown) 
		{
            StartGhostWalk();

            /*
			m_movement = transform.position + (new Vector3 (m_horizontal, 0f, m_vertical) * m_dashSpeed * Time.fixedDeltaTime);
			m_playerRigidBody.MovePosition(m_movement);

			m_coolDown = false;
			DashEffect.SetActive (true);
			StartCoroutine (Wait ());
            */
        }
	
		m_time += Time.deltaTime;
		if (m_time >= m_coolDownTime) 
		{
			m_coolDown = true;
			m_time = 0;
		}
	}

    public void IsAttacking(bool value)
    {
        m_IsAttacking = value;
    }

	IEnumerator Wait(){
		yield return new WaitForSeconds (1);
		DashEffect.SetActive (false);
	}

    IEnumerator GhostWalk()
    {
        yield return new WaitForSeconds(ghostWalkDuration);
        StopGhostWalk();
    }

    private void StartGhostWalk()
    {
        m_ghostWalkActivated = true;
        m_actualSpeed = ghostWalkSpeed;
        playerMesh.material.shader = holoShader;
        StartCoroutine(GhostWalk());
    }

    private void StopGhostWalk()
    {
        m_ghostWalkActivated = false;
        m_actualSpeed = speed;
        playerMesh.material.shader = texShader;


    }

    public bool GhostWalkActivated()
    {
        return m_ghostWalkActivated;
    }
}
