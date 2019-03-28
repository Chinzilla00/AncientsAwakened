using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.TrueCopper
{
    [AutoloadEquip(EquipType.Legs)]
	public class TrueCopperLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Copper Greaves");
			Tooltip.SetDefault(@"10% increased damage
+12% Melee Speed
25% reduced ammo consumption
'And you thought copper was worthless.'");

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
                Main.DiscoColor,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override void SetDefaults()
		{
			item.width = 26;
			item.height = 18;
			item.value = 3000000;
			item.defense = 11;
		}

		public override void UpdateEquip(Player player)
        {
            player.meleeDamage += .1f;
            player.rangedDamage += .1f;
            player.magicDamage += .1f;
            player.minionDamage += .1f;
            player.thrownDamage += .1f;
            player.meleeSpeed += .12f;
            player.ammoCost75 = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CopperGreaves);
            recipe.AddIngredient(null, "Crystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}