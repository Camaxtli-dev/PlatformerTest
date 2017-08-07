using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : Singleton<PoolManager> {
   
    private Dictionary<int, List<ObjectInstance>> poolDictionary = new Dictionary<int, List<ObjectInstance>>();

    public void CreatePool(GameObject prefab, int poolSize) {
        int poolKey = prefab.GetInstanceID();
        if(!poolDictionary.ContainsKey(poolKey)) {
            poolDictionary.Add(poolKey, new List<ObjectInstance>());

            GameObject poolHolder = new GameObject(prefab.name + " pool");
            poolHolder.transform.parent = transform;

            for(int i = 0; i < poolSize; i++) {
                ObjectInstance newObject = new ObjectInstance(GameObject.Instantiate(prefab) as GameObject);
                poolDictionary[poolKey].Add(newObject);
                newObject.SetParent(poolHolder.transform);
            }
        }
    }

    private ObjectInstance IncreasePool(GameObject prefab) {
        ObjectInstance newObject = new ObjectInstance(GameObject.Instantiate(prefab) as GameObject);
        poolDictionary[prefab.GetInstanceID()].Add(newObject);

        newObject.SetParent(transform.Find(prefab.name + " pool").transform);
        return newObject;
    }

    public GameObject GetReuseObject(GameObject prefab, Vector3 position, Quaternion rotation) {
        int poolKey = prefab.GetInstanceID();
        if(poolDictionary.ContainsKey(poolKey)) {
            ObjectInstance objectToReuse;
            foreach(ObjectInstance obj in poolDictionary[poolKey]) {
                if(obj.gameObject.activeSelf == false) {
                    obj.Reuse(position, rotation);
                    return obj.gameObject;
                }
            }
            objectToReuse = IncreasePool(prefab);
            objectToReuse.Reuse(position, rotation);
            return objectToReuse.gameObject;
        }
        return null;
    }

    class ObjectInstance {
        public GameObject gameObject;

        public ObjectInstance(GameObject objectInstance) {
            gameObject = objectInstance;
            gameObject.SetActive(false);
        }

        public void Reuse(Vector3 position, Quaternion rotation) {
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
            gameObject.SetActive(true);

            if(gameObject.GetComponent<PoolObject>()) {
                gameObject.GetComponent<PoolObject>().OnObjectReuse();
            }
        }

        public void SetParent(Transform parent) {
            gameObject.transform.parent = parent;
        }
    }
}