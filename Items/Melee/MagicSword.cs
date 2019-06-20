using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class MagicSword : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mana Blade");
            Tooltip.SetDefault("Fires Homing projectiles at the cost of mana");
		}

		public override void SetDefaults()
		{
			item.damage = 10;
			item.magic = true;
			item.mana = 5;
			item.width = 46;
			item.height = 46;
			item.useTime = 30;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.shoot = mod.ProjectileType("MagicPro");
			item.shootSpeed = 8f;
			item.knockBack = 5;
			item.value = Item.sellPrice(0, 10, 0, 0);
			item.rare = 1;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ManaCrystal, 5);
			recipe.AddIngredient(ItemID.SilverBar, 15);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
        
	}
}
