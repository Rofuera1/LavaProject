using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private Mover _mover;
        [SerializeField]
        private CollisionDetecter _collisionDetector;
        [SerializeField]
        private Inventory _inventory;
        [SerializeField]
        private Transferer _transferer;

        private void Awake()
        {
            _collisionDetector.CollidedWithResource += onResourceCollide;

            _collisionDetector.CollidedWithProducer += onLaborCollide;
            _collisionDetector.StopCollidedWithProducer += onLaborExit;
        }

        private void onResourceCollide(Resources.Resource res)
        {
            if(res.PickUpByPlayer(_inventory))
                _transferer.TransferResourceToInventory(res, () => _inventory.AddResource(res));
        }

        private void onLaborCollide(ResourcePlaces.LaborProducer labor)
        {
            labor.OnStartLabor();
        }

        private void onLaborExit(ResourcePlaces.LaborProducer labor)
        {
            labor.OnStopLabor();
        }
    }
}
