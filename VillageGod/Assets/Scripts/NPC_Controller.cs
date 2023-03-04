using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CharacterState
{
    Idle,
    Moving,
    Attacking
};

public class NPC_Controller : InteractableInstance
{

    public float InteractionRange = 0.5f;

    public CharacterState State = CharacterState.Idle;

    private NavMeshAgent agent;

    public InteractableInstance Target = null;
    public InstanceType TargetType = InstanceType.None;



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (State == CharacterState.Moving)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        // Done
                        DecideWhatToDoNext();
                    }
                }
            }
        }
        else if (State == CharacterState.Attacking)
        {
            Target.InteractedByNPC(this);
            Target = null;
            TargetType = InstanceType.None;
            DecideWhatToDoNext();
        }
    }

    public void MoveToPoint(Vector3 WorldPos)
    {
        State = CharacterState.Moving;
        agent.SetDestination(WorldPos);
    }

    public void Interact(GameObject Target)
    {

    }

    public void DecideWhatToDoNext()
    {
        if (TargetType == InstanceType.None)
        {
            State = CharacterState.Idle;
        }
        else if (TargetType == InstanceType.Tree || TargetType == InstanceType.Log)
        {
            Vector3 closestPoint = Target.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            float distance = Vector3.Distance(closestPoint, transform.position);
            if (distance > InteractionRange)
            {
                MoveToPoint(new Vector3(Target.transform.position.x, 0, Target.transform.position.z));
            }
            else
            {
                State = CharacterState.Attacking;
            }
        }
    }

    public override void LeftClickedOn()
    {
        throw new System.NotImplementedException();
    }

    public override void RightClickedOn()
    {
        throw new System.NotImplementedException();
    }

    public override void InteractedByNPC(NPC_Controller npc)
    {
        throw new System.NotImplementedException();
    }
}
