using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Equinox
{
    public class Equinox : ModItem
    {

        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Equinox");
            Tooltip.SetDefault(
@"Gives immensely increased stats
'True balance'");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 11;
            item.accessory = true;
            item.expert = true;
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

        public override void UpdateEquip(Player player)
        {
            player.lifeRegen += 6;
            player.statDefense += 9;
            player.meleeSpeed += 0.35f;
            player.meleeDamage += 0.35f;
            player.meleeCrit += 5;
            player.rangedDamage += 0.35f;
            player.rangedCrit += 5;
            player.magicDamage += 0.35f;
            player.magicCrit += 5;
            player.pickSpeed -= 0.35f;
            player.minionDamage += 0.35f;
            player.minionKB += 0.75f;
            player.thrownDamage += 0.35f;
            player.thrownCrit += 5;
        }
        

		public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.nightVision = true;
            player.GetModPlayer<AAPlayer>(mod).RStar = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RadiantStar", 1);
            recipe.AddIngredient(null, "DarkVoid", 1);
            recipe.AddIngredient(null, "DarkMatter", 20);
            recipe.AddIngredient(null, "RadiumBar", 20);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}