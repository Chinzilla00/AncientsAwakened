using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Blocks
{
    public class HaphestusForge : ModItem
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
            DisplayName.SetDefault("Hephaestus Forge");
            Tooltip.SetDefault(
@"*Slaps top of forge* This baby can fit so many crafting stations in it
Functions as a Hellforge, Hellstone Anvil, Alchemy Table, Demon Altar Tinkerer's Workshop, and a Table and Chair");
        }

        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 32;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.rare = 5;
            item.consumable = true;
            item.value = 150;
            item.createTile = mod.TileType("HaphestusForge");
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "CrimsonAltar", 1);
                recipe.AddIngredient(ItemID.Hellforge, 1);
                recipe.AddIngredient(ItemID.Bottle, 1);
                recipe.AddIngredient(ItemID.TinkerersWorkshop, 1);
                recipe.AddIngredient(ItemID.ObsidianTable, 1);
                recipe.AddIngredient(ItemID.ObsidianChair, 1);
                recipe.AddIngredient(null, "HellstoneAnvil", 1);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "CorruptAltar", 1);
                recipe.AddIngredient(ItemID.Hellforge, 1);
                recipe.AddIngredient(ItemID.Bottle, 1);
                recipe.AddIngredient(ItemID.TinkerersWorkshop, 1);
                recipe.AddIngredient(ItemID.ObsidianTable, 1);
                recipe.AddIngredient(ItemID.ObsidianChair, 1);
                recipe.AddIngredient(null, "HellstoneAnvil", 1);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "MireAltar", 1);
                recipe.AddIngredient(ItemID.Hellforge, 1);
                recipe.AddIngredient(ItemID.TinkerersWorkshop, 1);
                recipe.AddIngredient(ItemID.Bottle, 1);
                recipe.AddIngredient(ItemID.ObsidianTable, 1);
                recipe.AddIngredient(ItemID.ObsidianChair, 1);
                recipe.AddIngredient(null, "HellstoneAnvil", 1);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "InfernoAltar", 1);
                recipe.AddIngredient(ItemID.Hellforge, 1);
                recipe.AddIngredient(ItemID.TinkerersWorkshop, 1);
                recipe.AddIngredient(ItemID.Bottle, 1);
                recipe.AddIngredient(ItemID.ObsidianTable, 1);
                recipe.AddIngredient(ItemID.ObsidianChair, 1);
                recipe.AddIngredient(null, "HellstoneAnvil", 1);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
