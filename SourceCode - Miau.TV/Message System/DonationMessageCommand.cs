using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonationMessageCommand : Command
{
    private readonly int _value;


    public DonationMessageCommand(int value)
    {
        this._value = value;
    }

    public override void Execute()
    {
       UIManager.Instance.NewDonation(_value);
       AudioManager.instance.Play("Donation3");
    }

}
