using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WFA_20210929
{
    class newButton:Button
    {
        public newButton()
        {
            this.Location = new Point(17, 70);
            this.Name = "newButton";
            this.AutoSize = true;
            this.Text = "newButton";
            this.TabIndex = 0;
            this.UseVisualStyleBackColor = true;
            this.BackColor = Color.AliceBlue;
            this.ForeColor = Color.Brown;
            this.BringToFront();
            this.Enabled = true;
            this.Visible = true;
        }

        private void click(Object sender, EventArgs e)
        {
            this.Text = "11111";
        }

    }
}
