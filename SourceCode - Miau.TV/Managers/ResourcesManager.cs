using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesManager : Singleton<ResourcesManager>
{
    public int money
    {
        get { return _money; }
        private set { _money = Mathf.Clamp(value, 0, int.MaxValue); }
    }
    protected int _money = 100;

    public int viewers
    {
        get { return _viewers; }
        private set{ _viewers = Mathf.Clamp(value, 0, int.MaxValue); }
    }
    protected int _viewers;

    public int subscribers
    {
        get { return _subscribers; }
        private set { _subscribers = Mathf.Clamp(value, 0, int.MaxValue); }
    }
    protected int _subscribers;

    private Queue<Command> _commands = new Queue<Command>();
    private Command _currentCommand;

    private void Update()
    {
        ProcessCommands();
    }

    public void AddMoney(int value)
    {
        money += value;
    }

    public void SubstractMoney(int value)
    {
        money -= value;
    }

    public void AddViewers(int value)
    {
        viewers += value;
    }
    public void SubstractViewers(int value)
    {
        viewers -= value;
    }
    public void AddSubscriber()
    {
       _commands.Enqueue(new SubscriberMessageCommand());
        subscribers ++;
    }

    void ProcessCommands()
    {
        if (_currentCommand != null && UIManager.Instance.subscriberAlive)
            return;

        if (!_commands.Any())
            return;

        _currentCommand = _commands.Dequeue();
        _currentCommand.Execute();
    }

}
