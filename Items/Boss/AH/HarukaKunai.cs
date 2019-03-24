using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.AH
{
    public class HarukaKunai : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 80;
			item.ranged = true;
			item.width = 14;
			item.height = 34;
			item.noUseGraphic = true;
			item.maxStack = 999;
			item.consumable = true;
			item.useTime = 8;
			item.useAnimation = 8;
			item.shoot = mod.ProjectileType("HarukaKunaiF");
			item.shootSpeed = 15f;
			item.useStyle = 1;
			item.knockBack = 0;
			item.value = Item.sellPrice(0, 0, 0, 0);
			item.rare = 0;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Abyssal Kunai");
		}
	}
}
