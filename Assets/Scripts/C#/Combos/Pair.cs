/// <summary>
/// Pair Class, for swapping Gesture IDs.
/// </summary>
public class Pair {
    int Head;//
    int Tail;

	/// <summary>
	/// Initializes a new instance of the <see cref="Pair"/> class.
	/// </summary>
	/// <param name="Head">Head.</param>
	/// <param name="Tail">Tail.</param>
    public Pair(int Head, int Tail){
        this.Head = Head;
        this.Tail = Tail;
    }

	/// <summary>
	/// Gets the head.
	/// </summary>
	/// <returns>The head.</returns>
    public int GetHead()
    {
        return Head;
    }

	/// <summary>
	/// Gets the tail.
	/// </summary>
	/// <returns>The tail.</returns>
    public int GetTail()
    {
        return Tail;
    }
}
