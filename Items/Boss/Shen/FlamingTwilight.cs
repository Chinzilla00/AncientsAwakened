using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Shen
{
    public class FlamingTwilight : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.damage = 400;
			item.ranged = true;
			item.width = 76;
			item.height = 36;
			item.useTime = 14;
			item.useAnimation = 14;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 6;
			item.UseSound = SoundID.Item34;
            item.value = Item.sellPrice(1, 50, 0, 0);
            item.rare = ItemRarityID.Cyan;
            AARarity = 14;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("DiscordianInfernoF");
			item.shootSpeed = 11f;
			item.useAmmo = AmmoID.Gel;
		}

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity14;
                }
            }
        }

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flaming Twilight");
			Tooltip.SetDefault(@"Left click to blasts a discordian fireball at your foes 
Right click to rain fire and fury at your cursor position
Consumes gel as ammo
33% chance not to consume gel");
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .33;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-8, 0);
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = mod.ProjectileType("DiscordianInfernoF");
            if (player.altFunctionUse == 2)
            {
                float num72 = item.shootSpeed;
                int num112 = 5;
                for (int num113 = 0; num113 < num112; num113++)
                {
                    Vector2 vector2 = new Vector2(player.position.X + (player.width * 0.5f) + (Main.rand.Next(201) * -player.direction) + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                    vector2.X = ((vector2.X + player.Center.X) / 2f) + Main.rand.Next(-200, 201);
                    vector2.Y -= 100 * num113;
                    float num78 = Main.mouseX + Main.screenPosition.X - vector2.X + (Main.rand.Next(-40, 41) * 0.03f);
                    float num79 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
                    if (num79 < 0f)
                    {
                        num79 *= -1f;
                    }
                    if (num79 < 20f)
                    {
                        num79 = 20f;
                    }
                    float num80 = (float)Math.Sqrt((num78 * num78) + (num79 * num79));
                    num80 = num72 / num80;
                    num78 *= num80;
                    num79 *= num80;
                    float num114 = num78;
                    float num115 = num79 + (Main.rand.Next(-40, 41) * 0.02f);
                    Projectile.NewProjectile(vector2.X, vector2.Y, num114 * 0.75f, num115 * 0.75f, type, damage, knockBack, player.whoAmI, 0f, 0.5f + (float)(Main.rand.NextDouble() * 0.3f));
                }
                return false;
            }
            else
            {
                float Angle = Main.rand.Next(15, 46);
                float spread = Angle * 0.0174f;
                float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
                double startAngle = Math.Atan2(speedX, speedY) - .1d;
                double deltaAngle = spread / 6f;
                double offsetAngle;
                for (int i = 0; i < 3; i++)
                {
                    offsetAngle = startAngle + (deltaAngle * i);
                    Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), item.shoot, damage, knockBack, Main.myPlayer);
                }
            }
            return false;
		}
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Discordium", 5);
            recipe.AddIngredient(null, "ChaosScale", 5);
            recipe.AddIngredient(null, "Dawnstrike");
            recipe.AddIngredient(null, "Darksprayer");
            recipe.AddTile(null, "ACS");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
