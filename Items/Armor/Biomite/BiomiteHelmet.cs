using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Biomite
{
    [AutoloadEquip(EquipType.Head)]
	public class BiomiteHelmet : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Biomite Helmet");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 20;
			item.value = 7500;
			item.rare = 2;
			item.defense = 5;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("BiomitePlate") && legs.type == mod.ItemType("BiomiteBoots");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Get the armor setbonus according to the biome you stay" + SetBonus(player);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "TerraShard", 15);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public string SetBonus(Player player)
		{
			string set = "";
			if (Main.dayTime)
			{
				player.statLifeMax2 += 20;
				set += "\nIncreases maximum life by 20";
			}
			else
			{
				player.statManaMax2 += 20;
				set += "\nIncreases maximum mana by 20";
			}
			if (player.GetModPlayer<AAPlayer>().ZoneVoid)
			{
				player.detectCreature = true;
				set += "\nYou can detect the enemies around you";
			}
			if (player.GetModPlayer<AAPlayer>().ZoneInferno)
			{
				player.buffImmune[BuffID.OnFire] = true;
				set += "\nYou immune to 'Onfire!' debuff";
			}
			if (player.GetModPlayer<AAPlayer>().ZoneMire)
			{
				player.buffImmune[BuffID.Poisoned] = true;
				set += "\nYou immune to 'Poisoned' debuff";
			}
			if (player.GetModPlayer<AAPlayer>().Terrarium)
			{
				player.statDefense += 5;
				set += "\nIncrease 5 defense";
			}
			if (player.ZoneJungle)
			{
				player.manaRegenBonus += 3;
				set += "\nIncrease your mana regenaration";
			}
			if (player.ZoneSnow)
			{
				player.buffImmune[BuffID.Chilled] = true;
				set += "\nYou immune to 'Chilled' debuff";
			}
			if (player.ZoneDesert)
			{
				player.buffImmune[BuffID.WindPushed] = true;
				set += "\nYou immune to 'WindPushed' debuff";
			}
			if (player.ZoneHoly)
			{
				player.buffImmune[BuffID.Slow] = true;
				player.lifeRegen += 3;
				set += "\nIncrease your life regenaration\nYou immune to Slow";
			}
			if (player.ZoneCorrupt)
			{
				player.moveSpeed += .1f;
				player.GetModPlayer<AAPlayer>().MaxMovespeedboost += 0.1f;
				set += "\nIncrease 10% movespeed";
			}
			if (player.ZoneCrimson)
			{
				player.armorPenetration += 5;
				set += "\nIncrease 5 armor penetration";
			}
			return set;
		}
	}
}