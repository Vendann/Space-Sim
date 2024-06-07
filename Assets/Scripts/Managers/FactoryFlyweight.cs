using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Space
{
    public class FactoryFlyweight : SingletonManager<FactoryFlyweight>
    {
        [SerializeField] private bool collectionCheck = true;
        [SerializeField] private int defaultCapacity = 50;
        [SerializeField] private int maxSize = 150;

        private Dictionary<FlyweightDefType, IObjectPool<Flyweight>> pools =
            new Dictionary<FlyweightDefType, IObjectPool<Flyweight>>();

        public Flyweight Spawn(FlyweightDefinition definition) {
            return GetPoolForDefinition(definition)?.Get();
        }

        public Flyweight Spawn(FlyweightDefinition definition, Vector3 position) {
            var flyweight = GetPoolForDefinition(definition)?.Get();
            flyweight.transform.position = position;
            return flyweight;
        }

        public Flyweight Spawn(FlyweightDefinition definition, Vector3 position, Quaternion rotation) {
            var flyweight = GetPoolForDefinition(definition)?.Get();
            flyweight.transform.position = position;
            flyweight.transform.rotation = rotation;
            return flyweight;
        }

        public void ReturnToPool(Flyweight flyweight) {
            GetPoolForDefinition(flyweight.Definition)?.Release(flyweight);
        }

        private IObjectPool<Flyweight> GetPoolForDefinition(FlyweightDefinition definition) {
            IObjectPool<Flyweight> pool;

            if (pools.TryGetValue(definition.DefinitionType, out pool))
                return pool;

            pool = new ObjectPool<Flyweight>(
                definition.Create,
                definition.OnGet,
                definition.OnRelease,
                definition.OnDestroyPooledObject,
                collectionCheck,
                defaultCapacity,
                maxSize);

            pools.Add(definition.DefinitionType, pool);

            return pool;
        }
    }
}
