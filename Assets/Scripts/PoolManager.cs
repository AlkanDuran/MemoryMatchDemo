using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;

public class PoolManager : LocalSingleton<PoolManager>
{
    [SerializeField] private LeanGameObjectPool _cardPool;

    public GameObject SpawnCard()
    {
        GameObject result = null;
        _cardPool.TrySpawn(ref result);
        return result;
    }

    public void DespawnCard(GameObject card)
    {
        card.GetComponent<Card>().IsSelected = false;
        card.GetComponent<Button>().interactable = true;
        _cardPool.Despawn(card);
    }
}