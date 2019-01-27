using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Blocks
{
    public class HaphestusForge : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hephaestus Forge");
            Tooltip.SetDefault(
@"*Slaps top of forge* This baby can fit so many crafting stations in it
Functions as a Hellforge, Hellstone Anvil, Alchemy Table, Demon Altar, Tinkerer's Workshop, and a Table and Chair");
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
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "CrimsonAltar", 1);
                recipe.AddIngredient(ItemID.Hellforge, 1);
                recipe.AddIngredient(ItemID.Bottle, 1);
                recipe.AddIngredient(ItemID.TinkerersWorkshop, 1);
                recipe.AddIngredient(ItemID.WoodenTable);
                recipe.AddIngredient(ItemID.WoodenChair, 1);
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
                recipe.AddIngredient(ItemID.WoodenTable);
                recipe.AddIngredient(ItemID.WoodenChair, 1);
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
                recipe.AddIngredient(ItemID.WoodenTable);
                recipe.AddIngredient(ItemID.WoodenChair, 1);
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
                recipe.AddIngredient(ItemID.WoodenTable);
                recipe.AddIngredient(ItemID.WoodenChair, 1);
                recipe.AddIngredient(null, "HellstoneAnvil", 1);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
