using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class CollisionDetecter : MonoBehaviour
    {
        public delegate void OnCollided<T>(T obj) where T : class;

        public OnCollided<Resources.Resource> CollidedWithResource;
        public OnCollided<ResourcePlaces.LaborProducer> CollidedWithProducer;
        public OnCollided<ResourcePlaces.Taker> CollidedWithTaker;

        public OnCollided<Resources.Resource> StopCollidedWithResource;
        public OnCollided<ResourcePlaces.LaborProducer> StopCollidedWithProducer;
        public OnCollided<ResourcePlaces.Taker> StopCollidedWithTaker;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Resources.Resource>())
                CollidedWithResource?.Invoke(other.gameObject.GetComponent<Resources.Resource>());

            else if (other.gameObject.GetComponent<ResourcePlaces.LaborProducer>())
                CollidedWithProducer?.Invoke(other.gameObject.GetComponent<ResourcePlaces.LaborProducer>());

            else if (other.gameObject.GetComponent<ResourcePlaces.Taker>())
                CollidedWithTaker?.Invoke(other.gameObject.GetComponent<ResourcePlaces.Taker>());
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<Resources.Resource>())
                StopCollidedWithResource?.Invoke(other.gameObject.GetComponent<Resources.Resource>());

            else if (other.gameObject.GetComponent<ResourcePlaces.LaborProducer>())
                StopCollidedWithProducer?.Invoke(other.gameObject.GetComponent<ResourcePlaces.LaborProducer>());

            else if (other.gameObject.GetComponent<ResourcePlaces.Taker>())
                StopCollidedWithTaker?.Invoke(other.gameObject.GetComponent<ResourcePlaces.Taker>());
        }
    }
}
