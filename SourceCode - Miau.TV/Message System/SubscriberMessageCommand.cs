using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubscriberMessageCommand : Command
{
    private readonly int _value;


    public SubscriberMessageCommand()
    {
    }

    public override void Execute()
    {
        UIManager.Instance.NewSubscriber();
        AudioManager.instance.Play("Follow1");
    }
}
