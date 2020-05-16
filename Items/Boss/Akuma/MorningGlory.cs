using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Akuma
{
    public class MorningGlory : BaseAAItem
    {

        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Morning Glory");
            Tooltip.SetDefault("Splits into 4 additional phantom piercing spears on throw");
        }

        public override void SetDefaults()
        {

            item.shoot = mod.ProjectileType("MorningGlory");
            item.shootSpeed = 24f;
            item.damage = 197;
            item.knockBack = 4f;
            item.melee = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item20;
            item.useAnimation = 19;
            item.useTime = 19;
            item.width = 30;
            item.height = 30;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = ItemRarityID.Cyan;
            AARarity = 13;
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

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DayBreak);
            recipe.AddIngredient(null, "DaybreakIncinerite", 5);
            recipe.AddIngredient(null, "CrucibleScale", 5);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
