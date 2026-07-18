using Celeste.Mod.Celeste_Multiworld;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Celeste.Mod.ItemToggle.UI
{
    public class ToggleUIManager : DrawableGameComponent
    {
        private float OffsetX = Celeste.ViewWidth / 16f;
        private float OffsetY = Celeste.ViewHeight / 16f;
        private int Padding = 8;
        private int TitleHeight = 96;
        private int ItemMargin = 72;
        private int KeyMenuWidth = 400;
        private float RenderScale = 1f;

        private int NumRows, NumCols = 0;
        public (int r, int c) SelectedItem = (0, 0);
        public int SelectedKey = 0;

        private MenuContext Context;

        private readonly Color ActiveColor = Calc.HexToColor("30B335");
        private readonly Color SelectBorderColor = Calc.HexToColor("FF8000");
        private readonly Color SelectBodyColor = Calc.HexToColor("45283C");
        private readonly float FontSize = ActiveFont.BaseSize; // 64px

        private readonly Dictionary<long,string> APKeyAPToID = new Dictionary<long, string>();

        private static int Count = 0;

        public ToggleUIManager(Game game) : base(game)
        {
            game.Components.Add(this);
            Visible = false;

            NumRows = ToggleUIItemData.ItemGrid.GetLength(0);
            NumCols = ToggleUIItemData.ItemGrid.GetLength(1);

            var apItemIDToString = (Dictionary<long,string>) typeof(Celeste_MultiworldModule).Assembly.GetType("Celeste.Mod.Celeste_Multiworld.Items.APItemData").GetProperty("ItemIDToString",BindingFlags.Static | BindingFlags.Public).GetValue(null);
            APKeyAPToID = (Dictionary<long,string>) typeof(Celeste_MultiworldModule).Assembly.GetType("Celeste.Mod.Celeste_Multiworld.Locations.APLocationData").GetProperty("KeyAPToID",BindingFlags.Static | BindingFlags.Public).GetValue(null);

            ToggleUIItemData.KeyMenu.SetActive = (_) => OpenKeyMenu();
            ToggleUIItemData.KeyList = APKeyAPToID.Select(e => {
                string name = apItemIDToString[e.Key];
                if (apItemIDToString[e.Key].Contains(" - ")) name = apItemIDToString[e.Key].Split(" - ")[1];
                return new ToggleableItem(e.Key,name,Celeste_MultiworldModule.SaveData.KeyItems);
            }).ToList();
        }

        private enum MenuContext
        {
            ItemSelect,
            KeySelect
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!Enabled) {
                Visible = false;
                return;
            }

            bool disabled = MInput.Disabled;
            MInput.Disabled = false;
            // UI Controls
            if (Visible)
            {
                switch (Context)
                {
                    case MenuContext.ItemSelect:
                        if (Visible)
                        {
                            if (Input.MenuRight.Pressed)
                            {
                                do SelectedItem.c = (SelectedItem.c + 1) % NumCols;
                                while (ToggleUIItemData.ItemGrid[SelectedItem.r, SelectedItem.c] == null);
                            }
                            if (Input.MenuLeft.Pressed)
                            {
                                do SelectedItem.c = (SelectedItem.c + NumCols - 1) % NumCols;
                                while (ToggleUIItemData.ItemGrid[SelectedItem.r, SelectedItem.c] == null);
                            }
                            if (Input.MenuUp.Pressed)
                            {
                                do SelectedItem.r = (SelectedItem.r + NumRows - 1) % NumRows;
                                while (ToggleUIItemData.ItemGrid[SelectedItem.r, SelectedItem.c] == null);
                            }
                            if (Input.MenuDown.Pressed)
                            {
                                do SelectedItem.r = (SelectedItem.r + 1) % NumRows;
                                while (ToggleUIItemData.ItemGrid[SelectedItem.r, SelectedItem.c] == null);
                            }
                            if (Input.MenuConfirm.Pressed)
                            {
                                var item = ToggleUIItemData.ItemGrid[SelectedItem.r, SelectedItem.c];
                                item?.SetActive(!item.GetActive());
                            }
                            if (ItemToggleModule.Settings.LockItem.Pressed)
                            {
                                var item = ToggleUIItemData.ItemGrid[SelectedItem.r, SelectedItem.c];
                                if (item != null) item.IsLocked = !item.IsLocked;
                            }
                        }
                        break;
                    case MenuContext.KeySelect:
                        if (Input.MenuCancel.Pressed)
                        {
                            Context = MenuContext.ItemSelect;
                            Input.MenuCancel.ConsumePress();
                        }
                        if (Input.MenuUp.Pressed)
                        {
                            int numKeys = GetKeysInArea().Count;
                            SelectedKey = (SelectedKey + numKeys - 1) % numKeys;
                        }
                        if (Input.MenuDown.Pressed)
                        {
                            int numKeys = GetKeysInArea().Count;
                            SelectedKey = (SelectedKey + 1) % numKeys;
                        }
                        if (Input.MenuConfirm.Pressed)
                        {
                            var key = GetKeysInArea()[SelectedKey];
                            Logger.Info("item",$"{key.Name}: {key.GetActive()}");
                            key.SetActive(!key.GetActive());
                        }
                        break;
                }
            }
            if (ItemToggleModule.Settings.OpenItemToggleMenu.Pressed || (Visible && Context == MenuContext.ItemSelect && Input.MenuCancel.Pressed))
            {
                Visible = !Visible;
                disabled = Visible;
                Input.MenuCancel.ConsumePress();

                // Resize UI
                OffsetX = Celeste.ViewWidth / 16;
                OffsetY = Celeste.ViewHeight / 16;
                if (Celeste.ViewWidth <= 1280) RenderScale = 0.5f;
                else RenderScale = 1f;
            }
            
            MInput.Disabled = disabled;
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Visible) return;

            float width   = 2 * Padding + ItemMargin * NumCols;
            float height  = 3 * Padding + ItemMargin * NumRows + TitleHeight;

            base.Draw(gameTime);
            Monocle.Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            // Background
            DrawScaledRect(OffsetX, OffsetY, width, height, new Color(0.1f, 0.1f, 0.1f, 0.75f), RenderScale);

            // Title
            DrawScaledRect(OffsetX + Padding, OffsetY + Padding, width - 2 * Padding, TitleHeight, new Color(0.1f, 0.1f, 0.1f, 0.5f), RenderScale);
            ActiveFont.Draw("Item Toggle Menu", RenderScale*new Vector2(OffsetX + width / 2, OffsetY + Padding + TitleHeight / 2), new Vector2(0.5f), new Vector2(RenderScale), Color.White);
            Monocle.Draw.SpriteBatch.End();

            // Grid
            Monocle.Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            for (int r = 0; r < NumRows; r++)
            {
                for (int c = 0; c < NumCols; c++)
                {
                    ToggleableItem item = ToggleUIItemData.ItemGrid[r, c];
                    if (item == null) continue;

                    float X = OffsetX + Padding + c * ItemMargin;
                    float Y = OffsetY + 2 * Padding + r * ItemMargin + TitleHeight;

                    // Selection highlight
                    if (SelectedItem.Equals((r, c)))
                    {
                        DrawScaledRect(X  , Y  , ItemMargin  , ItemMargin  , SelectBorderColor, RenderScale);
                        DrawScaledRect(X+2, Y+2, ItemMargin-4, ItemMargin-4, SelectBodyColor, RenderScale);
                    }
                    // Active highlight
                    if (item.GetActive())
                    {
                        if (!(item.ItemID > 0xCA10080 && item.ItemID < 0xCA1008A)) // Dash direction
                        {
                            DrawScaledRect(X+2, Y+2, ItemMargin-4, ItemMargin-4, ActiveColor, RenderScale);
                        }
                    }
                    // Red highlight on key if there are no keys in the current level
                    if (item.Name == "key" && GetKeysInArea().Count == 0)
                    {
                        DrawScaledRect(X+2, Y+2, ItemMargin-4, ItemMargin-4, Color.DarkRed, RenderScale);
                    }

                    X += ItemMargin / 2;
                    Y += ItemMargin / 2;
                    MTexture tex = GFX.Game[$"ToggleIcons/{item.Name}"];
                    Color color = Color.White;
                    if (item.GetActive() && item.ItemID > 0xCA10080 && item.ItemID < 0xCA1008A) // Dash direction
                    {
                        color = Color.Magenta;
                    }
                    tex.DrawCentered(new Vector2(RenderScale*X, RenderScale*Y+0.01f), color, 2f*RenderScale); // Small shift to vertical texcoord for 1:1 pixel rendering

                    if (item.IsLocked) { // Locked item graphic
                        MTexture lockTex = GFX.Game[$"ToggleIcons/lock"];
                        lockTex.DrawCentered(new Vector2(RenderScale*X, RenderScale*Y+0.01f), Color.White, RenderScale);
                    }
                }
            }
            Monocle.Draw.SpriteBatch.End();

            // Key select menu
            Monocle.Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            if (Context == MenuContext.KeySelect)
            {
                var keys = GetKeysInArea();

                float fontSizeMod = 0.5f;
                float topLeftX = OffsetX+width-KeyMenuWidth;
                float topLeftY = OffsetY+height+Padding;
                float keyMenuHeight = keys.Count * (fontSizeMod*FontSize+16) + 2*Padding;
                DrawScaledRect(topLeftX, topLeftY, KeyMenuWidth, keyMenuHeight, new Color(0.1f, 0.1f, 0.1f, 0.75f), RenderScale);

                topLeftX += Padding;
                topLeftY += Padding;
                float Y = 0;

                for (int i = 0; i < keys.Count; i++)
                {
                    // Selection highlight
                    if (SelectedKey == i)
                    {
                        DrawScaledRect(topLeftX  , topLeftY+Y  , KeyMenuWidth-2*Padding  , FontSize*fontSizeMod+8, SelectBorderColor, RenderScale);
                        DrawScaledRect(topLeftX+2, topLeftY+Y+2, KeyMenuWidth-2*Padding-4, FontSize*fontSizeMod+4, SelectBodyColor  , RenderScale);
                    }

                    // Active highlight
                    if (keys[i].GetActive())
                    {
                       DrawScaledRect(topLeftX+2, topLeftY+Y+2, KeyMenuWidth-2*Padding-4, FontSize*fontSizeMod+4, ActiveColor, RenderScale);
                    }

                    ActiveFont.Draw(keys[i].Name, RenderScale*new Vector2(topLeftX+Padding,topLeftY+Y+4), new Vector2(0f), new Vector2(fontSizeMod*RenderScale), Color.White);

                    Y += FontSize*fontSizeMod+16;
                }
            }
            Monocle.Draw.SpriteBatch.End();
        }

        public void OpenKeyMenu()
        {
            if (SaveData.Instance.CurrentSession_Safe == null) return;
            if (GetKeysInArea().Count == 0) return;

            Context = MenuContext.KeySelect;
            SelectedKey = 0;
        }

        private List<ToggleableItem> GetKeysInArea()
        {
            if (SaveData.Instance.CurrentSession_Safe == null) return new List<ToggleableItem>();
            string currentMapID = $"{SaveData.Instance.CurrentSession_Safe.Area.ID}_{(int)SaveData.Instance.CurrentSession_Safe.Area.Mode}";
            return ToggleUIItemData.KeyList.Where(k => APKeyAPToID[k.ItemID].StartsWith(currentMapID)).ToList();
        }

        private static void DrawScaledRect(float x, float y, float width, float height, Color color, float scale)
        {
            Monocle.Draw.Rect(scale*x,scale*y,scale*width,scale*height,color);
        }
    }
}
