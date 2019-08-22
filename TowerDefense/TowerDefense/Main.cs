/******************************
 * Bosen Qu (20768684)
 * August 21, 2019
 * Tower Defense Game Main page
 ******************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TowerDefense
{
    public partial class Main : Form
    {
        const int CLIENT_HEIGHT = 800;
        const int CLIENT_WIDTH = 1300;
        const int GAME_BUTTON_AREA_WIDTH = 100;
        public static Size formSize { get { return new Size(CLIENT_WIDTH, CLIENT_HEIGHT); } }
        Map gameMap;
        ButtonArea gameButtonArea;
        Placement gamePlacement;

        public Main()
        {
            InitializeComponent();
            //set form size
            ClientSize = new Size(CLIENT_WIDTH, CLIENT_HEIGHT);
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            //initialize background
            gameMap = new Map(new Point(0, 0), new Size(CLIENT_WIDTH - GAME_BUTTON_AREA_WIDTH, CLIENT_HEIGHT));
            gamePlacement = new Placement(gameMap.MapRect.Location, gameMap.MapRect.Size, gameMap.TileSize);
            gameButtonArea = new ButtonArea(new Point(CLIENT_WIDTH - GAME_BUTTON_AREA_WIDTH, 0), new Size(GAME_BUTTON_AREA_WIDTH, CLIENT_HEIGHT), gamePlacement);   
        }

        void GameLoop()
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            gameButtonArea.paint(e);
            gameMap.paint(e);
            gamePlacement.paint(e);         
        }

        private void Main_MouseClick(object sender, MouseEventArgs e)
        {   
            bool addResponse = gameButtonArea.mouseClick(e);
            gamePlacement.mouseClick(e, addResponse);
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            gamePlacement.mouseMove(e);
            Refresh();
        }
    }
}
