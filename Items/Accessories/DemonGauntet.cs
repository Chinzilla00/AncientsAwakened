using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories
{

    [AutoloadEquip(EquipType.HandsOn, EquipType.HandsOff)]
    public class DemonGauntlet : ModItem
    {
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon Gauntlet");
            Tooltip.SetDefault(
@"Enemies are more likely to target you
14% Increased Melee Damage and Speed
Increased Melee Knockback
Melee Attacks Inflict a different debuff depending on your world evil
Inflicts Ichor in Crimson Worlds/Cursed Flame in Corruption worlds");
            if (Main.netMode != 2)
            {
                Microsoft.Xna.Framework.Graphics.Texture2D[] glowMasks = new Microsoft.Xna.Framework.Graphics.Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Items/Accessories/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            //
            //
        }

        public override void SetDefaults()
        {
            item.width = 45;
            item.height = 48;
            item.value = Item.sellPrice(0, 12, 0, 0);
            item.rare = 7;
            item.accessory = true;
            item.defense = 8;
            item.glowMask = customGlowMask;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += 0.14f;
            player.meleeSpeed += 0.14f;
            player.aggro += 5;
        }

        

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
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
                recipe.AddIngredient(ItemID.FleshKnuckles, 1);
                recipe.AddIngredient(ItemID.SoulofNight, 10);
                recipe.AddIngredient(ItemID.CursedFlame, 10);
                recipe.AddTile(TileID.TinkerersWorkbench);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }

    }
}