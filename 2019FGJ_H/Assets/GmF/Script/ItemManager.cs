using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    public List<ItemBase> SampleItems = new List<ItemBase>();

    [HideInInspector]
    public List<ItemBase> aliveItems = new List<ItemBase>();

    public List<ItemPoint> ItemPoints = new List<ItemPoint>();

    public int MaxItemCount = 2;
    public float CreateItemIntervalTime = 10;
    public float CreateItemIntervalTimeRadomRange = 10;
    private float NextCreateItemIntervalTime = 0;
    float lastCreateItmTime = 0;
    public bool StartPlayCeateItem;
    public int StartPlayeCreateItemAmount = 1;
    public AudioClip BasicGetItmeAudioClip;
    public AudioClip BasicCreateItemAudioClip;

    [HideInInspector]
    public int CreateItemCount = 0;

    public 

    void Awake()
    {
        Instance = this;
        for (int i = 0; i < SampleItems.Count; i++)
        {
            SampleItems[i].gameObject.SetActive(false);
        }

        RegistItemMsg();
        NextCreateItemIntervalTime = CreateItemIntervalTime + Random.Range(0, CreateItemIntervalTimeRadomRange);
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

        if (lastCreateItmTime == 0 && StartPlayCeateItem && CreateItemCount < StartPlayeCreateItemAmount)
        {
            CreateItem();
        }

        if (Time.time - lastCreateItmTime > NextCreateItemIntervalTime)
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

        CreateItemCount++;
        lastCreateItmTime = Time.time;
        NextCreateItemIntervalTime = CreateItemIntervalTime + Random.Range(0, CreateItemIntervalTimeRadomRange);

        newPoint.CreateItem(sampleItem);
    }

    ItemObserver itemObserver;

    void RegistItemMsg()
    {
        if(itemObserver == null)
        {
            itemObserver = new ItemObserver();

            string[] modifyEnumNames = System.Enum.GetNames(typeof(ModifyValueEnum));
            for (int i = 0; i < modifyEnumNames.Length; i++)
            {
                NotificationCenter.Default.AddObserver(itemObserver, modifyEnumNames[i]);
            }
        }
    }

    public class ItemObserver : INotification
    {
        void INotification.OnNotify(Notification _noti)
        {
            Debug.Log("ItemManager.cs Get item msg: name[" + _noti.name + "], " + "data[" + _noti.data.ToString() + "], ");
        }
    }
}
