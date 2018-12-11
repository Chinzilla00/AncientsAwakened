using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Infinity
{
    [AutoloadEquip(EquipType.Legs)]
	public class InfinityGreaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Infinity Slayer Greaves");
			Tooltip.SetDefault(@"30% increased damage
60% increased movement speed
25% decreased ammo consumption
+3 minion slots
14% increased damage resistance
Infinite power and malice flows through this armor");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 16;
			item.value = 3000000;
			item.defense = 29;
		}

		public override void UpdateEquip(Player player)
		{
            player.rangedDamage *= 1.3f;
            player.thrownDamage *= 1.3f;
            player.minionDamage *= 1.3f;
            player.meleeDamage *= 1.3f;
            player.magicDamage *= 1.3f;
            player.moveSpeed *= 1.6f;
			player.ammoCost75 = true;
			player.endurance *= 1.14f;
            player.maxMinions += 3;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(158, 3, 32);
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DoomsdayLeggings", 1);
            recipe.AddIngredient(null, "Infinitium", 14);
            recipe.AddTile(null, "BinaryReassembler");
            recipe.SetResult(this);
            recipe.AddRecipe();
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
    }
}