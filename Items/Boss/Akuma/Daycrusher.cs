using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Akuma
{
    public class Daycrusher : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Daycrusher");
            Tooltip.SetDefault(@"Slams into foes with the force of a solar mass
Inflicts Daybroken");
        }

        public override void SetDefaults()
        {
            item.width = 54;
            item.height = 44;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.noMelee = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 40;
            item.useTime = 40;
            item.reuseDelay = 10;
            item.knockBack = 7.5F;
            item.damage = 400;
            item.noUseGraphic = true;
            item.shoot = mod.ProjectileType("Daycrusher");
            item.shootSpeed = 20F;
            item.UseSound = SoundID.Item20;
            item.melee = true;
            item.rare = ItemRarityID.Cyan;
            AARarity = 13;
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
            recipe.AddIngredient(null, "DaybreakIncinerite", 5);
            recipe.AddIngredient(null, "CrucibleScale", 5);
            recipe.AddIngredient(ItemID.Flairon, 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}