using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Boss.Akuma
{
    public class YOTD : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Year of the Dragon");
            Tooltip.SetDefault(@"Fires dazzling fireworks to amaze your friends and blow your enemies away!
'Akuma brand fireworks is not responsible for damage done by shooting fireworks point-blank into enemies' faces.'");
        }

        public override void SetDefaults()
        {
            item.crit = 20;
            item.useStyle = 5;
            item.autoReuse = true;
            item.useAnimation = 22;
            item.useTime = 22;
            item.useAmmo = AmmoID.Rocket;
            item.width = 50;
            item.height = 20;
            item.shoot = 134;
            item.UseSound = SoundID.Item11;
            item.damage = 155;
            item.shootSpeed = 15f;
            item.noMelee = true;
            item.value = Item.sellPrice(1, 0, 0, 0);
            item.knockBack = 2f;
            item.rare = 10;
            item.ranged = true;
        }


        /*public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
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
        }*/

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num81 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
            float num82 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
            for (int num211 = 0; num211 < 2; num211++)
            {
                float num212 = num81;
                float num213 = num82;
                num212 += (float)Main.rand.Next(-40, 41) * 0.05f;
                num213 += (float)Main.rand.Next(-40, 41) * 0.05f;
                Vector2 vector22 = vector2 + Vector2.Normalize(new Vector2(num212, num213).RotatedBy((double)(-1.57079637f * (float)player.direction), default(Vector2))) * 6f;
                Projectile.NewProjectile(vector22.X, vector22.Y, num212, num213, 167 + Main.rand.Next(4), item.damage, item.knockBack, Main.myPlayer, 0f, 1f);
            }
            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Akuma;
                }
            }
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
