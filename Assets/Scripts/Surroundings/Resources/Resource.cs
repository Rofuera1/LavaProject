using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Resources
{
    public enum States
    {
        OnGround,
        NotOnGround
    }

    public enum Types
    {
        Wood,
        Iron,
        Diamond
    }

    public class Resource : MonoBehaviour
    {
        public Types Type;
        public States CurrentState { get; private set; }

        public Event OnCreated;
        public Event OnPickedUp;
        public Event OnGivenToFactory;

        public virtual void Created(ResourcePlaces.Producer producer)
        {
            CurrentState = States.NotOnGround;
        }

        public virtual void PlacedOnGround()
        {
            CurrentState = States.OnGround;
        }

        public virtual bool PickUpByPlayer(Player.Inventory inventory)
        {
            return CurrentState == States.OnGround;
        }

        public virtual void GiveToFactory(ResourcePlaces.Taker factory)
        {

        }
    }
}
