using Terraria.ID;

namespace AAMod.Items.Boss.Greed
{
    public class StoneShell : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stone Shell");
            Tooltip.SetDefault(@"Harder than bedrock but lighter than pumice");
        }
        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 24;
			item.maxStack = 99;
            item.rare = ItemRarityID.Yellow;
        }
    }
}
