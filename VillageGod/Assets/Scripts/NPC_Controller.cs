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
    
    [Header("Parameter")]
    public CharacterState State = CharacterState.Idle;
    public InteractableInstance Target = null;
    public InstanceType TargetType = InstanceType.None;

    [Header("Component Reference")]
    public Animator animator;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();

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


    public void UpdateAnimation()
    {
        if (State == CharacterState.Moving)
        {
            animator.SetBool("Moving", true);
            Vector3 v3d = agent.velocity;
            //Debug.Log("Before"+v);
            v3d = Quaternion.AngleAxis(-45, new Vector3(0, 1, 0)) * v3d;
            //Debug.Log("After"+v);
            Vector2 v2d = new Vector2(v3d.z, v3d.x * -1);
            //Debug.Log(v2d);
            float up, down, left, right;
            if (v2d.x < 0)
            {
                left = Mathf.Abs(v2d.x);
                right = 0;
            }
            else 
            {
                right = v2d.x;
                left = 0;
            }
            if (v2d.y < 0)
            {
                down = Mathf.Abs(v2d.y);
                up = 0;
            }
            else
            {
                up = v2d.y;
                down = 0;
            }
            float max = Mathf.Max(up, down, left, right);
            if (max == up)
            {
                animator.SetInteger("MovingDirection", 1);
            }
            else if (max == down)
            {
                animator.SetInteger("MovingDirection", 3);
            }
            else if (max == left)
            {
                animator.SetInteger("MovingDirection", 4);
            }
            else if (max == right)
            {
                animator.SetInteger("MovingDirection", 2);
            }
        }
        else animator.SetBool("Moving", false);
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
