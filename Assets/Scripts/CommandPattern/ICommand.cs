/// <summary>
/// This script is an interface. It is an interface, so that it can be implemented by other classes.
/// Any class which implements the ICommand interface must have a public function which matches this signature (Execute and Undo).
/// </summary>

public interface ICommand
{
    public void Execute();
    public void Undo();
}
