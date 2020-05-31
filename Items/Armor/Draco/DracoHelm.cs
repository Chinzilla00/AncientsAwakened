using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Draco
{
    [AutoloadEquip(EquipType.Head)]
	public class DracoHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Draconian Sun Kabuto");
			Tooltip.SetDefault(@"20% increased melee critical chance
3% increased damage resistance
+25 Max Life
The blazing fury of the Inferno rests in this armor");

		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 22;
			item.value = 3000000;
			item.defense = 38;
            item.rare = ItemRarityID.Cyan;
            AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity13;
                }
            }
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
			player.meleeCrit += 20;
            player.endurance += .03f;
            player.statLifeMax2 += 25;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("DracoPlate") && legs.type == mod.ItemType("DracoLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAMod.Common.DracoHelmBonus");

            player.buffImmune[46] = true;
            player.buffImmune[47] = true;
            player.AddBuff(BuffID.Shine, 2);
            player.GetModPlayer<AAPlayer>().dracoSet = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DaybreakIncinerite", 15);
            recipe.AddIngredient(null, "CrucibleScale", 5);
            recipe.AddIngredient(null, "KindledKabuto", 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}