using System;

namespace Enemy
{
    public abstract class BaseState
    {
        public abstract void Enter();
        public abstract Type Tick();
        public abstract void Exit();
    }
}
  
   
