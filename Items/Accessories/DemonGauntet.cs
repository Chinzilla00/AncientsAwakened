using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;


namespace AAMod.Items.Accessories
{

    [AutoloadEquip(EquipType.HandsOn, EquipType.HandsOff)]
    public class DemonGauntlet : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon Gauntlet");
            Tooltip.SetDefault(
@"Enemies are more likely to target you
14% Increased Melee Damage
10% Increased melee speed
Increased Melee Knockback
Melee Attacks Inflict a different debuff depending on your world evil
Inflicts Ichor in Crimson Worlds/Cursed Flame in Corruption worlds");
            
        }

        public override void SetDefaults()
        {
            item.width = 45;
            item.height = 48;
            item.value = Item.sellPrice(0, 12, 0, 0);
            item.rare = ItemRarityID.Lime;
            item.accessory = true;
            item.defense = 8;
            
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            Color GlowColor = Globals.AAColor.CursedInferno;
            if (WorldGen.crimson)
            {
                GlowColor = Globals.AAColor.Ichor;
            }
            spriteBatch.Draw
                (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                GlowColor,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
                );
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            Texture2D texture2 = Main.itemTexture[item.type];
            Color GlowColor = Globals.AAColor.CursedInferno;
            if (WorldGen.crimson)
            {
                GlowColor = Globals.AAColor.Ichor;
            }
            spriteBatch.Draw(texture2, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            for (int i = 0; i < 4; i++)
            {
                //Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture, position, null, GlowColor, 0, origin, scale, SpriteEffects.None, 0f);

            }

            return false;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamage += 0.14f;
            player.meleeSpeed += 0.1f;
            player.aggro += 5;
            player.GetModPlayer<AAPlayer>().demonGauntlet = true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.FireGauntlet, 1);
                recipe.AddIngredient(ItemID.FleshKnuckles, 1);
                recipe.AddIngredient(ItemID.SoulofNight, 10);
                recipe.AddIngredient(ItemID.Ichor, 10);
                recipe.AddTile(TileID.TinkerersWorkbench);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.FireGauntlet, 1);
                recipe.AddIngredient(ItemID.PutridScent, 1);
                recipe.AddIngredient(ItemID.SoulofNight, 10);
                recipe.AddIngredient(ItemID.CursedFlame, 10);
                recipe.AddTile(TileID.TinkerersWorkbench);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }

    }
}