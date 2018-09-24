using System;

public class PaperRockScissors
{
    public static char GetUserInput()
    {
        char userChoice; string input;
        Console.Write(": ");
        input = Console.ReadLine().ToLower();
        while ((input != "p") && (input != "r") && (input != "q")
            && (input != "s"))
        {
            Console.WriteLine("Invalid Input! Enter either [R]ock, [P]aper or [S]cissors");
            Console.Write("what is your next move?: ");
            input = Console.ReadLine();            
        }
        userChoice = Convert.ToChar(input.ToLower());
        return userChoice; 
    }
    public static char GetComputerChoice()
    {
        int randomNumber;
        char compChoice;
        Random random = new Random();
        randomNumber = random.Next(0, 3);
        if (randomNumber == 0)
            compChoice = 'p';
        else if (randomNumber == 1)
            compChoice = 'r';
        else
            compChoice = 's';
        return compChoice;
    }

    public static int FindWinner(char user_choice, char computer_choice)
    {
        int result;
        if (user_choice == computer_choice)
            result = 0;
        else if ((user_choice == 'p' && computer_choice == 'r') || (user_choice == 'r' && computer_choice == 's')
            || (user_choice == 's' && computer_choice == 'p'))
            result = 1;
        else
            result = -1;
        return result;        
    }

    public static void Main()
    {
        int  result;
        char userMove = 'x', computerMove;
        while (userMove != 'q')
        {
            Console.Write("Enter either [R]ock, [P]aper, [S]cissors or [Q]uit");
            userMove = GetUserInput();
            if (userMove != 'q')
            {
                computerMove = GetComputerChoice();
                result = FindWinner(userMove, computerMove);
                if (result == 1)
                    Console.WriteLine("You Win! :D");
                else if (result == -1)
                    Console.WriteLine("ComputerWins! :(");
                else
                    Console.WriteLine("Tie! :|");
            }
        }
    }
}