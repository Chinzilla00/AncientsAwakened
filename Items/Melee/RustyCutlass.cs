using Terraria.ID;

namespace AAMod.Items.Melee
{
    public class RustyCutlass : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rusty Cutlass");
			Tooltip.SetDefault("Even being rusty, it's still hard 'n sharp");
		}
		
		public override void SetDefaults()
		{
			item.damage = 21;
			item.melee = true;
			item.width = 34;
			item.height = 40;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 3;
			item.value = 20000;
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;  
		}
	}
}