using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class Firestorm : BaseAAItem
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Firestorm");
			Tooltip.SetDefault("Launches bouncing fireballs on swing");
        }
		
		public override void SetDefaults()
		{
			item.damage = 64;
			item.melee = true;
			item.width = 64;
			item.height = 64;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 1;
			item.knockBack = 4;
			item.value = 50000;
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = 15;
			item.shootSpeed = 10f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float numberProjectiles = 3;
			float rotation = MathHelper.ToRadians(10);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX*4, speedY*4).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}
		
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextFloat() < 1f)
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 6, 0f, 0f, 46, default(Color), 1.381579f);
            }
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RadiantIncinerite", 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }
	}
}
