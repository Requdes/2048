using System.Collections.Generic;
using System;

public abstract class AbstractStateMashine : IStateMashine {
    private readonly Dictionary<Type, IState> _registeredStates;
    private IState _curretState;

    public AbstractStateMashine () {
        _registeredStates = new Dictionary<Type, IState>();
    }

    public void RegisterState<TState>(TState state) where TState : class, IState {
        _registeredStates.Add(typeof(TState), state);
    }

    public void Enter<TState> () where TState : class, IState {
        TState state = ChangeState<TState>();
        state.Enter();
    }

    private TState ChangeState<TState> () where TState : class, IState {
        if (_curretState is IExitableState exitable) exitable.Exit();

        TState state = GetState<TState>();
        _curretState = state;

        return state;
    }

    private TState GetState<TState> () where TState : class, IState {
        return _registeredStates[typeof(TState)] as TState;
    }
}
