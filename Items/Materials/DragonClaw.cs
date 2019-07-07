namespace AAMod.Items.Materials
{
    public class DragonClaw : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Claw");
            Tooltip.SetDefault("Don't prick yourself");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
			item.maxStack = 99;
        }
    }
}
