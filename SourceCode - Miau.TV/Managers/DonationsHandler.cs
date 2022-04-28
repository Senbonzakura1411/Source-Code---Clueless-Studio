using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DonationsHandler : Singleton<DonationsHandler>
{
    protected int donationAmount;

    private Queue<Command> _commands = new Queue<Command>();
    private Command _currentCommand;

    private void Update()
    {
        ProcessCommands();
    }

    void ProcessCommands()
    {
        if (_currentCommand != null && UIManager.Instance.donationAlive)
            return;

        if (!_commands.Any())
            return;

        _currentCommand = _commands.Dequeue();
        _currentCommand.Execute();
    }
    public void AddDonation(int value)
    {
        donationAmount += value;
        _commands.Enqueue(new DonationMessageCommand(value));
    }

    public void CollectDonations()
    {
        ResourcesManager.Instance.AddMoney(donationAmount);
        donationAmount = 0;
    }
}
