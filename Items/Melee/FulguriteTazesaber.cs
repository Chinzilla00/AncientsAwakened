using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
	public class FulguriteTazesaber : BaseAAItem
	{
		public override void SetDefaults()
		{

			item.damage = 90;
			item.melee = true;
			item.width = 46;
			item.height = 48;
			item.useTime = 24;
			item.useAnimation = 24;
			item.useStyle = 1;
			item.knockBack = 3;
			item.value = 54000;
			item.rare = 5;
			item.UseSound = SoundID.Item15;
			item.autoReuse = true;
			item.useTurn = true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fulgurite Tazesaber");
			Tooltip.SetDefault("");
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 62);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "FulguriteBar", 18);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddTile(134);
			recipe.AddRecipe();
		}
	}
}
