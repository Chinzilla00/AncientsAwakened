using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueDeathly
{
    [AutoloadEquip(EquipType.Head)]
    public class TrueDeathlySkull : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deathly Ghast Skull");
            Tooltip.SetDefault("14% Increased ranged damage and Critical Strike chance");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 34;
            item.value = 90000;
            item.rare = 8;
            item.defense = 17;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage *= 1.14f;
            player.rangedCrit += 14;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("TrueDeathlyRibguard") && legs.type == mod.ItemType("TrueDeathlyGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = @"You are as quiet as death itself, making enemies less likely to target you
25% Reduced Ammo Consumption
Being killed causes your spirit to materialize, reviving you
While Etheral, you have more invincibility frames, but your defense is reduced by 10";

            player.aggro -= 8;
            player.ammoCost75 = true;
            player.GetModPlayer<AAPlayer>(mod).trueDeathly = true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.Ectoplasm, 20);
                recipe.AddIngredient(ItemID.Bone, 50);
                recipe.AddIngredient(null, "DeathlySkull", 1);
                recipe.AddTile(null, "TruePaladinsSmeltery");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}