﻿using DolphinScript.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DolphinScript.Forms.UtilityForms
{
    public partial class FixedPointSelectionForm : BaseOverlayForm
    {
        public FixedPointSelectionForm(IScreenService screenService, IScreenCaptureService screenCaptureService, IObjectFactory objectFactory, IPointService pointService) 
            : base(screenService, screenCaptureService, objectFactory, pointService)
        {
            InitializeComponent();
        }
    }
}
