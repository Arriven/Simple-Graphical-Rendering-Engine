using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrontEnd
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            m_prevFrameTime = DateTime.Now;
            
            InitializeComponent();
            Initialize();
            gameTimer.Interval = 1000 / 150;
            gameTimer.Start();
        }

        public void Initialize()
        {
            m_gameWorld = new GameEngine.GameWorld();
            m_entities = new List<GameEngine.GameEntity>();
            m_entities.Add(m_gameWorld.Factory.Create(new GameEngine.Vector(0, -0.5f), new GameEngine.Vector(0.25f, 0.25f)));
            m_entities.Add(m_gameWorld.Factory.Create(new GameEngine.Vector(0.5f, 0.5f), new GameEngine.Vector(0.25f, 0.25f)));
            m_entities.Add(m_gameWorld.Factory.Create(new GameEngine.Vector(-0.5f, 0.5f), new GameEngine.Vector(0.25f, 0.25f)));
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            var currFrameTime = DateTime.Now;
            var dt = (float)(currFrameTime - m_prevFrameTime).Milliseconds / 1000;
            m_gameWorld.Update(dt);
            m_prevFrameTime = currFrameTime;
            pbMainFrame.Invalidate();
        }

        private void pbMainFrame_Paint(object sender, PaintEventArgs e)
        {
            BMP canvas = new BMP((uint)pbMainFrame.Width, (uint)pbMainFrame.Height);
            m_gameWorld.Render(canvas);
            e.Graphics.DrawImage(canvas.ToBmp(), 0, 0);
        }

        private GameEngine.GameWorld m_gameWorld;
        private DateTime m_prevFrameTime;
        private List<GameEngine.GameEntity> m_entities;
    }
}
