using Terraria.ID;

namespace AAMod.Items.Magic
{
    public class GunkWand : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gunk Wand");
        }

        public override void SetDefaults()
        {
            item.damage = 20;
            item.magic = true;
            item.mana = 6;
            item.width = 36;
            item.height = 38;
            item.useTime = 28;
            item.useAnimation = 28;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = 1000;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Gunk");
            item.shootSpeed = 4f;
        }
    }
}