public class BottlePickUp : PickUpObject
{
    protected override void UpdateProgress(ProgressController controller)
    {
        base.UpdateProgress(controller);
        controller.ChangeProgress(false);
    }
}

