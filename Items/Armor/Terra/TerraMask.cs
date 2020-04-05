using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Terra
{
    [AutoloadEquip(EquipType.Head)]
    public class TerraMask : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Mask");
            Tooltip.SetDefault(@"9% Increased Minion damage");
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
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("TerraPlate") && legs.type == mod.ItemType("TerraGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.DemonHoodBonus");
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
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
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DemonHood", 1);
            recipe.AddIngredient(null, "TerraCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}