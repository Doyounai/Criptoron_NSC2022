using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    private NavMeshAgent agent;
    public safeManager _safeManger;

    public float scanTime = 3f;
    public float elapseTime = 0f;

    safe destination;

    public Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        newDestinatino();
    }

    private void Update()
    {
        if (destination == null)
            return;

        if (Vector3.Distance(transform.position, destination.transform.position) <= 0.8f)
        {
            if (elapseTime < scanTime)
            {
                destination.is_currentUse = true;
                elapseTime += Time.deltaTime;
            }
            else
            {
                elapseTime = 0;
                destination.is_currentUse = false;
                if (destination.is_correctSafe)
                    win();
                newDestinatino();
            }
        }
        else if (destination.is_currentUse)
        {
            newDestinatino();
        }
    }

    private void win()
    {
        _safeManger.showWinner(this.name);
    }

    public void newDestinatino()
    {
        int landingIndex;
        int count = 0;

        if(destination != null)
            destination.is_currentUse = false;

        do
        {
            count++;
            if(count >= 20)
            {
                destination = null;
                agent.SetDestination(Vector3.zero);
                break;
            }

            landingIndex = Random.Range(0, _safeManger.safes.Count);
            destination = _safeManger.safes[landingIndex];
            agent.SetDestination(_safeManger.safes[landingIndex].botLandingPoint.position);
        } while (_safeManger.safes[landingIndex].is_currentUse);
    }

    public void stopWalking()
    {
        agent.isStopped = true;
    }

    public void playWalking()
    {
        agent.isStopped = false;
        elapseTime = 0f;
        newDestinatino();
    }
}
