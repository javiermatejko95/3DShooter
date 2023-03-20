public class SegmentModel : EntityModel
{
    private int maxHealthAmount;

    public int MaxHealthAmount { get => maxHealthAmount; }

    public SegmentModel(SegmentData data) : base (data) 
    {
        id = data.Id;
        healthAmount = data.MaxHealthAmount;
        maxHealthAmount = data.MaxHealthAmount;
    } 
}
