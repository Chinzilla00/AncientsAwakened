using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class CodeMagnet : BaseAAItem
    {
        public bool On = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Binary Code Magnet");
			Tooltip.SetDefault(@"Pulls items to you by moving its code closer to you
Use the item to turn it on and off");
		}

        public override void SetDefaults()
        {
            item.width = item.height = 16;
            item.rare = 4;
            item.maxStack = 1;
			item.value = 8000;
        }

        public override bool UseItem(Player player)
        {
            if (On)
            {
                On = false;
            }
            else
            {
                On = true;
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DoomiteScrap", 20);
            recipe.AddIngredient(null, "Doomite", 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = On ? Main.itemTexture[item.type] : mod.GetTexture("Items/Usable/CodeMagnetOff");
            spriteBatch.Draw
                (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                lightColor,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
                );
            return false;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = On ? Main.itemTexture[item.type] : mod.GetTexture("Items/Usable/CodeMagnetOff");
            spriteBatch.Draw(texture, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}
