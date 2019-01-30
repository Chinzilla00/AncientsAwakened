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
    public class PowerStone : ModItem
    {
        

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Power Stone");
            Tooltip.SetDefault(
@"Multiplies your attack power by 2.25
'Fun isn’t something one considers when balancing the universe'");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 8));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.width = 66;
            item.height = 78;
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
                    line2.overrideColor = Color.Purple;
                }
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage *= 2.25f;
            player.rangedDamage *= 2.25f;
            player.magicDamage *= 2.25f;
            player.thrownDamage *= 2.25f;
            player.minionDamage *= 2.25f;
        }
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AAPlayer>().Power = true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.Amethyst, 10);
                recipe.AddIngredient(ItemID.LargeAmethyst, 1);
                recipe.AddIngredient(ItemID.AvengerEmblem, 1);
                recipe.AddIngredient(ItemID.FragmentNebula, 15);
                recipe.AddIngredient(ItemID.FragmentSolar, 15);
                recipe.AddIngredient(ItemID.FragmentVortex, 15);
                recipe.AddIngredient(ItemID.FragmentStardust, 15);
                recipe.AddIngredient(ItemID.SoulofMight, 30);
                recipe.AddIngredient(null, "DarkMatter", 10);
                recipe.AddIngredient(null, "RadiumBar", 10);
                recipe.AddTile(null, "QuantumFusionAccelerator");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
        public bool CanEquipAccessory(Item item, Player player, int slot)
        {
            if (item.type == mod.ItemType("PowerStone"))
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