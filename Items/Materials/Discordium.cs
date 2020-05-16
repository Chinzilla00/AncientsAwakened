using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class Discordium : BaseAAItem
    {
        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            DisplayName.SetDefault("Discordium");
            Tooltip.SetDefault("The World Chaoses melded together into a single, powerful bar");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 9));
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity14;
                }
            }
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            item.width = 10;
            item.height = 10;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = ItemRarityID.Purple;
            item.glowMask = customGlowMask;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DaybreakIncinerite", 1);
            recipe.AddIngredient(null, "EventideAbyssium", 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.Magenta.ToVector3() * 0.55f * Main.essScale);
        }
    }
}