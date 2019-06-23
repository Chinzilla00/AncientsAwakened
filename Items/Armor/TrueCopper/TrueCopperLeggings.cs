using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.TrueCopper
{
    [AutoloadEquip(EquipType.Legs)]
	public class TrueCopperLeggings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Copper Greaves");
			Tooltip.SetDefault(@"10% increased damage
+12% Melee Speed
25% reduced ammo consumption
'And you thought copper was worthless.'");

		}

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            Texture2D texture2 = Main.itemTexture[item.type];
            spriteBatch.Draw(texture2, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(texture, position, null, Main.DiscoColor, 0, origin, scale, SpriteEffects.None, 0f);
            }
            return false;
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
            item.expert = true;
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