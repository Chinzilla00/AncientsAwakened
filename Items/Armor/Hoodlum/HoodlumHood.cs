using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace AAMod.Items.Armor.Hoodlum
{
    [AutoloadEquip(EquipType.Head)]
    public class HoodlumHood : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hopping Hoodlum Hood");
            Tooltip.SetDefault(@"18% increased melee & minion Damage
Enemies are more likely to target you
Hopping Mad.");
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = ItemRarityID.Yellow;
            item.defense = 13;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("HoodlumShirt") && legs.type == mod.ItemType("HoodlumPants");
		}

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.HoodlumHoodBonus");
            if (player.statLife <= player.statLifeMax2 * .5f)
            {
                player.moveSpeed += .5f;
                player.minionDamage += .5f;
                player.meleeDamage += .5f;
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += .18f;
            player.minionDamage += .18f;
            player.aggro += 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RajahPelt", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}