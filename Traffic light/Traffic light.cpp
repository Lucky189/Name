#include "TrafficLight.h"
#include <iostream>

int main()
{
    TrafficLight light;
    int choice;

    while (true)
    {
        std::cout << "\nChoose signal:\n";
        std::cout << "1 - RED\n";
        std::cout << "2 - YELLOW\n";
        std::cout << "3 - GREEN\n";
        std::cout << "0 - EXIT\n";
        std::cout << "Your choice: ";

        std::cin >> choice;

        if (choice == 0)
            break;

        switch (choice)
        {
        case 1:
            light.changeSignal(Signal::TO_RED);
            break;
        case 2:
            light.changeSignal(Signal::TO_YELLOW);
            break;
        case 3:
            light.changeSignal(Signal::TO_GREEN);
            break;
        default:
            std::cout << "Invalid input!\n";
        }

        light.currentState();
    }

    return 0;
}