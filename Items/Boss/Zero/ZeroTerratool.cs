using AAMod.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class ZeroTerratool : ModItem
    {
        public static bool AxeBool = false;
        public static bool PickBool = false;
        public static bool HammerBool = false;
        


        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Terratool");
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {

            item.damage = 40;
            item.melee = true;
            item.width = 54;
            item.height = 60;
            item.useTime = 5;
            item.useAnimation = 20;
            item.axe = 300;
            item.pick = 300;
            item.tileBoost += 20;
            item.hammer = 0;
            item.useStyle = 1;
            item.knockBack = 3;
            item.value = 1000000;
            item.rare = 11;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            
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
                    line2.overrideColor = new Color(120, 0, 30);
                }
            }
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddIngredient(ItemID.LunarBar, 20);
            recipe.AddTile(null, "BinaryReassembler");
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }

        public override bool AltFunctionUse(Player player)
        {
            if (!TerratoolUI.visible)
            {
                Main.PlaySound(SoundID.MenuOpen);
                TerratoolUI.visible = true;
                AAMod.instance.UserInterface.SetState(AAMod.instance.TerratoolUI);
            }
            else
            {
                Main.PlaySound(SoundID.MenuClose);
                TerratoolUI.visible = false;
                AAMod.instance.UserInterface.SetState(null);
            }
            return true;
        }
    }
}
