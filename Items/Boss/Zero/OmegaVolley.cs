using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class OmegaVolley : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.useStyle = 5;
			item.autoReuse = true;
			item.useAnimation = 22;
			item.useTime = 4;
            item.reuseDelay = 22;
			item.width = 72;
			item.height = 34;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;
			item.UseSound = SoundID.Item41;
			item.damage = 50;
			item.shootSpeed = 5f;
			item.noMelee = true;
			item.value = Item.sellPrice(0, 30, 0, 0);
			item.rare = 11;
			item.knockBack = 3f;
			item.ranged = true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Omega Volley");
			Tooltip.SetDefault(@"Shoots an insanely accurate volley of sonic bullets
33% chance to not consume ammo");
        }

		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .77;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, -2);
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num117 = 0.314159274f;
            int num118 = 6;
            Vector2 vector7 = new Vector2(speedX, speedY);
            vector7.Normalize();
            vector7 *= 40f;
            bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector7, 0, 0);
            for (int num119 = 0; num119 < num118; num119++)
            {
                float num120 = (float)num119 - ((float)num118 - 1f) / 2f;
                Vector2 value9 = vector7.RotatedBy((double)(num117 * num120), default(Vector2));
                if (!flag11)
                {
                    value9 -= vector7;
                }
                int num121 = Projectile.NewProjectile(vector2.X + value9.X, vector2.Y + value9.Y, speedX, speedY, type, (int)((double)damage), knockBack, player.whoAmI, 0.0f, 0.0f);
                Main.projectile[num121].noDropItem = true;
            }
            float SpeedX = speedX + (float)Main.rand.Next(-25, 26) * 0.05f;
            float SpeedY = speedY + (float)Main.rand.Next(-25, 26) * 0.05f;

            return false;

        }


        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity14;
                }
            }
        }
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "ApocalyptitePlate", 5);
			recipe.AddIngredient(ItemID.ChainGun);
            recipe.AddTile(mod.TileType("ACS"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
