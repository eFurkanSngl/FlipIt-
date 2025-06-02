using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public static ParticlePool Instance;
    [SerializeField] private GameObject _particleObject;
    [SerializeField] private int _poolsize = 10;

    private Queue<GameObject> _particlePool = new Queue<GameObject>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance.gameObject);
        }
        InitializePool();
    }

    private void InitializePool()
    {
        for(int i = 0; i < _poolsize; i++)
        {
            GameObject obj = Instantiate(_particleObject);
            obj.SetActive(false);
            _particlePool.Enqueue(obj);
        }
    }

    public GameObject GetParticlePool()
    {
        if(_particlePool.Count > 0)
        {
            GameObject obj = _particlePool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        GameObject newObj = Instantiate(_particleObject);
        return newObj;
    }

    public void ReturnParticlePool(GameObject obj)
    {
        obj.SetActive(false);   
        _particlePool.Enqueue(obj);
    }

}
