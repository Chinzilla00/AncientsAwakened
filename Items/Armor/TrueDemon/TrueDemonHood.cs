using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueDemon
{
    [AutoloadEquip(EquipType.Head)]
    public class TrueDemonHood : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Demon Cowl");
            Tooltip.SetDefault(@"13% Increased Minion damage
Increases your max number of minions by 2");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.value = 700000;
            item.rare = 8;
            item.defense = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.13f;
            player.maxMinions += 2;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("TrueDemonGarb") && legs.type == mod.ItemType("TrueDemonBoots");
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = "Your minions bathe your enemies in shadowflame \n" + "You Always have a small Imp servant by your side";
            Projectile.NewProjectile(player.position, new Microsoft.Xna.Framework.Vector2(0, 0), ProjectileID.FlyingImp, 0, 0f, Main.myPlayer, 0f, 0f);
            player.GetModPlayer<AAPlayer>(mod).trueDemon = true;
            player.impMinion = true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "DemonHood", 1);
                recipe.AddIngredient(null, "PureEvil", 2);
                recipe.AddIngredient(null, "DevilSilk", 7);
                recipe.AddTile(null, "TruePaladinsSmeltery");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}