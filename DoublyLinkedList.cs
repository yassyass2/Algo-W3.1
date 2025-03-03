using System.Collections;

namespace Solution;

public class DoublyLinkedList<T> : IDoublyLinkedList<T> where T : IComparable<T>
{
    public DoubleNode<T>? First, Last;
    public DoublyLinkedList() => First = Last = null;
    public void Clear() => First = Last = null;

    //Search
    public DoubleNode<T>? Search(T value)
    {
        if (First == null){
            return default;
        }
        var current = First;
        while(current != null){
            if (current.Value.CompareTo(value) == 0){
                return current;
            }
            current = current.Next;
        }
        return current;
    }

    #region "addNode=> first, last, sorted" 
    public void AddFirst(T value)
    {
        var newNode = new DoubleNode<T>(value, First);
        if (first == null){
            First = Last = newNode;
            return;
        }
        First.Previous = newNode;
        First = newNode;
    }

    public void AddLast(T value)
    {
        var newNode = new DoubleNode<T>(value, null, Last);
        if (Last == null){
            First = Last = newNode;
            return;
        }
        Last.Next = newNode;
        Last = newNode;
    }

    public void AddSorted(T value)
    {
        if(First == null || First.Value.CompareTo(value) > 0){
            AddFirst(value);
        }

        var node = new DoubleNode<T>(value);
        var current = First;
        while(current.Next != null && current.Next.Value.CompareTo(value) <= 0){
            current = current.Next;
        }

        if (current.Next == null){
            AddLast(value);
            return;
        }
        node.Previous = current;
        node.Next = current.Next;

        current.Next.Previous = node; // eerst previous
        current.Next = node;
    }
    #endregion

    public bool Remove(T value)
    {
        var node = Search(value);
        if (node == default) return false;
        Delete(value);
        return true;
    }

    public void Delete(DoubleNode<T> node)
    {
        // check Prev
        // check Next
        // check First
        // check Last
        if (First == null) return true;

        if (node.Previous != null){
            node.Previous.Next = node.Next;
        }
        if (node.Next != null){
            node.Next.Previous = node.Previous;
        }

        if (node == First){
            First = node.Next;
        }
        if (node == Last){
            Last = node.Previous;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        DoubleNode<T>? current = First;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
