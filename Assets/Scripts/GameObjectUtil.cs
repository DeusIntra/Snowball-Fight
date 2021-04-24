using UnityEngine;

public class GameObjectUtil
{
    public delegate void ChildHandler(GameObject child);

    /// <summary>
    /// Iterates all children of a game object
    /// </summary>
    /// <param name="gameObject">A root game object</param>
    /// <param name="childHandler">A function to execute on each child</param>
    /// <param name="recursive">Do it on children? (in depth)</param>
    public static void IterateChildren(GameObject gameObject, ChildHandler childHandler, bool recursive)
    {
        DoIterate(gameObject, childHandler, recursive);
    }

    /// <summary>
    /// NOTE: Recursive!!!
    /// </summary>
    /// <param name="gameObject">Game object to iterate</param>
    /// <param name="childHandler">A handler function on node</param>
    /// <param name="recursive">Do it on children?</param>
    private static void DoIterate(GameObject gameObject, ChildHandler childHandler, bool recursive)
    {
        foreach (Transform child in gameObject.transform)
        {
            childHandler(child.gameObject);
            if (recursive)
                DoIterate(child.gameObject, childHandler, true);
        }
    }
}
