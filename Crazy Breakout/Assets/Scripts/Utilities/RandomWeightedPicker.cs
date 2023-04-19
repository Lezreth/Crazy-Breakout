using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Pick an item randomly, but weighted.
/// </summary>
/// <typeparam name="T">The type of items to pick from.  Must contain a weight as defined by IWeighted.</typeparam>
public class RandomWeightedPicker<T> where T : IWeighted
{
    /// <summary>
    /// The items to choose from.
    /// </summary>
    private readonly IEnumerable<T> items;

    /// <summary>
    /// The sum of all of the weights in the collection.
    /// </summary>
    private readonly int totalWeight;

    /// <summary>
    /// Initialize the structure.  O(1) or O(n) depending on the options, the default being O(n).
    /// </summary>
    /// <param name="items">The list of items to use.</param>
    /// <param name="checkWeights"><c>True</c> = Will check the weights to ensure they are all positive.  <c>False</c> = Skip sign check.</param>
    /// <param name="shallowCopy"><c>True</c> = Will copy the original collection structure (not the items).  Keep in mind that the item's lifecycle is impacted.</param>
    /// <exception cref="ArgumentNullException">Thrown if items is null.</exception>
    /// <exception cref="ArgumentException">Thrown if items is empty or contains non-positive weights.</exception>
    public RandomWeightedPicker(IEnumerable<T> items, bool checkWeights = true, bool shallowCopy = true)
    {
        //  Ensure the list is not empty or null.
        if (items == null) { throw new MissingReferenceException("items"); }
        if (!items.Any()) { throw new UnityException("items cannot be empty"); }

        //  Cache the list of items.
        if (shallowCopy)
        { this.items = new List<T>(items); }
        else
        { this.items = items; }

        //  Ensure all weights are positive.
        if (checkWeights && this.items.Any(i => i.Weight <= 0))
        {
            throw new UnityException("Some items in the list have a non-positive weight.");
        }

        totalWeight = this.items.Sum(i => i.Weight);
    }

    /// <summary>
    /// Pick a random item based on its chance. O(n)
    /// </summary>
    /// <returns>The weighted randomly chosen item.</returns>
    public T PickAnItem()
    {
        int number = Random.Range(0, totalWeight);
        return items.First(i => (number -= i.Weight) < 0);
    }
}
