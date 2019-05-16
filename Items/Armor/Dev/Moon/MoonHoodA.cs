using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;

namespace AAMod.Items.Armor.Dev.Moon
{
    [AutoloadEquip(EquipType.Head)]
	public class MoonHoodA : ModItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Lunar Mage Hood");
            Tooltip.SetDefault(@"24% increased Magic damage & critical strike chance
+200 Max Mana
15% decreased mana consumption");

        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(159, 207, 190);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 20;
            item.rare = 9;
            item.defense = 34;
        }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage += .24f;
            player.magicCrit += 24;
            player.statManaMax2 += 200;
            player.manaCost -= .15f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("MoonRobeA") && legs.type == mod.ItemType("MoonBootsA");
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = @"'Stings, doesn't it?'
You glow like the moon in the sky
Magic attacks inflict Moonraze on your target
You have a lunar friend to assist you";
            
            player.GetModPlayer<AAPlayer>(mod).MoonSet = true;
            
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(mod.BuffType("MB")) == -1)
                {
                    player.AddBuff(mod.BuffType("MB"), 3600, true);
                }
                if (player.ownedProjectileCounts[mod.ProjectileType("MoonBeeMinion")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, mod.ProjectileType("MoonBeeMinion"), 100, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MoonHood", 1);
            recipe.AddIngredient(null, "EXArmor", 1);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}