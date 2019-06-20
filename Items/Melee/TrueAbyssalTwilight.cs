using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class TrueAbyssalTwilight : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dread Twilight");
			Tooltip.SetDefault("The rising moon incarnate");
		}
		public override void SetDefaults()
		{
			item.damage = 75;
			item.melee = true;
			item.width = 76;
			item.height = 76;
			item.useTime = 17;
			item.useAnimation = 17;
			item.useStyle = 1;
			item.knockBack = 3;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 8;
			item.UseSound = SoundID.Item19;
			item.autoReuse = false;
			item.shoot = mod.ProjectileType("TrueAbyssalTwilightShot");
            item.shootSpeed = 10f;
		}

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, mod.DustType<Dusts.AbyssDust>(), 0f, 0f, 46, default(Color), 1.381579f)];
                dust.noGravity = true;
            }
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(mod, "AbyssalTwilight", 1);
                recipe.AddIngredient(ItemID.BrokenHeroSword, 1);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(mod, "AbyssalTwilight", 1);
                recipe.AddIngredient(mod, "MireCrystal", 1);
                recipe.AddIngredient(mod, "TerraCrystal", 1);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Venom, 500);
        }
	}
}
