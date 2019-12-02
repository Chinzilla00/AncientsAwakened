using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Akuma
{
    public class YOTD : BaseAAItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Year of the Dragon");
            Tooltip.SetDefault(@"Fires dazzling fireworks to amaze your friends and blow your enemies away!
'Akuma brand fireworks is not responsible for damage done by shooting fireworks point-blank into enemies' faces.'");
        }

        public override void SetDefaults()
        {
            item.crit = 14;
            item.useStyle = 5;
            item.autoReuse = true;
            item.useAnimation = 25;
            item.useTime = 25;
            item.useAmmo = AmmoID.Rocket;
            item.width = 50;
            item.height = 20;
            item.shoot = mod.ProjectileType("YotD");
            item.UseSound = SoundID.Item11;
            item.damage = 600;
            item.shootSpeed = 30f;
            item.noMelee = true;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.knockBack = 2f;
            item.rare = 9;
            AARarity = 13;
            item.ranged = true;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
                }
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float num72 = item.shootSpeed;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num78 = Main.mouseX + Main.screenPosition.X - vector2.X;
            float num79 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
            if (player.gravDir == -1f)
            {
                num79 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
            }
            float num80 = (float)Math.Sqrt((num78 * num78) + (num79 * num79));
            float num81 = num80;
            if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
            {
                num78 = player.direction;
                num79 = 0f;
                num80 = num72;
            }
            else
            {
                num80 = num72 / num80;
            }
            num78 *= num80;
            num79 *= num80;
            int num112 = 3;
            for (int num113 = 0; num113 < num112; num113++)
            {
                vector2 = new Vector2(player.position.X + (player.width * 0.5f) + Main.rand.Next(201) * -(float)player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - - 600f);
                vector2.X = ((vector2.X + player.Center.X) / 2f) + Main.rand.Next(-200, 201);
                vector2.Y -= 100 * num113;
                num78 = Main.mouseX + Main.screenPosition.X - vector2.X + (Main.rand.Next(-40, 41) * 0.03f);
                num79 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
                if (num79 < 0f)
                {
                    num79 *= -1f;
                }
                if (num79 < 20f)
                {
                    num79 = 20f;
                }
                num80 = (float)Math.Sqrt((num78 * num78) + (num79 * num79));
                num80 = num72 / num80;
                num78 *= num80;
                num79 *= num80;
                float num114 = num78;
                float num115 = num79 + (Main.rand.Next(-40, 41) * 0.02f);
                Projectile.NewProjectile(vector2.X, vector2.Y, num114 * 0.75f, num115 * -0.75f, type, damage/2, knockBack, player.whoAmI, 0f, -0.5f + ((float)Main.rand.NextDouble() * 0.3f));
            }
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DaybreakIncinerite", 5);
            recipe.AddIngredient(null, "CrucibleScale", 5);
            recipe.AddIngredient(ItemID.FireworksLauncher);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
