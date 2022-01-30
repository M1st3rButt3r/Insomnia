public class DeathMushroom : MotherMushroom
{
    protected override void StartEffect()
    {
        GameManager.Instance.Die("You ate an uneatable Mushroom! Please try again!");
    }
}
