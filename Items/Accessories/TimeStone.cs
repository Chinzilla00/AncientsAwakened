using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Face)]
    public class TimeStone : ModItem
    {
        

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Time Stone");
            Tooltip.SetDefault(
@"Respawn time cut by 80%
hitting enemies causes time for them to slow
'Dread it. Run from it. Destiny still arives.'");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 16));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.width = 54;
            item.height = 52;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.rare = 11;
            item.accessory = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Color.Green;
                }
            }
        }

		public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AAPlayer>().Time = true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.Emerald, 10);
                recipe.AddIngredient(ItemID.LargeEmerald, 1);
                recipe.AddIngredient(null, "TimeTeller", 1);
                recipe.AddIngredient(ItemID.FragmentNebula, 15);
                recipe.AddIngredient(ItemID.FragmentSolar, 15);
                recipe.AddIngredient(ItemID.FragmentVortex, 15);
                recipe.AddIngredient(ItemID.FragmentStardust, 15);
                recipe.AddIngredient(ItemID.SoulofSight, 30);
                recipe.AddIngredient(null, "DarkMatter", 10);
                recipe.AddIngredient(null, "RadiumBar", 10);
                recipe.AddTile(null, "QuantumFusionAccelerator");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
        public bool CanEquipAccessory(Item item, Player player, int slot)
        {
            if (item.type == mod.ItemType("TimeStone"))
            {
                if (slot < 10) // This allows the accessory to equip in Vanity slots with no reservations.
                {
                    int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                    for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                    {
                        // We need "slot != i" because we don't care what is currently in the slot we will be replacing.
                        if (slot != i && player.armor[i].type == mod.ItemType<InfinityGauntlet>())
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}