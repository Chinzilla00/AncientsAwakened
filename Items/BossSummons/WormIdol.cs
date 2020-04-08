using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AAMod.Items.BossSummons
{
    public class WormIdol : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Worm Idol");
            Tooltip.SetDefault(@"An ancient statue depicting some form of worm god.
It looks like it hasn't been touched in years");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.rare = 11;
        }
        public override void HoldItem(Player player)
        {
            player.GetModPlayer<IdolPointer>().effect = true;
        }
    }

    public class IdolPointer : ModPlayer
    {
        public bool effect;

        public static Vector2 AltarSpawn = new Vector2(Main.maxTilesX * 0.15f, 100);

        public override void ResetEffects()
        {
            effect = false;
        }

        public static readonly PlayerLayer AltarPointer = new PlayerLayer("AAMod", "LabLegs", PlayerLayer.Legs, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("AAMod");
            if (drawPlayer.GetModPlayer<IdolPointer>().effect && AltarSpawn.X != Main.maxTilesX * 0.15f && AltarSpawn.Y != 100)
            {
                Texture2D texture = mod.GetTexture("Items/BossSummons/WormIdol");

                int drawX = (int)(drawPlayer.position.X - Main.screenPosition.X);
                int drawY = (int)(drawPlayer.position.Y - Main.screenPosition.Y);
                Vector2 Position = drawPlayer.position;
                Vector2 origin = texture.Size() / 2;
                Vector2 pos = new Vector2((int)(Position.X - Main.screenPosition.X - drawPlayer.bodyFrame.Width / 2 + drawPlayer.width / 2), (int)(Position.Y - Main.screenPosition.Y + drawPlayer.height - drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2);
                pos.Y -= drawPlayer.mount.PlayerOffset;

                float AltarPlace = (AltarSpawn - drawPlayer.Center).ToRotation();
                DrawData data = new DrawData(texture, pos, new Rectangle(0, 0, (int)texture.Size().X, (int)texture.Size().Y), Color.White, AltarPlace, origin, 1f, 0, 0);
                //data.shader = drawInfo.legArmorShader;
                Main.playerDrawData.Add(data);
            }
        });

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            int legLayer = layers.FindIndex(PlayerLayer => PlayerLayer.Name.Equals("Wings"));
            if (legLayer != -1 && AltarSpawn.X != Main.maxTilesX * 0.15f * 16 && AltarSpawn.Y != 100)
            {
                AltarPointer.visible = true;
                layers.Insert(legLayer + 1, AltarPointer);
            }
            else
            {
                AltarPointer.visible = false;
            }
        }
    }
}