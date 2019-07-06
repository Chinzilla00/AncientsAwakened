using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace AAMod.Items.Dev
{
    public class FuryForgerEX : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fury Greatforger");
			Tooltip.SetDefault(@"Striking enemies causes an explosion + sparks to fly from them
Fury Forger EX");
		}
		public override void SetDefaults()
		{
			item.damage = 400;
			item.melee = true;
			item.width = 92;
			item.height = 98;
			item.useTime = 35;
			item.useAnimation = 35;
			item.useStyle = 1;
			item.knockBack = 4;
            item.value = Item.sellPrice(0, 50, 0, 0);
            item.rare = 9;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.expert = true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Forge"), player.Center);
            float spread = 45f * 0.0174f;
            double startAngle = Math.Atan2(player.velocity.X, player.velocity.Y) - spread / 2;
            double deltaAngle = spread / 12f;
            if (player.whoAmI == Main.myPlayer)
            {
                for (int i = 0; i < 6; i++)
                {
                    double offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), mod.ProjectileType("SparkFury"), item.damage, 1.25f, player.whoAmI, 0f, 1f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f), mod.ProjectileType("SparkFury"), item.damage, 1.25f, player.whoAmI, 0f, 1f);
                }
            }
            target.AddBuff(BuffID.Daybreak, 200);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FuryForger");
            recipe.AddIngredient(null, "EXSoul");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
