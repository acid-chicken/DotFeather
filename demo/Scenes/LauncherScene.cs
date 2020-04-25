using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;

namespace DotFeather.Demo
{
public class LauncherScene : Scene
{
    public override void OnStart(Router router, GameBase game, System.Collections.Generic.Dictionary<string, object> args)
    {
        BackgroundColor = Color.FromArgb(255, 32, 32, 32);
        var titleText = new TextDrawable("DotFeather", 56, FontStyle.Normal, Color.White)
        {
            Location = new Vector(24, 24),
        };
        Root.Add(titleText);

        var sampleProgramText = new TextDrawable($"Demo {DemoOS.VERSION}", 24, FontStyle.Normal, Color.White)
        {
            Location = new Vector(24 + titleText.Width + 8, 50),
        };

        Root.Add(sampleProgramText);

        this.router = router;

        Root.Add(listView);
        listView.ItemSelected += ItemSelected;
        listView.Location = new Vector(16, titleText.Location.Y + titleText.Height + 16);

        ChangeDirectory(DemoOS.CurrentDirectory);
    }

    public override void OnUpdate(Router router, GameBase game, DFEventArgs e)
    {
        Title = $"DotFeather Demo - {DemoOS.CurrentDirectory.Name.ToUpperInvariant()}";
        listView.Width = game.Width - 32;
        listView.Height = game.Height - 16 - (int)listView.Location.Y;
    }

    public void ItemSelected(int i, ListViewItem item)
    {
        var parent = DemoOS.CurrentDirectory.Parent;
        if (parent != null && i == 0)
        {
            ChangeDirectory(parent);
        }
        else
        {
            var el = DemoOS.CurrentDirectory.Files[parent != null ? i - 1 : i];
            switch (el)
            {
            case Folder f:
                ChangeDirectory(f);
                break;
            case SceneFile f:
                router?.ChangeScene(f.Scene);
                break;
            }
        }
    }

    public void ChangeDirectory(Folder folder)
    {
        DemoOS.CurrentDirectory = folder;
        listView.BeginUpdating();
        listView.Items.Clear();
        if (folder.Parent != null)
        {
            listView.Items.Add(new ListViewItem("← ..", folder.Parent.Name.ToUpperInvariant()));
        }

        folder.Files.ForEach(el => listView.Items.Add(el switch
    {
        Folder folder => new ListViewItem(folder.Name.ToUpperInvariant()),
                   SceneFile file => new ListViewItem(file.Name.ToUpperInvariant(), file.Description[CultureInfo.CurrentCulture.TwoLetterISOLanguageName]),
            _ => new ListViewItem(el.Name),
        }));
        listView.EndUpdating();
    }

    private readonly ListView listView = new ListView();

    private Router? router;

    public class ListView : Container
    {
        public ObservableCollection<ListViewItem> Items {
            get;
        } = new ObservableCollection<ListViewItem>();

        public int ItemHeight
        {
            get => itemHeight;
            set
            {
                ItemHeight = value;
                UpdateList();
            }
        }

        public int Padding
        {
            get => padding;
            set
            {
                padding = value;
                UpdateList();
            }
        }

        public ListView(IEnumerable<ListViewItem>? items = null)
        {
            this.IsTrimmable = true;
            if (items != null)
                Items = new ObservableCollection<ListViewItem>(items);
            Items.CollectionChanged += (_, __) => UpdateList();
            var t = Texture2D.CreateSolid(Color.FromArgb(24, 24, 24), 1, 1);
            backdrop = new Sprite(t)
            {
                ZOrder = -4
            };
            inner = new Container();
            Add(backdrop);
            Add(inner);
        }

        public void BeginUpdating() => isUpdating = true;

        public void EndUpdating()
        {
            if (isUpdating)
            {
                isUpdating = false;
                UpdateList();
            }
        }

        public override void OnUpdate(GameBase game)
        {
            base.OnUpdate(game);

            backdrop.Width = Width;
            backdrop.Height = Height;

            var (mx, my) = DFMouse.Position;
            var (x, y) = Location;

            // 範囲外なら無視
            if (!Intersects(DFMouse.Position, Location, Location + new Vector(Width, Height))) return;

            var innerY = inner.Location.Y;

            if (landingPoint == null)
            {
                innerY += DFMouse.Scroll.Y * 1.5f;
            }

            if (DFMouse.IsLeftDown)
            {
                landingPoint = DFMouse.Position;
                landingScrollY = (int)inner.Location.Y;
            }
            if (landingPoint is Vector v)
            {
                // 内部でマウスを押下している状態
                innerY = landingScrollY + (my - v.Y);
                if (DFMouse.IsLeftUp)
                {
                    landingPoint = null;
                    if (v.Distance(DFMouse.Position) < 2)
                    {
                        for (var i = 0; i < Items.Count; i++)
                        {
                            var elHeight = ItemHeight + padding + 16;
                            var ely = y + i * elHeight + padding + inner.Location.Y;
                            if (ely <= my && my <= ely + elHeight)
                                ItemSelected?.Invoke(i, Items[i]);
                        }
                    }
                }
            }

            if (innerY < -(padding + (ItemHeight + padding + 16) * Items.Count) + Height)
                innerY = -(padding + (ItemHeight + padding + 16) * Items.Count) + Height;

            if (innerY > 0)
                innerY = 0;

            inner.Location = new Vector(inner.Location.X, innerY);
        }

        private bool Intersects(Vector point, Vector topLeft, Vector bottomRight)
        {
            var (px, py) = point;
            var (tlx, tly) = topLeft;
            var (brx, bry) = bottomRight;

            return tlx <= px && tly <= py && px <= brx && py <= bry;
        }

        private void UpdateList()
        {
            if (isUpdating)
                return;

            inner.Clear();
            var y = padding;
            foreach (var item in Items)
            {
                if (item.Icon is Texture2D ico)
                {
                    var icon = new Sprite(ico)
                    {
                        Location = new Vector(padding, ItemHeight),
                        Width = ItemHeight,
                        Height = ItemHeight
                    };

                    inner.Add(icon);
                }
                var text = new TextDrawable(item.Text, ItemHeight, FontStyle.Normal, Color.White)
                {
                    Location = new Vector(padding + ItemHeight + padding, y)
                };
                inner.Add(text);

                y += ItemHeight;

                if (item.Description != null)
                {
                    y += 4;
                    var desc = new TextDrawable(item.Description, 12, FontStyle.Normal, Color.LightGray)
                    {
                        Location = new Vector(text.Location.X, y)
                    };
                    inner.Add(desc);
                    y += 12;
                }
                else
                {
                    y += 16;
                }
                y += padding;
            }
        }
        public event ItemSelectedEventHandler? ItemSelected;

        private int padding = 8;
        private Vector? landingPoint;
        private int landingScrollY;
        private bool isUpdating = false;
        private readonly Sprite backdrop;
        private readonly int itemHeight = 24;
        private readonly Container inner;

        public delegate void ItemSelectedEventHandler(int index, ListViewItem item);
    }

    public class ListViewItem
    {
        public Texture2D? Icon {
            get;
            set;
        }
        public string Text {
            get;
            set;
        }
        public string? Description {
            get;
            set;
        }

        public ListViewItem(string text, string? description = null, Texture2D? icon = null) => (Text, Icon, Description) = (text, icon, description);
    }
}
}
