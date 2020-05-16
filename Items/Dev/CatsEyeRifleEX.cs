using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Dev
{
    public class CatsEyeRifleEX : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Silencer");
            Tooltip.SetDefault(@"Fires Shadow bolts
Doesn't require ammo
Cat's Eye Rifle EX");
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

        public override void SetDefaults()
        {
            item.damage = 1750; 
            item.noMelee = true;
            item.ranged = true;
            item.width = 86; 
            item.height = 22; 
            item.useTime = 30; 
            item.useAnimation = 30;  
            item.useStyle = ItemUseStyleID.HoldingOut; 
            item.shoot = mod.ProjectileType("CatsEye");
            item.knockBack = 12; 
            item.value = Item.sellPrice(3, 0, 0, 0);
            item.autoReuse = true; 
            item.shootSpeed = 25f; 
            item.crit = 5;
            item.expert = true; item.expertOnly = true;
            item.rare = ItemRarityID.Red;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_GUN; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

		
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CatsEyeRifle");
            recipe.AddIngredient(null, "EXSoul");
            recipe.SetResult(this);
            recipe.AddRecipe(); 
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ArchwitchStaff");
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}