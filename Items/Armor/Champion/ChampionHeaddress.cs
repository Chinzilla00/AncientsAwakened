using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;

namespace AAMod.Items.Armor.Champion
{
    [AutoloadEquip(EquipType.Head)]
    public class ChampionHeaddress : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Champion Headdress");
            Tooltip.SetDefault(@"70% increased minion damage
10% increased non-minion damage
+7 maximum Minions
+2 maximum sentries 
The armor of a champion feared across the land");
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = Item.sellPrice(3, 0, 0, 0);
            item.rare = ItemRarityID.Cyan;
            AARarity = 14;
            item.defense = 27;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity14;
                }
            }
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("ChampionChestplate") && legs.type == mod.ItemType("ChampionGreaves");
		}


        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAMod.Equipset.ChampionHeaddressBonus");
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            modPlayer.Baron = true;
            modPlayer.ChampionSu = true;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(mod.BuffType("BaronBuff")) == -1)
                {
                    player.AddBuff(mod.BuffType("BaronBuff"), 3600, true);
                }
                if (player.ownedProjectileCounts[mod.ProjectileType("BaronBunny")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, mod.ProjectileType("BaronBunny"), 100, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }


        public override void UpdateEquip(Player player)
        {
            player.minionDamage += .6f;
            player.allDamage += .1f;
            player.maxMinions += 7;
            player.maxTurrets += 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "HoodlumHood", 1);
            recipe.AddIngredient(null, "ChampionPlate", 10);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}