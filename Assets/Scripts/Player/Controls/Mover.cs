using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Mover : MonoBehaviour
    {
        [SerializeField]
        private Input _input;
        [SerializeField]
        private UnityEngine.AI.NavMeshAgent _navMeshAgent;
        private Transform navMeshAgentTrasform;
        [SerializeField]
        private float _speed;

        private void Awake()
        {
            _input.OnDrag.AddListener(onStartMoving);
            navMeshAgentTrasform = _navMeshAgent.transform;

            //_navMeshAgent.updateRotation = true;                 Doesn't work as planned
        }

        private void onStartMoving()
        {
            StartCoroutine(waitForMovingStop());
        }

        private IEnumerator waitForMovingStop()
        {
            while (_input.Dragging)
            {
                onMove();

                yield return new WaitForFixedUpdate();
            }
        }

        private void onMove()
        {
            Vector2 moveBy = _input.DragMagnitude;
            Vector3 moveVector = new Vector3(moveBy.x, 0f, moveBy.y) * _speed * Time.deltaTime;

            moveAgent(moveVector);
        }

        private void moveAgent(Vector3 deltaMove)
        {
            _navMeshAgent.Move(deltaMove);
            navMeshAgentTrasform.LookAt(navMeshAgentTrasform.position + deltaMove);
        }
    }
}