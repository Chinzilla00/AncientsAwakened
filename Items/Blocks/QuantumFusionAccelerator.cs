using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Blocks
{
    public class QuantumFusionAccelerator : ModItem
    {
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != 2)
            {
                Microsoft.Xna.Framework.Graphics.Texture2D[] glowMasks = new Microsoft.Xna.Framework.Graphics.Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Items/Blocks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            item.glowMask = customGlowMask;
            DisplayName.SetDefault("Quantum Fusion Accelerator");
            Tooltip.SetDefault(
@"Launching particles at the speed of sound
Allows you to work with DarkMatter and Radium");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 150;
            item.createTile = mod.TileType("QuantumFusionAccelerator");
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.LunarCraftingStation, 1);
                recipe.AddIngredient(null, "DarkmatterOre", 30);
                recipe.AddIngredient(null, "DarkEnergy", 15);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
