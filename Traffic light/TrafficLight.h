#pragma once
#include <iostream>

enum class State
{
    RED,
    YELLOW,
    GREEN
};

enum class Signal
{
    TO_RED,
    TO_YELLOW,
    TO_GREEN
};

class TrafficLight
{
public:
    TrafficLight();

    void changeSignal(Signal signal);
    void currentState() const;

private:
    State _currentState;

    void setState(State newState);
};
