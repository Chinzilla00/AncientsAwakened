using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Sagittarius
{
    public class NeutronStaff : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 30;
            item.magic = true;
            item.width = 38;
            item.height = 38;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = 10000;
            item.rare = 2;
            item.mana = 2;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType ("SagStar");
            item.shootSpeed = 7f;
        }   

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Neutron Rod");
            Tooltip.SetDefault("Fires spinning stars that bounce on walls");
            Item.staff[item.type] = true;
        }
    }
}
