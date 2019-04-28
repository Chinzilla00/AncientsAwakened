using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;
using BaseMod;
using Terraria.Localization;

namespace AAMod.Items.BossSummons
{
    //imported from my tAPI mod because I'm lazy
    public class CyberneticBell : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cybernetic Bell");
            Tooltip.SetDefault(@"A carefully tinkered bell
Summons the Raider Ultima
Can only be used at night");
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 38;
            item.maxStack = 20;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.consumable = true;
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

        public override bool UseItem(Player player)
        {
            AAModGlobalNPC.SpawnBoss(mod, player, "Raider", true, 0, 0, "Raider Ultima");
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The bell rings, but nothing happens.", Color.Purple.R, Color.Purple.G, Color.Purple.B, false);
                return false;
            }
            if (NPC.AnyNPCs(mod.NPCType("Raider")))
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The Raider hears the bell and keeps attempting to kill you", Color.Purple.R, Color.Purple.G, Color.Purple.B, false);
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "IncineriteBar", 6);
                recipe.AddRecipeGroup("AAMod:Iron", 6);
                recipe.AddIngredient(null, "SoulOfSmite", 6);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this, 1);
                recipe.AddRecipe();
            }
        }
    }
}