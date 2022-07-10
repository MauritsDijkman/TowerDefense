using System.Collections.Generic;

/// <summary>
/// This script contains the Execute and Undo functions for the commands.
/// The ExecuteCommand function needs an ICommand to run. 
/// The given command is executed and added to the list with commands (so that it can be undone).
/// The UndoLastCommand function undoes the last command in the list.
/// </summary>

public class CommandStack
{
    private Stack<ICommand> _commandHistory = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _commandHistory.Push(command);
    }

    public void UndoLastCommand()
    {
        if (_commandHistory.Count <= 0)
            return;

        _commandHistory.Pop().Undo();
    }
}
