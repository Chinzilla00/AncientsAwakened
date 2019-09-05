using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueTribal
{
    [AutoloadEquip(EquipType.Head)]
    public class TrueTribalHat : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Druid Hood");
            Tooltip.SetDefault(@"10% Increased magic damage
Increases Maximum Mana by 60");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.value = 90000;
            item.rare = 8;
            item.defense = 10;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("TrueTribalCloak") && legs.type == mod.ItemType("TrueTribalKilt");
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = @"You are blessed with a gift of nature, allowing you to autodrink mana potions
Mana usage lowered by 40%
The jungle gives you protection, causing deadly spores to spawn around you";
            player.sporeSac = true;
            player.manaCost *= 0.6f;
            player.manaFlower = true;
            player.SporeSac();
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 60;
            player.magicDamage *= 1.10f;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "TribalHat", 1);
                recipe.AddIngredient(null, "PlanteraPetal", 6);
                recipe.AddIngredient(ItemID.JungleSpores, 10);
                recipe.AddTile(null, "TruePaladinsSmeltery");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}