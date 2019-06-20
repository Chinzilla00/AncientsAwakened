using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.TrueRaider
{
    [AutoloadEquip(EquipType.Body)]
	public class TrueRaiderPlate : BaseAAItem
	{
		public static int counter = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Asgardian Chestplate");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice (0, 5, 0, 0);
			item.rare = 7;
			item.defense = 27;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return head.type == mod.ItemType("TrueRaiderHelm") && legs.type == mod.ItemType("TrueRaiderBoots");
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = @"You are immune to all cold-related debuffs
You quickly regenerate your HP while standing still
You are immune to knockback";
            player.buffImmune[44] = true;
			player.buffImmune[46] = true;
			player.buffImmune[47] = true;
            player.noKnockback = true;
            if (player.velocity.X == 0f && player.velocity.Y == 0f)
			{
				if (player.statLife < player.statLifeMax2)
				{
					if (counter >= 4)
					{
						counter = 0;
						player.statLife += 1;
						player.HealEffect(1, true);
					}
					counter++;
				}
			}
            else
            {
                counter = 0;
            }
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("RaiderChest"));
			recipe.AddIngredient(null, "IceCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}