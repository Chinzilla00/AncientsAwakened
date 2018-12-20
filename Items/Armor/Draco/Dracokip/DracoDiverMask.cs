using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Armor.Draco.Dracokip
{
    [AutoloadEquip(EquipType.Head)]
	public class DracoDiverMask : ModItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Draconian Diver Mask");
            Tooltip.SetDefault(@"So I heard you like mudkips
'Great for impersonating Ancients Awakened Devs!'");

        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 20;
            item.value = 3000000;
            item.defense = 37;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeCrit += 18;
            player.magicCrit += 18;
            player.endurance *= 1.1f;
            player.statManaMax2 += 100;
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
            return body.type == mod.ItemType("DracoDiverJacket") && legs.type == mod.ItemType("DracoDiverBoots");
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = @"You are immune to all ice-related debuffs
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
            recipe.AddIngredient(null, "DracoHelm", 1);
            recipe.AddIngredient(null, "FishDiverMask", 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}