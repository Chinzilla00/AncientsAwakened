using Terraria;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Boss
{
    public class StormCharm : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm Charm");
            Tooltip.SetDefault(@"15% increased damage and damage resistance
10% Increased melee speed
All attacks deal 20 True damage (damage unaffected by class)
Grants the ability to dash.");
        }
        public override void SetDefaults()
        {
            item.width = 58;
            item.height = 54;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 2;
            item.accessory = true;
            item.expert = true;
            item.defense = 3;
        }

        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "HoloCape", 1);
            recipe.AddIngredient(null, "StormPendant", 1);
            recipe.AddIngredient(null, "StormRiot", 1);
            recipe.AddIngredient(null, "DragonSerpentNecklace", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateEquip(Player player)
        {
            player.endurance += .15f;
            player.meleeDamage += .15f;
            player.rangedDamage += .15f;
            player.magicDamage += .15f;
            player.minionDamage += .15f;
            player.thrownDamage += .15f;
            player.GetModPlayer<AAPlayer>(mod).StormClaw = true;
            player.dash = 1;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == mod.ItemType<Broodmother.DragonCape>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<Hydra.HydraPendant>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<DragonSerpentNecklace>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<Raider.HoloCape>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<Orthrus.StormPendant>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
    
}