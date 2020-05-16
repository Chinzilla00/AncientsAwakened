using Terraria;
using Terraria.ID;

namespace AAMod.Items.Magic
{
    public class TerraFocus : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Focus");
            Tooltip.SetDefault(@"Fires shots of terra magic at your foes");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 12;
            item.useTime = 4;
            item.reuseDelay = item.useAnimation + 6;
            item.shootSpeed = 14f;
            item.knockBack = 6f;
            item.width = 16;
            item.height = 16;
            item.damage = 50;
            item.UseSound = SoundID.Item9;
            item.crit = 20;
            item.shoot = Terraria.ModLoader.ModContent.ProjectileType<Projectiles.MagicBlastF>();
            item.mana = 14;
            item.rare = ItemRarityID.LightRed;
            item.value = 300000;
            item.noMelee = true;
            item.magic = true;
            item.autoReuse = true;
        }
    }
}