public class BillsPickUp : PickUpObject
{
    protected override void UpdateProgress(ProgressController controller)
    {
        base.UpdateProgress(controller);
        controller.ChangeProgress(true);
    }
}

