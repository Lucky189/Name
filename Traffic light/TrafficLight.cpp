#include "TrafficLight.h"

TrafficLight::TrafficLight() : _currentState(State::RED)
{
    std::cout << "Traffic light started. Current state: RED\n";
}

void TrafficLight::setState(State newState)
{
    _currentState = newState;

    std::cout << "New state: ";
    switch (_currentState)
    {
    case State::RED:
        std::cout << "RED\n";
        break;
    case State::YELLOW:
        std::cout << "YELLOW\n";
        break;
    case State::GREEN:
        std::cout << "GREEN\n";
        break;
    }
}

void TrafficLight::changeSignal(Signal signal)
{
    switch (signal)
    {
    case Signal::TO_RED:
        setState(State::RED);
        break;
    case Signal::TO_YELLOW:
        setState(State::YELLOW);
        break;
    case Signal::TO_GREEN:
        setState(State::GREEN);
        break;
    }
}

void TrafficLight::currentState() const
{
    std::cout << "Current state is ";

    switch (_currentState)
    {
    case State::RED:
        std::cout << "RED\n";
        break;
    case State::YELLOW:
        std::cout << "YELLOW\n";
        break;
    case State::GREEN:
        std::cout << "GREEN\n";
        break;
    }
}