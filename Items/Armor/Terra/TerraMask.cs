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
            item.rare = ItemRarityID.Lime;
            item.defense = 18;
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
            player.setBonus = Language.GetTextValue("Mods.AAMod.Equipset.TerraMaskBonus");
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            modPlayer.TerraSu = true;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.ownedProjectileCounts[mod.ProjectileType("TerraCrystal")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, mod.ProjectileType("TerraCrystal"), (int)(60 * player.minionDamage), 0f, Main.myPlayer, 0f, 0f);
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