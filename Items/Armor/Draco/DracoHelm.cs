using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System.Collections.Generic;


namespace AAMod.Items.Armor.Draco
{
    [AutoloadEquip(EquipType.Head)]
	public class DracoHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Draconian Sun Kabuto");
			Tooltip.SetDefault(@"18% increased melee and magic critical chance
10% increased damage resistance
100 increased maximum mana
The blazing fury of the Inferno rests in this armor");

		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 22;
			item.value = 3000000;
			item.defense = 37;
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
			player.meleeCrit += 18;
			player.magicCrit += 18;
            player.statManaMax2 += 100;
            player.endurance *= 1.1f;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Akuma;
                }
            }
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("DracoPlate") && legs.type == mod.ItemType("DracoLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			
			player.setBonus = @"'Blazing fury consumes you'
You are immune to all ice-related debuffs
You glow like the blazing fire in your soul
Your Melee and Magic attacks inflict Daybreak on your target";

            player.buffImmune[46] = true;
            player.buffImmune[47] = true;
            player.AddBuff(BuffID.Shine, 2);
            player.GetModPlayer<AAPlayer>(mod).dracoSet = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DaybreakIncinerite", 15);
            recipe.AddIngredient(null, "CrucibleScale", 5);
            recipe.AddIngredient(null, "KindledKabuto", 1);
            recipe.AddTile(null, "BinaryReassembler");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}