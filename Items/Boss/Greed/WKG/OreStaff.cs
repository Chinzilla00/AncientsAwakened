using Terraria.ID;

namespace AAMod.Items.Boss.Greed.WKG
{
    public class OreStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ore Staff");
        }

        public override void SetDefaults()
        {
            item.damage = 160;
            item.magic = true;
            item.mana = 10;
            item.width = 38;
            item.height = 44;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = 1000000;
            item.rare = 11;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("OreBomb");
            item.useTime = 25;
            item.useAnimation = 25;
            item.shootSpeed = 12;
        }
    }
}
