using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace AAMod.Items.Flasks
{
    public class VoidFlask : BaseAAItem
	{
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 26;
            item.maxStack = 999;
            item.consumable = true;
            item.useTime = 28;
            item.useAnimation = 28;
            item.shoot = mod.ProjectileType("BlackSolution");
            item.shootSpeed = 1f;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.noUseGraphic = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Flask");
            Tooltip.SetDefault(@"Spreads the Void");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(1, 4));
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                item.shoot = mod.ProjectileType("VoidFlask");
                item.shootSpeed = 9f;
            }
            else
            {
                item.shoot = mod.ProjectileType("BlackSolution");
                item.shootSpeed = 2f;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == mod.ProjectileType("Flask"))
            {
                Projectile.NewProjectile(position, new Microsoft.Xna.Framework.Vector2(speedX, speedY), type, 0, 0, Main.myPlayer, 7);
                return false;
            }
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}
