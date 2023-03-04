using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log_Interactable : InteractableInstance
{

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
        this.transform.parent = npc.transform;
        this.transform.localPosition = new Vector3(0, 2, 0);
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
