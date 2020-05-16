using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;

namespace AAMod.Items.Armor.Witch
{
    [AutoloadEquip(EquipType.Head)]
	public class WitchHood : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Fury Witch's Cowl");
			Tooltip.SetDefault(@"+120 Max Mana
Reduced mana consumption by 20%
+2 Max Minions
10% increased magic/minion damage 
10% increased magic critical strike chance
A hood enchanted with the firey spirit of a supreme dragon acolyte");
		}

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
            item.value = 300000;
            item.rare = ItemRarityID.Cyan;
            AARarity = 12;
            item.defense = 24;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.manaCost *= .8f;
            player.magicCrit += 10;
            player.magicDamage += .1f;
            player.minionDamage += .1f;
            player.maxMinions += 2;
            player.statManaMax2 += 120;
        }


        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("WitchRobe") && legs.type == mod.ItemType("WitchBoots");
        }

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAMod.Common.WitchHoodBonus");
            player.magicDamage += .2f;
            player.minionDamage += .2f;
            player.maxMinions += 4;

            player.GetModPlayer<AAPlayer>().Witch = true;

            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(mod.BuffType("FlameSoul")) == -1)
                {
                    player.AddBuff(mod.BuffType("FlameSoul"), 3600, true);
                }
                if (player.ownedProjectileCounts[mod.ProjectileType("FlameSoul")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, mod.ProjectileType("FlameSoul"), 60, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }
        
	}
}