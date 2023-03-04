using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableInstance : MonoBehaviour
{
    public InstanceType Type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    abstract public void LeftClickedOn();

    abstract public void RightClickedOn();
    abstract public void InteractedByNPC(NPC_Controller npc);
}
