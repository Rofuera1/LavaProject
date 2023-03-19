using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Inventory : MonoBehaviour
    {
        public delegate void ChangedValue(Resources.Types type, int newValue);
        public ChangedValue OnValueChanged;

        private Dictionary<Resources.Types, List<Resources.Resource>> resources = new Dictionary<Resources.Types, List<Resources.Resource>>();

        public void AddResource(Resources.Resource res)
        {
            if(resources.ContainsKey(res.Type))
            {
                resources[res.Type].Add(res);
                OnValueChanged?.Invoke(res.Type, resources[res.Type].Count);
            }
            else
            {
                resources.Add(res.Type, new List<Resources.Resource>() { res });
                OnValueChanged?.Invoke(res.Type, 1);
            }    
        }
    }
}
