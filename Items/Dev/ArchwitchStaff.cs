using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace AAMod.Items.Dev
{
    public class ArchwitchStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Archwitch's Galactic Scepter");
            Tooltip.SetDefault(@"The staff of the Dragon Queen
Left-click to spin the scepter, firing off stars at nearby enemies
Right click to fire explosive magic bolts");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 300;
            item.magic = true;
            item.width = 102;
            item.height = 100;
            item.useTime = 10;
            item.useAnimation = 10;
            item.channel = true;
            item.useStyle = 5;
            item.knockBack = 6f;
            item.value = Item.buyPrice(0, 40, 0, 0);
            item.rare = 11;                  
            item.shoot = mod.ProjectileType("ArchwitchStaff");
            item.noUseGraphic = true;
            item.noMelee = true;
            item.expert = item.expertOnly = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.staff[item.type] = true;
                item.shoot = mod.ProjectileType("ArchwitchStorm");
                item.shootSpeed = 12;
                item.noUseGraphic = false;
                item.channel = false;
                item.autoReuse = true;
                item.useTime = 5;
                item.useAnimation = 15;
                item.UseSound = new LegacySoundStyle(2, 105, Terraria.Audio.SoundType.Sound);
            }
            else
            {
                Item.staff[item.type] = false;
                item.shoot = mod.ProjectileType("ArchwitchStaff");
                item.shootSpeed = 0f;
                item.noUseGraphic = true;
                item.channel = true;
                item.autoReuse = false;
                item.useTime = 6;
                item.useAnimation = 6;
                item.UseSound = SoundID.Item1;
            }
            return base.CanUseItem(player);
        }
    }
}