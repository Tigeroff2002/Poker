using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Poker
{
    public partial class Form1 : Form
    {
        public string pathFront = $"{Environment.CurrentDirectory}\\cards\\";
        public string pathBack = $"{Environment.CurrentDirectory}\\cards\\back.jpg";
        public const int count_desk = 54;
        public const int count_arm = 5;
        public static Bitmap[] icons { get; set; }
        public static Bitmap backIcon { get; set; }
        public static bool[] img_exist { get; set; }
        public List<Card> player_cards { get; set; }
        public List<Card> bot_cards { get; set; }
        public Form1()
        {
            InitializeComponent();
            this.Text = "Покер";
            this.Width = 1920;
            this.Height = 1080;
            this.Visible = true;
            this.BackColor = Color.Green;
        }
        public void ThrowCards()
        {
            SetOfCards botSet = new SetOfCards();
            botSet.cards = new List<Card>();
            SetOfCards playerSet = new SetOfCards();
            playerSet.cards = new List<Card>();
            bot_cards = new List<Card>();
            player_cards = new List<Card>();
            icons = new Bitmap[count_desk];
            img_exist = new bool[count_desk];
            for (int i = 0; i < count_desk; i++)
            {
                try
                {
                    icons[i] = new Bitmap(pathFront + i.ToString() + ".jpg");
                }
                catch (ArgumentException e) { }
                img_exist[i] = true;
            }
            try
            {
                backIcon = new Bitmap(pathBack);
            }
            catch (ArgumentException e) { }
            // требуется разобраться со списками (какие из них потом понадобятся)
            for (int i = 0; i < count_arm; i++)
            {
                Card card = new Card(i, (int) Player.Bot);
                this.Controls.Add(card);
                bot_cards.Add(card);
                botSet.cards.Add(card);
            }
            for (int i = 0; i < count_arm; i++)
            {
                Card card = new Card(i, (int) Player.Human);
                this.Controls.Add(card);
                player_cards.Add(card);
                playerSet.cards.Add(card);
            }
        }
        public class Card : PictureBox
        {
            public const int width = 139;
            public const int height = 194;
            public Suit Suit { get; set; }
            public int Rank { get; set; }
            public bool isJoker { get; set; }
            public bool isMoved { get; set; }
            public View View { get; set; }
            public int x { get; set; }
            public int y { get; set; }
            public Card (int number, int type)
            {
                this.Visible = true;
                this.isMoved = false;
                DrawCard(number, type);
            }
            public void DrawCard(int number, int type)
            {
                int i;
                do
                {
                    i = new Random().Next(count_desk);
                    if (img_exist[i])
                    {
                        this.Image = type == 0 ? backIcon : icons[i];
                        this.Width = 139;
                        this.Height = 194;
                        this.Suit = (Suit) (i % 4);
                        this.Rank = i / 4;
                        this.isJoker = false;
                        if (i > 51)
                            this.isJoker = true;
                        this.Tag = i;
                        this.x = number * this.Width * 6/5;
                        this.y = 20 + type * 780;
                        this.Location = new Point(x, y);
                        if (type == 1)
                            this.MouseClick += new MouseEventHandler(click);
                    }

                }
                while (!img_exist[i]);
                img_exist[i] = false;
            }
            public void click(Object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left && !isMoved)
                {
                    this.y -= 60;
                    this.isMoved = true;
                }
                else if (e.Button == MouseButtons.Right && isMoved)
                {
                    this.y += 60;
                    this.isMoved = false;
                }
                this.Location = new Point(x, y);
            }
        }
        public class SetOfCards
        {
            public List<Card> cards { get; set; }
            public int init_x { get; set; }
            public int init_y { get; set; }
            private Point Location;
            public void setLocation()
            {
                this.Location = new Point(init_x, init_y);
            }
        }
    }
    public partial class MyButton: Button
    {
        private Byte type;
        private int x;
        private int y;
        private Form1 container;
        public MyButton(Form1 container, Byte type)
        {
            this.container = container;
            this.type = type;
            this.Width = 300;
            this.Height = 100;
            this.x = 1400;
            this.y = 700;
            y = x / 2;
            if (type == 1)
                this.Text = "Выдать карты";
            else if (type == 2)
                this.Text = "Заменить выбранные карты";
            else throw new ArgumentOutOfRangeException("type is out of bound");
            this.Location = new Point(x, y);
            this.Visible = true;
            this.Click += new EventHandler(click);
            this.BackColor = Color.Aquamarine;
        }
        public void click(Object sender, EventArgs e)
        {
            this.Visible = false;
            container.ThrowCards();
        }
    }
    public enum Suit
    {
        Diamonds,
        Clubs,
        Hearts,
        Spades
    }
    public enum View
    {
        Front,
        Back
    }
    public enum Player
    {
        Bot,
        Human
    }
}
