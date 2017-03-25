using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sqlstress.views
{
    public partial class CaseView : UserControl
    {
        public StressScheme Scheme
        {
            get { return _scheme; }
            set { _scheme = value; OnSchemeChanged(); }
        }   private StressScheme _scheme;

        public CaseView()
        {
            InitializeComponent();
        }

        public void OnSchemeChanged()
        {
            propertyGridCase.SelectedObject = this.Scheme;
            propertyGridCase.Update();
        }

    }
}
