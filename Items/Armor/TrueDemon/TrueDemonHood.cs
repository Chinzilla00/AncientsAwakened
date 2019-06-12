using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueDemon
{
    [AutoloadEquip(EquipType.Head)]
    public class TrueDemonHood : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Devil Cowl");
            Tooltip.SetDefault(@"13% Increased Minion damage
Increases your max number of minions by 2");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.value = 700000;
            item.rare = 8;
            item.defense = 10;
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
            player.setBonus = @"Your minions bathe your enemies in shadowflame
You Always have an Imp army by your side
Imp army doesn't affect minion count";
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            modPlayer.trueDemonBonus = true;
            modPlayer.trueDemon = true;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(mod.BuffType("DevilBuff")) == -1)
                {
                    player.AddBuff(mod.BuffType("DevilBuff"), 3600, true);
                }
                if (player.ownedProjectileCounts[mod.ProjectileType("ImpSlave")] < 5)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, mod.ProjectileType("ImpSlave"), 40, 0f, Main.myPlayer, 0f, 0f);
                }
            }
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