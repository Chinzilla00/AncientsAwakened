using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Demon
{
    [AutoloadEquip(EquipType.Head)]
    public class DemonHood : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon Cowl");
            Tooltip.SetDefault(@"9% Increased Minion damage
+2 Minion slots");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 20;
            item.value = 9000;
            item.rare = 4;
            item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.09f;
            player.maxMinions += 2;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("DemonGarb") && legs.type == mod.ItemType("DemonBoots");
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Lang.ArmorBonus("DemonHoodBonus");
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            modPlayer.impSet = true;
            modPlayer.demonBonus = true;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(mod.BuffType("DemonBuff")) == -1)
                {
                    player.AddBuff(mod.BuffType("DemonBuff"), 3600, true);
                }
                if (player.ownedProjectileCounts[mod.ProjectileType("ImpMinion")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, mod.ProjectileType("ImpMinion"), 20, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "ImpHood", 1);
                recipe.AddIngredient(ItemID.NecroHelmet, 1);
                recipe.AddIngredient(ItemID.JungleHat, 1);
                recipe.AddIngredient(ItemID.CrimsonHelmet, 1);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "ImpHood", 1);
                recipe.AddIngredient(ItemID.NecroHelmet, 1);
                recipe.AddIngredient(ItemID.JungleHat, 1);
                recipe.AddIngredient(ItemID.ShadowHelmet, 1);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}