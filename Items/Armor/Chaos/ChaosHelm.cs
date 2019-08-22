using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Chaos
{
    [AutoloadEquip(EquipType.Head)]
	public class ChaosHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chaos Helmet");
            Tooltip.SetDefault(@"15% increased melee damage and speed");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;
            item.defense = 28;
        }
		
		public override void UpdateEquip(Player player)
		{
           player.meleeSpeed += 0.15f;
            player.meleeDamage *= 1.15f;

        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("ChaosDou") && legs.type == mod.ItemType("ChaosGreaves");
        }

        int counter = 0;
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"You are immune to most debuffs
You quickly regenerate your HP while standing still
You are immune to knockback";
            player.buffImmune[46] = true;
            player.buffImmune[33] = true;
            player.buffImmune[36] = true;
            player.buffImmune[30] = true;
            player.buffImmune[20] = true;
            player.buffImmune[32] = true;
            player.buffImmune[31] = true;
            player.buffImmune[35] = true;
            player.buffImmune[23] = true;
            player.buffImmune[22] = true;
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
                        player.statLife += 2;
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
			recipe.AddIngredient(mod.ItemType("TrueRaiderHelm"));
			recipe.AddIngredient(null, "ChaosCrystal", 1);
            recipe.AddTile(null, "TruePaladinsSmeltery");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}