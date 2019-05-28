using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.TrueHallowed
{
    [AutoloadEquip(EquipType.Head)]
    public class TrueHallowedHelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Holy Hallowed Helm");
            Tooltip.SetDefault(@"15% increased damage and critical strike chance
Increases maximum mana by 100
10% increased melee speed");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 20;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 7;
            item.defense = 28;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("TrueHallowedPlate") && legs.type == mod.ItemType("TrueHallowedGreaves");
        }
        

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"20% Chance not to Consume Ammo, reduced mana usage, increased melee, and movement speed";

            player.meleeSpeed *= 1.2f;
            player.ammoCost80 = true;
            player.moveSpeed *= 1.2f;
            player.manaCost *= 0.8f;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage *= 1.15f;
            player.meleeCrit += 15;
            player.statManaMax2 += 100;
            player.meleeSpeed *= 1.1f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedHelmet, 1);
            recipe.AddIngredient(ItemID.HallowedMask, 1);
            recipe.AddIngredient(ItemID.HallowedHeadgear, 1);
            recipe.AddIngredient(null, "HallowCrystal", 1);
            recipe.AddTile(null, "PaladinsSmeltery");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D Glow = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw(Glow, position, null, AAColor.Hallow, 0, origin, scale, SpriteEffects.None, 0f);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                AAColor.Hallow,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}