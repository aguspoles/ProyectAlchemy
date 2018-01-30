using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePoint : MonoBehaviour {

	[SerializeField]
	GameObject player;
    public float arrowOffset;
    public LayerMask layerMask;
    public List<GameObject> indicators;

    private List<PotionIndicator> ObjIndicators;
    private GameObject arrow;
    private int m_activeInd = -1;

    Ray ray;

    void Start() {
        Cursor.visible = false; 
        
         ObjIndicators = new List<PotionIndicator>();

        arrow = transform.GetChild(0).gameObject;

        for (int i = 0; i < indicators.Count; i++)
        {
            PotionIndicator pot = Instantiate(indicators[i], transform).GetComponent<PotionIndicator>();
            pot.SetCode();
            ObjIndicators.Add(pot);
        }

        for (int i = 0; i < ObjIndicators.Count; i++)
            ObjIndicators[i].gameObject.SetActive(false);

    }
	
	void Update () {
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        
		RaycastHit hit = new RaycastHit ();

		if (player) {
			if (Physics.Raycast (ray, out hit, 500, layerMask)) {
				transform.position = hit.point;
				transform.position = new Vector3 (transform.position.x, hit.point.y + 0.1f, transform.position.z);
			}
            Vector3 dir = transform.position - player.transform.position;
            transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));

        }
	}

    public void ActivateIndicator(int code)
    {
        if (!CraftingDataBase.HasIndicator(code))
        {
            arrow.SetActive(true);
            if(m_activeInd > -1)
                ObjIndicators[m_activeInd].gameObject.SetActive(false);
            return;
        }

        arrow.SetActive(false);
        for (int i = 0; i < ObjIndicators.Count; i++)
        {
            if (ObjIndicators[i].GetCode() == code)
            {
                ObjIndicators[i].gameObject.SetActive(true);
                ObjIndicators[i].gameObject.transform.position = transform.position;
                m_activeInd = code;
            }
            else
                ObjIndicators[i].gameObject.SetActive(false);
        }   
    }

    public void DeactivateIndicator()
    {


        arrow.SetActive(true);
        if (m_activeInd > -1)
            ObjIndicators[m_activeInd].gameObject.SetActive(false);
        m_activeInd = -1;
    }

    void OnDrawGizmos()
    {
        Vector3 dir = transform.position - player.transform.position;
        Gizmos.DrawRay(player.transform.position, dir);
    }
}
