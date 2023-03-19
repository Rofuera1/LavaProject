using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu()]
    public class ProducerSettings : ScriptableObject
    {
        public Resources.Types Type;
        public int ProduceCapacity;
        public int ProduceAtOnce;
        public float CountdownForOne;
        public float RadiusForResourceEmission;
    }
}
