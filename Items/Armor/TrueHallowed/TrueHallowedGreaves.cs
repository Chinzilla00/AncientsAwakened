using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.TrueHallowed
{
	[AutoloadEquip(EquipType.Legs)]
	public class TrueHallowedGreaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Hallowed Greaves");
            Tooltip.SetDefault(@"10% increased damage
12% increased movement speed");
        }

		public override void SetDefaults()
		{
            item.width = 22;
            item.height = 18;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.defense = 14;
            item.rare = 7;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage *= 1.1f;
            player.rangedDamage *= 1.1f;
            player.magicDamage *= 1.1f;
            player.minionDamage *= 1.1f;
            player.thrownDamage *= 1.1f;
            player.moveSpeed *= 1.12f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedGreaves, 1);
            recipe.AddIngredient(null, "HallowCrystal", 1);
            recipe.AddTile(null, "PaladinsSmeltery");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D Glow = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw(Glow, position, null, AAColor.Hallow, 0, origin, scale, SpriteEffects.None, 0f);
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
                AAColor.Hallow,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}