using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_Interactable : InteractableInstance
{
    public GameObject LogPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void InteractedByNPC(NPC_Controller npc)
    {
        Instantiate(LogPrefab, new Vector3(transform.position.x, 0, transform.position.z), transform.rotation);
        Destroy(this.gameObject);
    }

    public override void LeftClickedOn()
    {
        throw new System.NotImplementedException();
    }

    public override void RightClickedOn()
    {
        throw new System.NotImplementedException();
    }
}
