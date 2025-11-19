using Models;
using UnityEngine;

namespace Managers
{
    internal class MinimapManager : Singleton<MinimapManager>
    {
        public UIMinimap minimap;

        private Collider minimapBoundingBox;

        public Collider MinimapBoundingBox
        {
            get { return minimapBoundingBox; }
        }

        public Transform PlayerTransform
        {
            get
            {
                if (User.Instance.CurrentCharacter == null)
                    return null;
                return User.Instance.CurrentCharacterObject.transform;
            }
        }

        public Sprite LoadCurrentMinimap()
        {
            return Resloader.Load<Sprite>("UI/Minimap/" + User.Instance.CurrentMapData.MiniMap);
        }

        public void UpdataMinimap(Collider minimapBoundingBox) 
        {
            this.minimapBoundingBox = minimapBoundingBox;
            if(this.minimap != null) 
            {
                this.minimap.UpdateMap();
            }
        }
    }
}