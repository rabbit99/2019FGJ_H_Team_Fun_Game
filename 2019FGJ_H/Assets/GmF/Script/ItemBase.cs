using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerBase))]
[RequireComponent(typeof(ModifyValueBase))]
public class ItemBase : TriggerBase
{
    public System.Action<ItemBase> OnDeath = null;
    public ModifyValueBase modifyData;
    public float LifeTime = 5;
    private float overLifeTime = 0;

    private AudioSource ItemAudioSource = null;
    public AudioClip GetItemAudioClip = null;
    public AudioClip CreateItemAudioClip = null;

    private void Awake()
    {
        modifyData = gameObject.GetComponent<ModifyValueBase>();
        SendData = modifyData.ModifyVlue;
        KeyName = modifyData.ModifyKeyString;

        if(ItemManager.Instance != null)
        {
            if (ItemManager.Instance != null && !ItemManager.Instance.SampleItems.Contains(this))
            {
                ItemManager.Instance.aliveItems.Add(this);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }

        ItemAudioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayCreateItemAudio();
    }

    public override void Update()
    {
        base.Update();
        CheckLife();
        overLifeTime += Time.deltaTime;
    }

    void CheckLife()
    {
        if (ItemManager.Instance != null && ItemManager.Instance.SampleItems.Contains(this))
        {
            return;
        }
        if (overLifeTime > LifeTime)
        {
            Death();
        }
    }

    void Death()
    {
        if (ItemManager.Instance != null)
        {
            ItemManager.Instance.aliveItems.Remove(this);
        }
        Destroy(gameObject);

        if (OnDeath != null)
        {
            OnDeath.Invoke(this);
        }
    }

    public override void OnTrigger()
    {
        base.OnTrigger();
        PlayGetItemAudio();
        Death();
    }

    void PlayGetItemAudio()
    {
        if (ItemAudioSource == null)
        {
            return;
        }

        if (GetItemAudioClip != null)
        {
            ItemAudioSource.clip = GetItemAudioClip;
        }
        else if (ItemManager.Instance != null && ItemManager.Instance.BasicGetItmeAudioClip != null)
        {
            ItemAudioSource.clip = ItemManager.Instance.BasicGetItmeAudioClip;
        }
        else
        {
            return;
        }

        ItemAudioSource.Play();
    }

    void PlayCreateItemAudio()
    {
        if (ItemAudioSource == null)
        {
            return;
        }
        if (ItemManager.Instance != null && ItemManager.Instance.BasicCreateItemAudioClip != null)
        {
            ItemAudioSource.clip = ItemManager.Instance.BasicCreateItemAudioClip;
        }
        if (CreateItemAudioClip != null)
        {
            ItemAudioSource.clip = CreateItemAudioClip;
        }

        ItemAudioSource.Play();
    }
}
