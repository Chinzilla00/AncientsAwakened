using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.BossSummons
{
    public class IntimidatingMushroom : ModItem
    {
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {

            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Items/BossSummons/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            item.glowMask = customGlowMask;
            DisplayName.SetDefault("Intimidating Looking Mushroom");
            Tooltip.SetDefault(@"Summons the Mushroom Monarch
Can only be used during the day");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 22;
            item.maxStack = 20;
            item.value = 1000;
            item.rare = 1;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return Main.dayTime && !NPC.AnyNPCs(mod.NPCType("MushroomMonarch"));
        }

        public override bool UseItem(Player player)
        {
            if (Main.netMode != 1)
            {
                NPC.NewNPC((int)player.position.X + Main.rand.Next(-400, 400), (int)player.position.Y + Main.rand.Next(-600, -250), mod.NPCType("MushroomMonarch"));
                NetMessage.SendData(23, -1, -1, null, mod.NPCType("MushroomMonarch"), 0f, 0f, 0f, 0);
            }
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Mushroom, 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}