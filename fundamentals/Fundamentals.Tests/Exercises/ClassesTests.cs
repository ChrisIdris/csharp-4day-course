using Fundamentals.Exercises;

namespace Fundamentals.Tests.Exercises;

// Tests for the exercises in Exercises/Classes.cs.
// These will fail until the student implements each method.
public class ClassesTests
{
    [Fact]
    public void Constructor_StartsEmpty()
    {
        ShoppingCart cart = new ShoppingCart();
        Assert.Equal(0, cart.Count());
    }

    [Fact]
    public void Add_IncreasesCount()
    {
        ShoppingCart cart = new ShoppingCart();
        cart.Add("apple");
        cart.Add("pear");
        Assert.Equal(2, cart.Count());
    }

    [Fact]
    public void Contains_TrueAfterAdd()
    {
        ShoppingCart cart = new ShoppingCart();
        cart.Add("apple");
        Assert.True(cart.Contains("apple"));
        Assert.False(cart.Contains("pear"));
    }

    [Fact]
    public void Remove_ReturnsTrueAndShrinksCountWhenFound()
    {
        ShoppingCart cart = new ShoppingCart();
        cart.Add("apple");
        cart.Add("pear");
        Assert.True(cart.Remove("apple"));
        Assert.Equal(1, cart.Count());
        Assert.False(cart.Contains("apple"));
        Assert.True(cart.Contains("pear"));
    }

    [Fact]
    public void Remove_ReturnsFalseWhenMissing()
    {
        ShoppingCart cart = new ShoppingCart();
        cart.Add("apple");
        Assert.False(cart.Remove("pear"));
        Assert.Equal(1, cart.Count());
    }

    [Fact]
    public void CartIsReferenceType_MutationViaHelperIsVisibleToCaller()
    {
        // Sanity check of the teaching point: passing a class instance into
        // another context and mutating it IS visible to the caller.
        ShoppingCart cart = new ShoppingCart();
        AddTwo(cart);
        Assert.Equal(2, cart.Count());
    }

    private static void AddTwo(ShoppingCart cart)
    {
        cart.Add("apple");
        cart.Add("pear");
    }
}
