using Terraria.ID;

namespace AAMod.Items.Ranged
{
    public class SingularityCannon : BaseAAItem
    {

        public override void SetDefaults()
        {
            item.damage = 55;
            item.noMelee = true;
            item.ranged = true;
            item.width = 36;
            item.height = 64;
            item.useTime = 40;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = Terraria.ModLoader.ModContent.ProjectileType<Projectiles.Singularity>();
            item.knockBack = 5;
            item.value = Terraria.Item.sellPrice(0, 8, 0, 0);
            item.rare = ItemRarityID.Purple;
            item.UseSound = SoundID.Item12;
            item.autoReuse = true;
            item.shootSpeed = 22f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Singularity Cannon");
            Tooltip.SetDefault("");
        }
    }
}
