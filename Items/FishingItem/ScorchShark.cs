using Terraria.ID;

namespace AAMod.Items.FishingItem
{
    public class ScorchShark : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.damage = 40;
			item.melee = true;
			item.width = 36;
			item.height = 32;
			item.useTime = 7;
			item.useAnimation = 20;
			item.pick = 180;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 5;
            item.value = 108000;
            item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scorch Shark");
			Tooltip.SetDefault("");
		}
    }
}
