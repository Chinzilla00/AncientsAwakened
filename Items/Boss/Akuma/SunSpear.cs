using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Akuma
{
    public class SunSpear : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Sun Partisan");
            Tooltip.SetDefault(@"One of two legendary spears used to divide time into day and night
Inflicts daybroken");
        }

        public override void SetDefaults()
        {
            item.damage =310;
            item.melee = true;
            item.width = 96;
            item.height = 96;
            item.scale = 1.1f;
            item.useTime = 17;
            item.useAnimation = 17;
            item.knockBack = 4.7f;
            item.UseSound = SoundID.Item20;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useTurn = true;
			item.autoReuse = true;
            item.useStyle = 5;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 10;
            item.shoot = mod.ProjectileType("SunSpear");  //put your Spear projectile name
            item.shootSpeed = 7f;
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


        public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
		}



        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DaybreakIncinerite", 5);
            recipe.AddIngredient(null, "CrucibleScale", 5);
            recipe.AddIngredient(ItemID.NorthPole, 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
