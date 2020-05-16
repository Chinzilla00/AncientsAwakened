using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class Blastshot : BaseAAItem
    {
        
        public override void SetDefaults()
        {
            item.damage = 50;
            item.noMelee = true;
            item.ranged = true;
            item.width = 62;
            item.height = 24;
            item.useTime = 7;
            item.useAnimation = 22;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAmmo = AmmoID.Gel;
            item.shoot = ModContent.ProjectileType<Projectiles.DragonfireProj>();
            item.knockBack = 0;
            item.value = 100000;
            item.rare = ItemRarityID.Pink;
            item.UseSound = SoundID.Item34;
            item.autoReuse = true;
            item.shootSpeed = 14f;
        }
		
		public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blastshot");
            Tooltip.SetDefault("Consumes Gel");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DragonFire", 5);
            recipe.AddIngredient(null, "IncineriteBar", 10);
            recipe.AddIngredient(null, "SoulOfSmite", 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
