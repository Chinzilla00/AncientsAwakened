using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
	public class SwampSlasher : ModItem
	{
        static int shoot = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Swamp Slasher");
			Tooltip.SetDefault("Slashes with the power of the Mire's mist.");
		}
		public override void SetDefaults()
		{
			item.damage = 38;
			item.melee = true;
			item.width = 42;
			item.height = 50;
			item.useTime = 40;
			item.useAnimation = 26;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 1000;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("MireLeaf");
            item.shootSpeed = 10;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ExilesKatana", 1);
            recipe.AddIngredient(null, "HydraClaw", 10);
            recipe.AddIngredient(null, "HydraHide", 5);
            recipe.AddTile(null, "HellstoneAnvil");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockback)
        {
            shoot++;
            if (shoot % 3 != 0) return false;

            shoot = 0;
            return true;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 41);
            }
        }
    }
}
