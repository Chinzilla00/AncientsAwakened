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
    public class SunStorm : BaseAAItem
  {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sunstorm");
			Tooltip.SetDefault(@"Summons orbiting fireballs which home to enemies after some time");
        }

        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.mana = 15;
            item.useStyle = 5;
            item.damage = 145;
            item.useAnimation = 30;
            item.useTime = 30;
            item.width = 40;
            item.height = 40;
            item.shoot = mod.ProjectileType("SunstormFireball");
            item.shootSpeed = 20f;
            item.knockBack = 4.5f;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.magic = true;
            item.rare = 9;
            AARarity = 13;
            item.noMelee = true;
            item.UseSound = new LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound);
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
                }
            }
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
            bool AnyOrbiters = AAGlobalProjectile.AnyProjectiles(Terraria.ModLoader.ModContent.ProjectileType<Projectiles.Akuma.SunstormFireball>());
            int SummonCount = 2;
            if (AnyOrbiters)
            {
                SummonCount = 1;
            }
            for (int Loops = 0; Loops < 4; Loops++)
            {
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, Main.myPlayer, 0, 0);
            }

            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DaybreakIncinerite", 5);
            recipe.AddIngredient(null, "CrucibleScale", 5);
            recipe.AddIngredient(ItemID.LunarFlareBook, 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
