using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourcePlaces
{
    public class Taker : MonoBehaviour
    {
        [SerializeField]
        private Settings.TakerSettings _settings;

        private int resourcesInStorage = 0;
        public int ResourcesInStorage => resourcesInStorage;

        #region AboutTaking

        public bool TryTakeObject(Resources.Resource obj)
        {
            if(canTakeObject(obj))
            {
                takenResource();
                return true;
            }

            return false;
        }

        private void takenResource()
        {
            resourcesInStorage++;
        }

        private bool canTakeObject(Resources.Resource obj)
        {
            return obj.Type == _settings.ObjectType;
        }
        #endregion

        #region AboutGiving

        public bool CanDestroyResource()
        {
            return resourcesInStorage > 0;
        }

        #endregion
    }
}
