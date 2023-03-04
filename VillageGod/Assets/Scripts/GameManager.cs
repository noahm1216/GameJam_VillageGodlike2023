using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InstanceType
{
	None,
	Tree,
	Log,
	Character
};

public class GameManager : MonoBehaviour
{
	// Start is called before the first frame update
	public LayerMask Playable;

	//private Vector3 DebugRayPoint;

	public InstanceType CurrentlySelectedType = InstanceType.None;
	public InteractableInstance CurrentlySelectedObject = null;

	public GameObject RingIndicator;
	public Transform ThreeDUIRef;
	public GameObject ArrowIndicatorPrefab;

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
	{
		if (CurrentlySelectedType == InstanceType.Character)
		{
			RingIndicator.transform.position = CurrentlySelectedObject.transform.position - new Vector3(0, 0.5f, 0);
		}
		else
		{
			RingIndicator.transform.position = new Vector3(0, -100, 0);
		}

		//Ray ray = cam.ScreenPointToRay(pos);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100, Playable))
		{
			if (Input.GetMouseButtonDown(0))
			{
				//DebugRayPoint = hit.point;
				SelectInstance(hit.collider.gameObject);
			}
			if (Input.GetMouseButtonDown(1))
			{
				if (CurrentlySelectedType == InstanceType.Character)
				{
					if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
					{
						Instantiate(ArrowIndicatorPrefab, hit.point, Quaternion.identity, ThreeDUIRef);
						CurrentlySelectedObject.GetComponent<NPC_Controller>().MoveToPoint(hit.point);
					}
					else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Interactable"))
					{
						Instantiate(ArrowIndicatorPrefab, hit.point, Quaternion.identity, ThreeDUIRef);
						CurrentlySelectedObject.GetComponent<NPC_Controller>().Target = hit.collider.gameObject.GetComponent<InteractableInstance>();
						CurrentlySelectedObject.GetComponent<NPC_Controller>().TargetType = hit.collider.gameObject.GetComponent<InteractableInstance>().Type;
						CurrentlySelectedObject.GetComponent<NPC_Controller>().DecideWhatToDoNext();
					}
				}
			}
		}
	}

	private void SelectInstance(GameObject g)
	{
		if (g.layer == LayerMask.NameToLayer("Character"))
		{
			CurrentlySelectedType = InstanceType.Character;
			CurrentlySelectedObject = g.GetComponent<InteractableInstance>();
		}
		else if (g.layer == LayerMask.NameToLayer("Ground"))
		{
			CurrentlySelectedType = InstanceType.None;
			CurrentlySelectedObject = null;
		}
	}

	private void OnDrawGizmos()
	{
		//Gizmos.DrawCube(DebugRayPoint, new Vector3(1,1,1));
	}
}
