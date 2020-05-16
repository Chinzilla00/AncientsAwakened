using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Rajah
{
    public class Punisher : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Punisher");
        }

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 17;
            item.useTime = 17;
            item.autoReuse = true;
            item.knockBack = 6.5f;
            item.width = 30;
            item.height = 10;
            item.damage = 90;
            item.shoot = Terraria.ModLoader.ModContent.ProjectileType<Projectiles.Rajah.Punisher>();
            item.shootSpeed = 15f;
            item.UseSound = SoundID.Item1;
            item.rare = ItemRarityID.Yellow;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.melee = true;
            item.noUseGraphic = true;
        }
    }
}