using Terraria.ID;

namespace AAMod.Items.Magic        //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class BogBomb : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 90; //Projectile Damage
            item.magic = true; //It's a magic tome
            item.mana = 12; //It will use that much
            item.width = 8;
            item.height = 8;
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.noMelee = true; //Why would you hit anyone with a book?
            item.knockBack = 4;
            item.value = 10000;
            item.rare = 2;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("BogOrb");
            item.shootSpeed = 8f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bog Bomb");
            Tooltip.SetDefault("Fires an explosive bomb that inflicts venom upon whatever it strikes");
        }
    }
}