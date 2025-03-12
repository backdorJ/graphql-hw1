namespace GraphQlSubscription;

public class Subscription
{
    [Subscribe]
    [Topic("NumberGenerated")]
    public NumberEvent GetNumberOfSubscriptions([EventMessage] NumberEvent number) => number;
}

public class NumberEvent
{
    public int Number { get; set; }
}