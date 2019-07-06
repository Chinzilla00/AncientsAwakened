using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Starcloud
{
    [AutoloadEquip(EquipType.Head)]
    public class StarcloudHeadgear : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 22;
            item.height = 20;
            item.value = 10;
            item.rare = 3;
            item.defense = 4;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Starcloud Headgear");
      Tooltip.SetDefault("+6% minion and magic damage.");
    }


        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("StarcloudChainmail") && legs.type == mod.ItemType("StarcloudLeggings");  //put your Breastplate name and Leggings name
        }
		public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.06f;
            player.magicDamage += 0.06f;//20 max mana
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+ 100% minion and magic damage if health is below 60."; // the armor set bonus
			if (player.statLife < 120)  //this make that if you have less then 100 health your melee damage multiple for 2
            {
				player.minionDamage *= 2;
				player.magicDamage *= 2;
            }﻿
        }
        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "StarcloudBar", 15);
            recipe.AddTile(TileID.Anvils);   
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
