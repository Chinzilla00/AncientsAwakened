using Terraria;
using Terraria.ID;

namespace AAMod.Items.Melee
{
    public class HydrasSpear : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydra Spear");
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            item.damage = 15;
            item.melee = true;
            item.width = 132;
            item.height = 132;
            item.scale = 1.1f;
            item.maxStack = 1;
            item.useTime = 24;
            item.useAnimation = 18;
            item.knockBack = 2.3f;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.value = Item.sellPrice(0, 2, 40, 0);
            item.rare = ItemRarityID.Green;
            item.shootSpeed = 5f;
            item.shoot = mod.ProjectileType("HydraSpear");  //put your Spear projectile name
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
        }
    }
}
