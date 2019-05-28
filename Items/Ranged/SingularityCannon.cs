using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class SingularityCannon : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 100;
            item.noMelee = true;
            item.ranged = true;
            item.width = 36;
            item.height = 64;
            item.useTime = 40;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.shoot = mod.ProjectileType<Projectiles.Singularity>();
            item.knockBack = 5;
            item.value = 1000;
            item.rare = 11;
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
