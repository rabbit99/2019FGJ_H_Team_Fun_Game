using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    public List<ItemBase> SampleItems = new List<ItemBase>();

    public List<ItemBase> aliveItems = new List<ItemBase>();

    public List<ItemPoint> ItemPoints = new List<ItemPoint>();
    ItemPoint lastPutItemPoint = null;

    public int MaxItemCount = 2;
    public float CreateItemDurateTime = 10;
    float lastCreateItmTime = 0;
    public bool FirstCeateItem;

    void Awake()
    {
        Instance = this;
        for (int i = 0; i < SampleItems.Count; i++)
        {
            SampleItems[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        RandomCreateItem();
    }

    void RandomCreateItem()
    {
        if (SampleItems.Count == 0)
        {
            return;
        }

        if(aliveItems.Count >= MaxItemCount)
        {
            return;
        }

        if (lastCreateItmTime == 0 && FirstCeateItem)
        {
            CreateItem();
        }

        if (Time.time - lastCreateItmTime > CreateItemDurateTime)
        {
            CreateItem();
        }
    }

    void CreateItem()
    {
        List<ItemPoint> tmpItemPoint = ItemPoints.FindAll(e => !e.IsHaveItem);
        if(tmpItemPoint.Count == 0)
        {
            return;
        }

        ItemPoint newPoint = tmpItemPoint[Random.Range(0, tmpItemPoint.Count)];

        ItemBase sampleItem = SampleItems[Random.Range(0, SampleItems.Count)];
        newPoint.CreateItem(sampleItem);
    }

    //Vector3 RandomCreateItemPos()
    //{
    //    if(ItemPoints.Count == 0)
    //    {
    //        return Vector3.zero;
    //    }

    //    if(ItemPoints.Count == 1)
    //    {
    //        lastPutItemPoint = ItemPoints[0];
    //        return lastPutItemPoint.transform.position;
    //    }

    //    List<ItemPoint> tmpItemPoints = ItemPoints.FindAll(e => e != lastPutItemPoint);

    //    lastPutItemPoint = tmpItemPoints[Random.Range(0, tmpItemPoints.Count)];

    //    return lastPutItemPoint.transform.position;
    //}
}
