using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using Terraria.Audio;

namespace AAMod.Items.Boss.Shen
{
    public class Skyfall : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skyfall");
            Tooltip.SetDefault("UNRELEASED");
        }

        public override void SetDefaults()
        {
            item.damage = 400;
            item.ranged = true;
            item.width = 22;
            item.height = 50;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.noMelee = true;
            item.channel = true;
            item.knockBack = 5f;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Skyfall");
            item.shootSpeed = 14f;
            
            item.UseSound = new LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound);
        }


        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float num72 = item.shootSpeed;
            type = Main.rand.Next(3);

            switch (type)
            {
                case 0:
                    type = mod.ProjectileType("Skyfall");
                    break;
                case 1:
                    type = mod.ProjectileType("SkyfallR");
                    break;
                default:
                    type = mod.ProjectileType("SkyfallB");
                    break;
            }
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num78 = Main.mouseX + Main.screenPosition.X - vector2.X;
            float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
            if (player.gravDir == -1f)
            {
                num79 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
            }
            float num80 = (float)Math.Sqrt((double)((num78 * num78) + (num79 * num79)));
            float num81 = num80;
            if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
            {
                num78 = (float)player.direction;
                num79 = 0f;
                num80 = num72;
            }
            else
            {
                num80 = num72 / num80;
            }
            num78 *= num80;
            num79 *= num80;
            vector2 = new Vector2(player.position.X + ((float)player.width * 0.5f) + (float)(Main.rand.Next(201) * -(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
            vector2.X = ((vector2.X + player.Center.X) / 2f) + (float)Main.rand.Next(-200, 201);
            num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X + ((float)Main.rand.Next(-40, 41) * 0.03f);
            num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
            if (num79 < 0f)
            {
                num79 *= -1f;
            }
            if (num79 < 20f)
            {
                num79 = 20f;
            }
            num80 = (float)Math.Sqrt((double)((num78 * num78) + (num79 * num79)));
            num80 = num72 / num80;
            num78 *= num80;
            num79 *= num80;
            float num114 = num78;
            float num115 = num79 + ((float)Main.rand.Next(-40, 41) * 0.02f);
            Projectile.NewProjectile(vector2.X, vector2.Y, num114 * 0.75f, num115 * 0.75f, type, damage, knockBack, player.whoAmI, 0f, 0.5f + ((float)Main.rand.NextDouble() * 0.3f));
            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Shen;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RadiantDawn", 1);
            recipe.AddIngredient(null, "FallingTwilight", 1);
            recipe.AddIngredient(null, "ChaosScale", 5);
            recipe.AddIngredient(null, "Discordium", 5);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}