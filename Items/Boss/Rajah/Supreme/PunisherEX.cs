using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Rajah.Supreme
{
    public class PunisherEX : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Avenger");
            Tooltip.SetDefault(@"The Punisher EX");
        }

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 14;
            item.useTime = 14;
            item.autoReuse = true;
            item.knockBack = 7f;
            item.width = 30;
            item.height = 10;
            item.damage = 500;
            item.shoot = Terraria.ModLoader.ModContent.ProjectileType<Projectiles.Rajah.Supreme.PunisherEX>();
            item.shootSpeed = 15f;
            item.UseSound = SoundID.Item1;
            item.rare = ItemRarityID.Cyan;
            item.expert = true; item.expertOnly = true;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.melee = true;
            item.noUseGraphic = true;
        }
    }
}