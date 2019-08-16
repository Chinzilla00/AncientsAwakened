using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Heartstone
{
    [AutoloadEquip(EquipType.Head)]
    public class HeartstoneHelmet : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 22;
            item.height = 20;
            item.value = 10;
            item.rare = 3;
            item.defense = 6;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Heartstone Headgear");
      Tooltip.SetDefault(@"+10 Health
Its forged with heart, no really");
    }


        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("HeartstoneChestguard") && legs.type == mod.ItemType("HeartstoneLeggings");  //put your Breastplate name and Leggings name
        }
		public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 10;   //20 max mana
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Doubles damage when health is below 40"; // the armor set bonus
			if (player.statLife < 40)  //this make that if you have less then 100 health your melee damage multiple for 2
            {
                player.allDamage *= 2;
            }﻿
        }
        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LifeCrystal, 2);
            recipe.AddRecipeGroup("AAMod:Gold", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
