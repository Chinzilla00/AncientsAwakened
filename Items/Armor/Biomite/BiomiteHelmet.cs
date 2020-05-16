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
			item.rare = ItemRarityID.Green;
			item.defense = 5;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("BiomitePlate") && legs.type == mod.ItemType("BiomiteBoots");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Lang.BiomiteArmor("BiomiteArmor1") + SetBonus(player);
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
				set += Lang.BiomiteArmor("BiomiteArmor2");
			}
			else
			{
				player.statManaMax2 += 20;
				set += Lang.BiomiteArmor("BiomiteArmor3");
			}
			if (player.GetModPlayer<AAPlayer>().ZoneVoid)
			{
				player.detectCreature = true;
				set += Lang.BiomiteArmor("BiomiteArmor4");
			}
			if (player.GetModPlayer<AAPlayer>().ZoneInferno)
			{
				player.buffImmune[BuffID.OnFire] = true;
				set += Lang.BiomiteArmor("BiomiteArmor5");
			}
			if (player.GetModPlayer<AAPlayer>().ZoneMire)
			{
				player.buffImmune[BuffID.Poisoned] = true;
				set += Lang.BiomiteArmor("BiomiteArmor6");
			}
			if (player.GetModPlayer<AAPlayer>().Terrarium)
			{
				player.statDefense += 5;
				set += Lang.BiomiteArmor("BiomiteArmor7");
			}
			if (player.ZoneJungle)
			{
				player.manaRegenBonus += 3;
				set += Lang.BiomiteArmor("BiomiteArmor8");
			}
			if (player.ZoneSnow)
			{
				player.buffImmune[BuffID.Chilled] = true;
				set += Lang.BiomiteArmor("BiomiteArmor9");
			}
			if (player.ZoneDesert)
			{
				player.buffImmune[BuffID.WindPushed] = true;
				set += Lang.BiomiteArmor("BiomiteArmor10");
			}
			if (player.ZoneHoly)
			{
				player.buffImmune[BuffID.Slow] = true;
				player.lifeRegen += 3;
				set += Lang.BiomiteArmor("BiomiteArmor11");
			}
			if (player.ZoneCorrupt)
			{
				player.moveSpeed += .1f;
				player.GetModPlayer<AAPlayer>().MaxMovespeedboost += 0.1f;
				set += Lang.BiomiteArmor("BiomiteArmor12");
			}
			if (player.ZoneCrimson)
			{
				player.armorPenetration += 5;
				set += Lang.BiomiteArmor("BiomiteArmor13");
			}
			return set;
		}
	}
}