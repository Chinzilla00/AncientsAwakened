using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tablet
{
    /*public class LoreTablet1 : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Tablets/LoreTablet"; } }


        public static int frameWidth = 24, frameHeight = 32;
        public int frameTimer = 0;
        public int frameCount = 10;
        public Rectangle frame;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fulgarian Data Log #001");
            Tooltip.SetDefault(@"Contact with the world of terraria has been made.
We will be documenting all of the strange anomolies detected here on this small planet
First step; Establish a base.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6,11));
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 32;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = Main.itemTexture[item.type];
            Texture2D texture2 = AAMod.instance.GetTexture("Items/Tablets/LoreTablet_Glow");
            BaseDrawing.DrawTexture(spriteBatch, texture, 0, item.position, item.width, item.height, scale, rotation, 0, 11, frame, lightColor);
            BaseDrawing.DrawTexture(spriteBatch, texture2, 0, item.position, item.width, item.height, scale, rotation, 0, 11, frame, Color.Purple);
            return false;
        }

        // Same as above but for drawing inside the player's inventory
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            TabletMethods.TabletDrawInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale, item, Color.Purple);
            return false;
        }

        public override void AddRecipes()
        {
            TabletMethods.TabletRecipes(mod, this);
        }
    }

    

    public class TabletMethods
    {
        public static int frameWidth = 24, frameHeight = 32;
        public int frameTimer = 0;
        public int frameCount = 10;
        public Rectangle frame;
        public static Texture2D tex = null;
        public static Texture2D glowTex = null;
        
        public static void TabletDrawInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale, Item item, Color GlowColor)
        {
            Texture2D texture = Main.itemTexture[item.type];
            Texture2D texture2 = AAMod.instance.GetTexture("Items/Tablets/LoreTablet_Glow");
            for (int i = 0; i < 4; i++)
            {
                Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture, position + offsetPositon, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
                spriteBatch.Draw(texture2, position + offsetPositon, null, GlowColor, 0, origin, scale, SpriteEffects.None, 0f);
            }
        }

        public static void TabletRecipes(Mod mod, ModItem tablet)
        {
            ModRecipe recipe = new ModRecipe(mod);
            int DataBank = mod.TileType<Tiles.DataBank>();
            {
                recipe.AddIngredient(tablet);
                recipe.AddIngredient(mod, "InfernoGrass", 1);
                recipe.AddTile(DataBank);
                recipe.SetResult(tablet);
                recipe.AddRecipe();
            }
            {
                recipe.AddIngredient(tablet);
                recipe.AddIngredient(mod, "MireGrass", 1);
                recipe.AddTile(DataBank);
                recipe.SetResult(tablet);
                recipe.AddRecipe();
            }
            {
                recipe.AddIngredient(tablet);
                recipe.AddIngredient(mod, "DragonScale", 1);
                recipe.AddTile(DataBank);
                recipe.SetResult(tablet);
                recipe.AddRecipe();
            }
            {
                recipe.AddIngredient(tablet);
                recipe.AddIngredient(mod, "MirePod", 1);
                recipe.AddTile(DataBank);
                recipe.SetResult(tablet);
                recipe.AddRecipe();
            }
            {
                recipe.AddIngredient(tablet);
                recipe.AddIngredient(mod, "DragonClaw", 1);
                recipe.AddTile(DataBank);
                recipe.SetResult(tablet);
                recipe.AddRecipe();
            }
            {
                recipe.AddIngredient(tablet);
                recipe.AddIngredient(mod, "HydraClaw", 1);
                recipe.AddTile(DataBank);
                recipe.SetResult(tablet);
                recipe.AddRecipe();
            }
            {
                recipe.AddIngredient(tablet);
                recipe.AddIngredient(mod, "InfernoGrass", 1);
                recipe.AddTile(DataBank);
                recipe.SetResult(tablet);
                recipe.AddRecipe();
            }
            {
                recipe.AddIngredient(tablet);
                recipe.AddIngredient(mod, "InfernoGrass", 1);
                recipe.AddTile(DataBank);
                recipe.SetResult(tablet);
                recipe.AddRecipe();
            }
        }
    }*/

}