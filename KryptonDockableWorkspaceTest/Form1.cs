using Krypton.Docking;
using Krypton.Navigator;

namespace KryptonDockableWorkspaceTest
{
    public partial class Form1 : Form
    {
        public string DefaultWorkspaceName { get; set; } = "Workspace";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KryptonDockingWorkspace w = kryptonDockingManager.ManageWorkspace(kryptonDockableWorkspace);
            kryptonDockingManager.ManageControl(kryptonPanel, w);
            kryptonDockingManager.ManageFloating(this);

            AddPage("Test", null, new Form(), "Testing description", true, DefaultWorkspaceName);
        }

        KryptonPage NewPage(string name, Image? image, Control content, string description)
        {
            // Create new page with title and image
            KryptonPage p = new KryptonPage();
            p.Text = name;
            p.TextTitle = name;
            p.TextDescription = description;
            if (image != null)
            {
                p.ImageSmall = (Bitmap)image;
            }


            if (content.GetType() == typeof(Form) || content.GetType().IsSubclassOf(typeof(Form)))
            {
                ((Form)content).TopLevel = false;
                ((Form)content).FormBorderStyle = FormBorderStyle.None;
            }

            // Add the control for display inside the page
            content.Dock = DockStyle.Fill;
            p.Controls.Add(content);

            return p;
        }

        public KryptonPage? AddPage(string name, Image? image, Control content, string description, bool add, string workspaceName)
        {
            if (content == null)
            {
                return null;
            }

            // Create page
            var page = NewPage(name, image, content, description);

            if (add)
            {
                // Add page to workspace
                kryptonDockingManager.AddToWorkspace(!string.IsNullOrEmpty(workspaceName) ? workspaceName : DefaultWorkspaceName, new KryptonPage[] { page });

                // Select page
                kryptonDockingManager.CellsWorkspace[0].SelectedPage = page;
            }

            // Show form content
            if (content.GetType().IsSubclassOf(typeof(Form)))
            {
                Form form = (Form)content;
                form.Show();
            }

            return page;
        }

        
    }
}