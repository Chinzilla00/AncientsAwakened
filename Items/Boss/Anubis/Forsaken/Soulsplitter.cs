using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Anubis.Forsaken
{
    public class Soulsplitter : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soulsplitter");
            Tooltip.SetDefault("Shoots out a trio of wall-piercing returning phantom blades on swing");
        }

		public override void SetDefaults()
		{
			item.autoReuse = true;
			item.useStyle = 1;
			item.useAnimation = 20;
			item.useTime = 20;
			item.knockBack = 5f;
			item.width = 24;
			item.height = 28;
			item.damage = 163;
			item.UseSound = SoundID.Item71;
			item.shoot = mod.ProjectileType("Soulsplitter");
			item.shootSpeed = 14f;
			item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
			item.rare = 11;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			float numberProjectiles = 3;
			float rotation = MathHelper.ToRadians(6);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<JackalsWrath>(), 1);
            recipe.AddIngredient(null, "SoulFragment", 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
