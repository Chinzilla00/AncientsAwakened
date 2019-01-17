using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class SporeSac : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 22;
            item.height = 26;
			item.maxStack = 999;
			item.consumable = true;
			item.useTime = 28;
			item.useAnimation = 28;
			item.shoot = mod.ProjectileType("SPORZ");
			item.shootSpeed = 1f;
			item.useStyle = 1;
			item.value = Item.sellPrice(0, 0, 1, 0);
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spore Bag");
			Tooltip.SetDefault(@"Spreads the surface mushroom biome
Don't ask the mushman how he made this.");
		}
	}
}
