using Common;
using GameServer.Entities;
using System.Collections.Generic;

namespace GameServer.Managers
{
    internal class EntityManager : Singleton<EntityManager>
    {
        private int idx = 0;
        public List<Entity> AllEntities = new List<Entity>();
        public Dictionary<int, List<Entity>> MapEntities = new Dictionary<int, List<Entity>>();

        public void AddEntity(int mapID, Entity entity)
        {
            AllEntities.Add(entity);
            entity.EntityData.Id = ++this.idx;

            List<Entity> entities = null;
            if (!MapEntities.TryGetValue(mapID, out entities))
            {
                entities = new List<Entity>();
                MapEntities[mapID] = entities;
            }
            entities.Add(entity);
        }

        public void RemoveEntity(int mapID, Entity entity)
        {
            this.AllEntities.Remove(entity);
            this.MapEntities[mapID].Remove(entity);
        }
    }
}