using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class AbyssalTwilight : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Abyssal Twilight");
			Tooltip.SetDefault("The Eternal Dusk Beckons");
		}

		public override void SetDefaults()
		{
			item.damage = 35;
			item.melee = true;
			item.width = 60;
			item.height = 60;
			item.useTime = 14;
			item.useAnimation = 14;
			item.useStyle = 1;
			item.knockBack = 2;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "ExilesKatana", 1);
			recipe.AddIngredient(mod, "OceanRazor", 1);
			recipe.AddIngredient(mod, "DoomiteSaber", 1);
			recipe.AddIngredient(mod, "IceLongsword", 1);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, Terraria.ModLoader.ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 46, default, 1.381579f)];
                dust.noGravity = true;
            }
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 400);
        }
	}
}
