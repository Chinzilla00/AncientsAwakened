using Terraria;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.EFish
{
    public class SoapBlaster : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soap Blaster");
            Tooltip.SetDefault("Rapidly shoots destructive bubbles");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.BubbleGun);
			item.useTime = 7;
			item.useAnimation = 7;
            item.damage = 150;
            item.rare = 11;
        }
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-15, 0);
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num75 = item.shootSpeed;
			float num82 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
			float num83 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
			float num84 = (float)Math.Sqrt((double)(num82 * num82 + num83 * num83));
			if ((float.IsNaN(num82) && float.IsNaN(num83)) || (num82 == 0f && num83 == 0f))
			{
				num82 = (float)player.direction;
				num83 = 0f;
				num84 = num75;
			}
			else
			{
				num84 = num75 / num84;
			}
			num82 *= num84;
			num83 *= num84;
			for (int num179 = 0; num179 < 3; num179++)
			{
				float num180 = num82;
				float num181 = num83;
				num180 += (float)Main.rand.Next(-20, 20) * 0.1f;
				num181 += (float)Main.rand.Next(-20, 20) * 0.1f;
				Projectile.NewProjectile(vector2.X, vector2.Y, num180*2, num181*2, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}



        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BubbleGun);
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}